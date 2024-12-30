using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField] private int width = 256;
    [SerializeField] private int height = 256;

    [SerializeField] private int scale = 20;

    [SerializeField] private int xOffset = 0;
    [SerializeField] private int yOffset = 0;

    [SerializeField] private int octaves = 0;
    [SerializeField] private float persistance = 0;
    [SerializeField] private float lacunarity = 0;

    

    private void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = UpdatePixelColours();
        UpdatePixelColours();
    }

    Texture2D UpdatePixelColours()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                   float xForPerlin = GetScaledCoord(x, y).xPix;
                   float yForPerlin = GetScaledCoord(x, y).yPix;

                    Color colour = GetPerlinNoiseForCoord(xForPerlin, yForPerlin);

                    texture.SetPixel(x, y, colour);
            }
        }

        texture.Apply();
        return texture;
    }

    private (float xPix, float yPix) GetScaledCoord(int xPixel, int yPixel)
    {
        float scaledX = ((float)xPixel / width * scale) + xOffset;
        float scaledY = ((float)yPixel / height * scale) + yOffset;

        return (scaledX, scaledY);
    }

    private Color GetPerlinNoiseForCoord(float xCoord, float yCoord)
    {
        float colourVal = Mathf.PerlinNoise(xCoord, yCoord);

        return new Color(colourVal, colourVal, colourVal);
    }


}
