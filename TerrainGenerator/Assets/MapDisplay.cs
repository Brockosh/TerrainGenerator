using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;

    public void DisplayMap(float[,] noiseMap, Texture2D texture)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        texture.Apply();

        textureRenderer.material.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width, 1, height);

    }

    public void DisplayMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {


                //THIS HAS BEEN INVERTED TO PUT BLACK FIRST
                texture.SetPixel(x, y, Color.Lerp(Color.black, Color.white, noiseMap[x, y]));
            }
        }

        texture.Apply();

        textureRenderer.material.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width, 1, height);

    }
}
