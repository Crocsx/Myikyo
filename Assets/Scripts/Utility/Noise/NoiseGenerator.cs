
using UnityEngine;

public class NoiseGenerator
{

    /// <summary>
    /// Generates a texture based on the provided noise settings.
    /// </summary>
    /// <param name="settings">The settings to use for generating the noise.</param>
    /// <returns>A Texture2D object representing the generated noise.</returns>

    public Texture2D GenerateTexture(NoiseSettings settings)
    {
        int w = 513;
        int h = 513;
        float pValue;

        Texture2D texture = new Texture2D(513, 513, TextureFormat.ARGB32, false);

        float minColour = 1;
        float maxColour = 0;

        // Loop over each pixel in the texture.
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                // Generate noise based on the provided settings.
                if (settings.seamless)
                {
                    float u = x / w;
                    float v = y / h;
                    float noise00 = NoiseUtility.FractalBrownianMotion((x + settings.offsetX) * settings.scaleX,
                                              (y + settings.offsetY) * settings.scaleY,
                                        settings.octaves,
                                        settings.persistance) * settings.heightScale;
                    float noise01 = NoiseUtility.FractalBrownianMotion((x + settings.offsetX) * settings.scaleX,
                                              (y + settings.offsetY + h) * settings.scaleY,
                                        settings.octaves,
                                        settings.persistance) * settings.heightScale;
                    float noise10 = NoiseUtility.FractalBrownianMotion((x + settings.offsetX + w) * settings.scaleX,
                                              (y + settings.offsetY) * settings.scaleY,
                                        settings.octaves,
                                        settings.persistance) * settings.heightScale;
                    float noise11 = NoiseUtility.FractalBrownianMotion((x + settings.offsetX + w) * settings.scaleX,
                                              (y + settings.offsetY + h) * settings.scaleY,
                                        settings.octaves,
                                        settings.persistance) * settings.heightScale;
                    float noiseTotal = u * v * noise00 +
                                    u * (1 - v) * noise01 +
                                    (1 - u) * v * noise10 +
                                    (1 - u) * (1 - v) * noise11;

                    float value = (int)(256 * noiseTotal) + 50;
                    float r = Mathf.Clamp((int)noise00, 0, 255);
                    float g = Mathf.Clamp(value, 0, 255);
                    float b = Mathf.Clamp(value + 50, 0, 255);

                    pValue = (r + g + b) / (3 * 255.0f);
                }
                else
                {
                    pValue = NoiseUtility.FractalBrownianMotion((x + settings.offsetX) * settings.scaleX,
                                       (y + settings.offsetY) * settings.scaleY,
                                            settings.octaves,
                                            settings.persistance) * settings.heightScale;
                }

                // Apply contrast and brightness to the noise value.
                float colValue = settings.contrast * (pValue - 0.5f) + 0.5f * settings.brightness;
                if (minColour > colValue) minColour = colValue;
                if (maxColour < colValue) maxColour = colValue;

                // Set the pixel color based on the noise value.
                Color pixCol = new Color(colValue, colValue, colValue, settings.alpha ? colValue : 1);
                texture.SetPixel(x, y, pixCol);
            }
        }

        // Map the color values to the range [0, 1] if the map setting is enabled.
        if (settings.map)
        {
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    Color pixCol = texture.GetPixel(x, y);
                    float colValue = pixCol.r;
                    colValue = NoiseUtility.Map(colValue, minColour, maxColour, 0, 1);
                    pixCol.r = colValue;
                    pixCol.g = colValue;
                    pixCol.b = colValue;
                    texture.SetPixel(x, y, pixCol);
                }
            }
        }

        // Apply the changes to the texture and return it.
        texture.Apply(false, false);
        return texture;
    }
}
