using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    [SerializeField] private int xSize;
    [SerializeField] private int zSize;

    [SerializeField] private int scale;
    [SerializeField] private float heightMultiplier = 2f;

    [SerializeField] private float xOffset;
    [SerializeField] private float zOffset;

    [SerializeField] private float noiseScale;

    Vector3[] vertices;
    int[] triangles;


    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        UpdateMeshDetails();
        UpdateMesh();
    }

    private void UpdateMeshDetails()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector3(x, GenerateHeightValue(x, z), z);
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

            }
            vert++;
        }
    }

    private float GenerateHeightValue(float x, float z)
    {
        float y = Mathf.PerlinNoise((x * noiseScale) + xOffset, z * noiseScale + zOffset) * heightMultiplier;
        return y;
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}