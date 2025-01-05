using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FalloffGenerator 
{
    public static float[,] GenerateFallOffMap(int mapSize, float falloffScale)
    {
        float[,] map = new float[mapSize, mapSize];

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                float x = i / (float)mapSize * 2 - 1;
                float y = j / (float)mapSize * 2 - 1;

                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                map[i, j] = Evaluate(falloffScale ,value);
            }
        }

        return map;
    }

    public static float Evaluate(float falloffScale, float value)
    {
        float a = 3;
        float b = falloffScale;

        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) +  Mathf.Pow(b - b * value, a));
    }
}
