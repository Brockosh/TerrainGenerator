using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDisplay : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DisplayMesh(Mesh mesh, Texture2D texture, float minHeight, float maxHeight, Color[] colours, float[] colourStartHeights, float[] baseBlends)
    {
        meshRenderer.material.SetInt("colourCount", colours.Length);
        meshRenderer.material.SetColorArray("colours", colours);
        meshRenderer.material.SetFloatArray("colourStartHeights", colourStartHeights);
        meshRenderer.material.SetFloatArray("baseBlends", baseBlends);

        meshRenderer.material.SetFloat("minHeight", minHeight);
        meshRenderer.material.SetFloat("maxHeight", maxHeight);


        meshFilter.mesh = mesh;

        mesh.vertices = mesh.vertices;
        mesh.triangles = mesh.triangles;
        mesh.colors = mesh.colors;
        mesh.uv = mesh.uv;

        texture.Apply();

        meshRenderer.material.mainTexture = texture;
    }
}
