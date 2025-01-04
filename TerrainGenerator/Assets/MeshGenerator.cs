using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class MeshGenerator 
{
    public static MeshInformation GenerateMesh(float[,] noiseMap, float heightMultiplier, AnimationCurve heightCurve)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        MeshInformation meshInformation = new MeshInformation(width, height);

        int vertIndex = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                meshInformation.vertices[vertIndex] = new Vector3(x, heightCurve.Evaluate(noiseMap[x,y]) * heightMultiplier, y);
                meshInformation.uvs[vertIndex] = new Vector2(x / (float)width, y / (float)height); 

                if (x < width - 1 && y < height - 1)
                {
                    meshInformation.AddTriangle(vertIndex, vertIndex + width, vertIndex + width + 1);
                    meshInformation.AddTriangle(vertIndex + width + 1, vertIndex + 1, vertIndex);
                }

                vertIndex++;
            }
        }

        return meshInformation;      
    }
}

public class MeshInformation
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;
    public MeshInformation(int width, int height)
    {
        vertices = new Vector3[width * height];
        triangles = new int[(width - 1) * (height - 1) * 6];
        uvs = new Vector2[width * height];
    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
