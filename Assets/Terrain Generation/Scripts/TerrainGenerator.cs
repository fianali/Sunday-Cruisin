using System;
using System.Collections;
using System.Diagnostics;
using Graphics.Tools.Noise;
using Graphics.Tools.Noise.Primitive;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    /*IEnumerator GenerateHeightsCoroutine(int roadwidth, int smoothFactor, int seed, GenericDelegate<float[,]> callback)
    {
        System.Diagnostics.Stopwatch timer = new Stopwatch();
        timer.Start();
    
        float[,] heights = new float[width, length];

        Vector3 position = transform.position + new Vector3(seed, 0, seed);

        for (int z = 0; z < width; z++)
        
        {
            if (timer.ElapsedMilliseconds > 3)
            {
                yield return null;
                timer.Reset();
                timer.Start();
            }

            for (int x = 0; x < length; x++)
            {
                heights[z, x] = CompileNoise(z, x, position);
            }
        }
        timer.Stop();
        // Debug.Log("Total time: " + timer.ElapsedMilliseconds);
        
        callback( heights);
    }
    
    float CompileNoise(int x, int y, Vector3 position)
    {
        // Sea level
        float height = 0;
        float octaveSum = 0f;

        float xNorm = (x + position.z - (position.z / 513)) ;
        float yNorm = (y + position.x - (position.x / 513)) ;
    
        for (int i = 0; i < octaves.Length; i++)
        {
            height += (1/octaves[i]) * CalculateNoise(xNorm, yNorm, octaves[i]);
            octaveSum += 1/octaves[i];
        }
        height /= octaveSum;

        height = Mathf.Pow(height, redistributionFactor);
    
        return height;
    }
    
    float CalculateNoise(float xNorm, float yNorm, float scale)
    {
        xNorm = xNorm / width * scale;
        yNorm = yNorm / length * scale;

        // return (NoiseManager.SimplexPerlin.GetValue(xNorm, yNorm) + 1)/1;
        return Mathf.PerlinNoise(xNorm, yNorm);
    }*/
}