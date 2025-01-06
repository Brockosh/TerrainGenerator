using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator 
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float noiseScale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if (noiseScale <= 0) noiseScale = 0.0001f;

        float minNoiseHeight = float.MaxValue;
        float maxNoiseHeight = float.MinValue;


        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                //The amount of impact each octave will have
                float amplitude = 1;

                //How much we zoom out each octave
                float frequency = 1;

                float noiseHeight = 0;


                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = x / noiseScale * frequency + offset.x;
                    float sampleY = y / noiseScale * frequency + offset.y;

                    //Multiply by 2 and subtract 1 to change range from 0 - 1 to -1 - 1
                    //This allows our noise to be more interesting and have more valleys and treat 0 as the true centre of the noise
                    //Each octave will only add height if we don't add this * 2 - 1
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;

                }

                //This just sets our maxNoiseHeight and minNoiseHeight to the highest and lowest height value we have gotten
                //across all of our pixels
                GetHighestAndLowestHeight(noiseHeight, ref minNoiseHeight, ref maxNoiseHeight);


                noiseMap[x, y] = noiseHeight;
            }
        }

        noiseMap = NormalizeNoiseMap(noiseMap, minNoiseHeight, maxNoiseHeight);


        return noiseMap;
    }

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float noiseScale, int octaves, float persistance, float lacunarity, Vector2 offset, float[,] falloffMap)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if (noiseScale <= 0) noiseScale = 0.0001f;

        float minNoiseHeight = float.MaxValue;
        float maxNoiseHeight = float.MinValue;


        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                //The amount of impact each octave will have
                float amplitude = 1;

                //How much we zoom out each octave
                float frequency = 1;

                float noiseHeight = 0;


                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = x / noiseScale * frequency + offset.x;
                    float sampleY = y / noiseScale * frequency + offset.y;

                    //Multiply by 2 and subtract 1 to change range from 0 - 1 to -1 - 1
                    //This allows our noise to be more interesting and have more valleys and treat 0 as the true centre of the noise
                    //Each octave will only add height if we don't add this * 2 - 1
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;

                }

                //This just sets our maxNoiseHeight and minNoiseHeight to the highest and lowest height value we have gotten
                //across all of our pixels
                GetHighestAndLowestHeight(noiseHeight, ref minNoiseHeight, ref maxNoiseHeight);


                noiseMap[x, y] = noiseHeight;
            }
        }

        noiseMap = NormalizeNoiseMap(noiseMap, minNoiseHeight, maxNoiseHeight);


        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);

            }
        }

        return noiseMap;
    }

    public static void GetHighestAndLowestHeight(float noiseHeight, ref float minHeight, ref float maxHeight)
    {
        if (noiseHeight > maxHeight)
        {
            maxHeight = noiseHeight;
        }
        else if (noiseHeight < minHeight)
        {
            minHeight = noiseHeight;
        }
    }

    public static float[,] NormalizeNoiseMap(float[,] map, float minNoiseHeight, float maxNoiseHeight)
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //Gives us the position of the value between two values, returned between 0 and 1
                //e.g. min = 0, max = 40, valueToCheck = 20, InverseLerp will return 0.5
                map[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, map[x, y]);
            }
        }

        return map;
    }
}
