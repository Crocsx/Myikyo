using HexasphereGrid;
using UnityEngine;

public class Demo : MonoBehaviour
{
    [Tooltip("Texture with elevation data. The red channel is used as the height value.")]
    public Texture2D heightMap;


    Hexasphere hexa;
    // Update is called once per frame
    void Start()
    {

        hexa = Hexasphere.GetInstance("Hexasphere");
        hexa.ApplyHeightMap(heightMap, 0.5f);
    }
}
