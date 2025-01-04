using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawType
    {
        noiseMap,
        colourMap
    }

    public DrawType drawType = DrawType.noiseMap;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public int octaves;

    [Range(0, 1)] public float persistance;
    public float lacunarity;

    public MapRegion[] mapRegions;

    public Vector2 offset;

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
        float[,] noiseMap = NoiseGenerator.GenerateNoiseMap(mapWidth, mapHeight, noiseScale, octaves, persistance, lacunarity, offset);
        Texture2D texture = TextureGenerator.CreateTextureFromNoiseMap(noiseMap, mapRegions);

        MapDisplay display = FindObjectOfType<MapDisplay>();

        if (drawType == DrawType.noiseMap)
        {
            display.DisplayMap(noiseMap);
        }
        else
        {
            display.DisplayMap(noiseMap, texture);
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
