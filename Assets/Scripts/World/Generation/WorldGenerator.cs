using HexasphereGrid;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public int seed = 1;

    [Header("Shape Properties")]
    public WorldShapeSettings shapeSettings;

    [Header("Tiles Properties")]
    public WorldTileSettings tileSettings;

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
        Random.InitState(seed);
        NoiseUtility.InitState(seed);
        GenerateWorld();
        GenerateTiles();
    }



    public void OnShapeSettingsUpdate()
    {
        GenerateWorld();
    }

    public void OnShapeColorUpdate()
    {
        UpdateColors();
    }

    public void onTileSettingUpdate()
    {
        GenerateTiles();
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

    void GenerateTiles()
    {
        GameObjectUtils.DestroyAllInstances<TileBase>();

        for (int i = 0; i < hexasphere.tiles.Length; i++)
        {
            Tile currTile = hexasphere.tiles[i];
            float extrudeAmount = currTile.extrudeAmount;
            foreach (TileSettings tileSettings in tileSettings.tiles)
            {
                if (currTile.isWater && !tileSettings.spawnOnWater)
                {
                    continue;
                }

                // If the extrude amount is within the range for this tile type
                if (extrudeAmount >= tileSettings.minElevationRange && extrudeAmount <= tileSettings.maxElevationRange && Random.Range(0f, 1f) < tileSettings.frequency)
                {
                    // To apply a proper scale, get as a reference the length of a diagonal in tile 0 (note the "false" argument which specifies the position is in local coordinates)
                    float size = Vector3.Distance(hexasphere.GetTileVertexPosition(0, 0, false), hexasphere.GetTileVertexPosition(0, 3, false));

                    hexasphere.GenerateTileGameObject(i);

                    // Instantiate the prefab for this tile type
                    GameObject tileObject = Instantiate(tileSettings.tile);
                    tileObject.transform.position = hexasphere.GetTileCenter(i);
                    tileObject.transform.SetParent(currTile.renderer.gameObject.transform);
                    tileObject.transform.LookAt(hexasphere.transform.position);
                    tileObject.transform.Rotate(Vector3.right, -90f);
                    tileObject.transform.localScale = new Vector3(size, size, size);
                    break;
                }
            }
        }
    }
}