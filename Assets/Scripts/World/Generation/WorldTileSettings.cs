using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TileSettings
{
    [Range(0, 1)]
    public float minElevationRange;
    [Range(0, 1)]
    public float maxElevationRange;
    [Range(0, 1)]
    public float frequency;
    public bool spawnOnWater = false;
    public GameObject tile;
}

[CreateAssetMenu(fileName = "WorldTileSettings", menuName = "ScriptableObjects/World/TileSettings", order = 1)]
public class WorldTileSettings : ScriptableObject
{
    [SerializeField]
    public List<TileSettings> tiles = new List<TileSettings>();
}
