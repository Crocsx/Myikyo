using HexasphereGrid;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Header("Shape Properties")]
    public WorldShapeSettings shapeSettings;

    [Header("Colors Properties")]
    public WorldColorSettings colorSettings;

    Hexasphere hexasphere;
    NoiseGenerator noise = new NoiseGenerator();
    GradientTextureGenerator color = new GradientTextureGenerator();

    void Awake()
    {
        hexasphere = Hexasphere.GetInstance("Hexasphere");
    }

    void Start()
    {
        GenerateWorld();
    }



    public void OnShapeSettingsUpdate()
    {
        GenerateWorld();
    }

    public void OnShapeColorUpdate()
    {
        UpdateColors();
    }


    public void GenerateWorld()
    {
        transform.localScale = new Vector3(shapeSettings.radius, shapeSettings.radius, shapeSettings.radius);

        Texture2D heightMap = noise.GenerateTexture(shapeSettings.noiseSettings);
        Texture2D colorMap = color.GenerateGradientTexture(colorSettings.gradient, colorSettings.textureWidth);

        hexasphere.ApplyHeightMap(heightMap, shapeSettings.seaLevel, colorMap);
    }

    void UpdateColors()
    {
        Texture2D colorMap = color.GenerateGradientTexture(colorSettings.gradient, colorSettings.textureWidth);

        hexasphere.UpdateHeightMap(colorMap, shapeSettings.seaLevel);
    }
}