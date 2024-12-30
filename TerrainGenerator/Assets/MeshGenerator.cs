using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    [SerializeField] private int xSize;
    [SerializeField] private int zSize;

    Vector3[] vertices;
    int[] triangles;


    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        StartCoroutine(UpdateMeshDetails2());
    }

    private void Update()
    {
        UpdateMesh();
    }

    private IEnumerator UpdateMeshDetails2()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }


        triangles = new int[(xSize * zSize) * 6];

        int vert = 0;
        int tri = 0;

        for(int z  = 0; z < zSize; z++)
        {
            for(int i = 0; i < xSize; i++)
            {
                triangles[tri] = vert + 0;
                triangles[tri + 1] = vert + xSize + 1;
                triangles[tri + 2] = vert + 1;
                triangles[tri + 3] = vert + 1;
                triangles[tri + 4] = vert + xSize + 1;
                triangles[tri + 5] = vert + xSize + 2;

                vert++;
                tri += 6;

                yield return new WaitForSeconds(0.1f);
            }
            vert++;
        }




    }

    private void UpdateMeshDetails()
    {
        vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 0),
            new Vector3(1, 0, 1),
            

        };

        triangles = new int[]
        {
            0, 1, 2,
            1, 3, 2
        };
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }


    private void OnDrawGizmos()
    {
        if (vertices == null) return;


        foreach(var vert  in vertices)
        {
            Gizmos.DrawSphere(vert ,0.1f);

        }
    }

}
