using UnityEngine;

public class GradientTextureGenerator
{
    public Texture2D GenerateGradientTexture(Gradient gradient, int width = 256)
    {
        Texture2D texture = new Texture2D(width, 1);
        for (int i = 0; i < width; i++)
        {
            float t = i / (width - 1f);
            Color color = gradient.Evaluate(t);
            texture.SetPixel(i, 0, color);
        }
        texture.Apply();
        return texture;
    }
}