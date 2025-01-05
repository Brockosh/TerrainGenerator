using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawType
    {
        noiseMap,
        colourMap,
        falloffMap,
        mesh
    }

    public DrawType drawType = DrawType.noiseMap;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public int octaves;

    public float falloffScale;

    [Range(0, 1)] public float persistance;
    public float lacunarity;


    public MapRegion[] mapRegions;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    private void Start()
    {
        //GenerateMap();
    }

    private void Update()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        float[,] falloffMap = FalloffGenerator.GenerateFallOffMap(mapWidth, falloffScale);
;       float[,] noiseMap = NoiseGenerator.GenerateNoiseMap(mapWidth, mapHeight, noiseScale, octaves, persistance, lacunarity, offset, falloffMap);
        Texture2D texture = TextureGenerator.CreateTextureFromNoiseMap(noiseMap, mapRegions);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        MeshDisplay meshDisplay = FindObjectOfType<MeshDisplay>();

        if (drawType == DrawType.noiseMap)
        {
            display.DisplayMap(noiseMap);
        }
        else if (drawType == DrawType.colourMap)
        {
            display.DisplayMap(noiseMap, texture);
        }
        else if (drawType == DrawType.mesh)
        {
            MeshInformation meshInfo = MeshGenerator.GenerateMesh(noiseMap, meshHeightMultiplier, meshHeightCurve);
            Mesh mesh = meshInfo.CreateMesh();
            meshDisplay.DisplayMesh(mesh, texture);
        }
        else if (drawType == DrawType.falloffMap)
        {
            //float[,] falloffMap = FalloffGenerator.GenerateFallOffMap(mapWidth);
            display.DisplayMap(falloffMap);
        }
    }
}

[System.Serializable]
public struct MapRegion
{
    public string regionName;
    public float regionHeight;
    public Color regionColour;
}
