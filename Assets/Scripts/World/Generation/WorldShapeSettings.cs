using UnityEngine;

[CreateAssetMenu(fileName = "WorldShapeSettings", menuName = "ScriptableObjects/World/ShapeSettings", order = 1)]
public class WorldShapeSettings : ScriptableObject
{
    public float radius = 1f;
    public float seaLevel = 0.5f;
    public NoiseSettings noiseSettings;
}