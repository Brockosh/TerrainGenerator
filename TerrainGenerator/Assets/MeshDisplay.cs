using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDisplay : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DisplayMesh(Mesh mesh, Texture2D texture, float minHeight, float maxHeight)
    {
        meshRenderer.material.SetFloat("_minHeight", minHeight);
        Debug.Log($"Min Height = {minHeight}");
        meshRenderer.material.SetFloat("_maxHeight", maxHeight);
        Debug.Log($"Max Height = {maxHeight}");


        meshFilter.mesh = mesh;

        mesh.vertices = mesh.vertices;
        mesh.triangles = mesh.triangles;
        mesh.colors = mesh.colors;
        mesh.uv = mesh.uv;

        texture.Apply();

        meshRenderer.material.mainTexture = texture;
    }
}
