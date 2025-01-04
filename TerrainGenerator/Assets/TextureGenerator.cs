using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class TextureGenerator 
{
    public static Texture2D CreateTextureFromNoiseMap(float[,] noiseMap, MapRegion[] regions)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);


        Texture2D texture = new Texture2D(noiseMap.GetLength(0), noiseMap.GetLength(1));


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int i = 0; i < regions.Length; i++)
                {
                    if (noiseMap[x, y] <= regions[i].regionHeight)
                    {
                        texture.SetPixel(x, y, regions[i].regionColour);
                        break;
                    }
                }
            }
        }


        return texture;
    }




}
