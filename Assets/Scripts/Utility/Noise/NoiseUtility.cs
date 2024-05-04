using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class provides utility functions for generating noise.
/// </summary>
public static class NoiseUtility
{
    /// <summary>
    /// This function generates Fractal Brownian Motion (FBM) noise.
    // FBM is a type of noise that combines multiple layers of Simplex noise.
    /// </summary>
    /// <param name="x">The coordinates at which to generate the noise.</param>
    /// <param name="y">The coordinates at which to generate the noise.</param>
    /// <param name="oct">The number of layers of noise to combine.</param>
    /// <param name="persistance">Controls the decrease in amplitude of each subsequent layer of noise.</param>
    /// <returns>Returns the total noise normalized by the maximum possible value.</returns>
    public static float FractalBrownianMotion(float x, float y, int oct, float persistance)
    {
        float total = 0; // The total noise value.
        float frequency = 1; // The frequency of the first layer of noise.
        float amplitude = 1; // The amplitude of the first layer of noise.
        float maxValue = 0; // The maximum possible value of the total noise.

        // Loop over each layer of noise.
        for (int i = 0; i < oct; i++)
        {
            // Generate a layer of noise and add it to the total noise.
            total += OpenSimplex2S.Noise2(1, x * frequency, y * frequency) * amplitude;

            // Add the amplitude to the maximum possible value.
            maxValue += amplitude;

            // Decrease the amplitude for the next layer of noise.
            amplitude *= persistance;

            // Increase the frequency for the next layer of noise.
            frequency *= 2;
        }

        // Return the total noise normalized by the maximum possible value.
        return total / maxValue;
    }

    /// <summary>
    /// This function maps a value from one range to another.
    /// </summary>
    /// <param name="value">The value to map.</param>
    /// <param name="originalMin">The original range of the value.</param>
    /// <param name="originalMax">The original range of the value.</param>
    /// <param name="targetMin">The target range to map the value to.</param>
    /// <param name="targetMax">The target range to map the value to.</param>
    /// <returns>Returns the value mapped from the original range to the target range.</returns>
    public static float Map(float value, float originalMin, float originalMax, float targetMin, float targetMax)
    {
        // Map the value from the original range to the target range.
        return (value - originalMin) * (targetMax - targetMin) / (originalMax - originalMin) + targetMin;
    }
}