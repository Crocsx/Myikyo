using UnityEngine;

[CreateAssetMenu(fileName = "WorldColorSettings", menuName = "ScriptableObjects/World/ColorSettings", order = 1)]
public class WorldColorSettings : ScriptableObject
{
    public Gradient gradient;
    public int textureWidth = 257;
}
