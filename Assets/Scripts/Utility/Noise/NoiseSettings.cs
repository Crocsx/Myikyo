using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    [Header("Noise Properties")]
    [Tooltip("Seed to use for generation")]
    public int seed = 1;
    [Range(0f, 0.1f)]
    [Tooltip("This properties control the scaling factors applied to the OpenSimple 2 noise function in the X and Y directions, respectively. Adjusting these values will change the overall\"frequency\" of the generated noise pattern.")]
    public float scaleX = 0.001f;
    [Range(0f, 0.1f)]
    [Tooltip("This properties control the scaling factors applied to the OpenSimple 2 noise function in the X and Y directions, respectively. Adjusting these values will change the overall\"frequency\" of the generated noise pattern.")]
    public float scaleY = 0.001f;
    [Range(1, 10)]
    [Tooltip("This property determines the number of octaves used in the OpenSimple 2 noise generation. Increasing the value will add more detail and complexity to the height map.")]
    public int octaves = 5;
    [Range(1, 10)]
    [Tooltip(" This property controls the persistence of the Perlin noise. It affects the amplitude of each subsequent octave in relation to the previous octave.Higher values create more pronounced variations between different octaves.")]
    public float persistance = 2.5f;
    [Range(0, 1)]
    [Tooltip("This property adjusts the overall height of the generated terrain. Increasing this value will result in a higher range of heights, while decreasing it will create flatter terrain.")]
    public float heightScale = 0.5f;
    [Range(1, 10000)]
    [Tooltip("This properties allow you to offset the Perlin noise function in the X and Y directions. Adjusting these values will shift the generated terrain horizontally and vertically.")]
    public int offsetX = 1000;
    [Range(1, 10000)]
    [Tooltip("This properties allow you to offset the Perlin noise function in the X and Y directions. Adjusting these values will shift the generated terrain horizontally and vertically.")]
    public int offsetY = 1000;
    [Range(0, 2)]
    [Tooltip("This properties control the brightness adjustments applied to the generated height map.Increasing the brightness value will make the terrain brighter")]
    public float brightness = 1;
    [Range(0, 2)]
    [Tooltip("This properties control the contrast adjustments applied to the generated height map.increasing the contrast value will increase the difference between light and dark areas.")]
    public float contrast = 1;
    [Tooltip("This property represents a toggle that determines whether the alpha channel of the generated texture should be used. If enabled (true), the alpha channel will be set to the height values,creating a grayscale image. If disabled (false), the alpha channel will be set to fully opaque.")]
    public bool alpha = true;
    [Tooltip("This property determines whether a grayscale map of the height values should be generated alongside the height map. If enabled (true) the height values are mapped to a grayscale range of colors, allowing for a visual representation of the terrain.\r\n")]
    public bool map = true;
    [Tooltip("This property controls the generation of seamless terrain. If enabled (true), the Perlin noise function is evaluated at the corners of the texture and blended together,ensuring a smooth transition across the borders of the height map.")]
    public bool seamless = true;
}
