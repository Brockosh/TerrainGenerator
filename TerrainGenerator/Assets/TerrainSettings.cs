using UnityEngine;

[CreateAssetMenu(fileName = "TerrainSettings", menuName = "ScriptableObjects/TerrainSettings", order = 1)]
public class TerrainSettings : ScriptableObject
{
    [Header("Color Settings")]
    [Tooltip("Array of colors to blend based on height.")]
    public Color[] colours;

    [Header("Height Settings")]
    [Tooltip("Start heights for each color.")]
    [Range(0, 1)]
    public float[] colourStartHeights;

    [Header("Blend Settings")]
    [Tooltip("Base blend values for each color.")]
    [Range(0, 1)]
    public float[] baseBlends;
}