using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDisplay : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DisplayMesh(Mesh mesh, Texture2D texture)
    {
        meshFilter.mesh = mesh;

        mesh.vertices = mesh.vertices;
        mesh.triangles = mesh.triangles;
        mesh.uv = mesh.uv;

        texture.Apply();

        meshRenderer.material.mainTexture = texture;
    }
}
