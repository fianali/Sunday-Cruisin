using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class Chunk : MonoBehaviour
{
    [SerializeField] private TerrainData baseTerrainData;
    [SerializeField] private float[] octaves;
    [SerializeField] private float redistributionFactor;
    
    [SerializeField] private TerrainPainter terrainPainter;
    [SerializeField] private TerrainScatter terrainScatter;
    
    private const int length = 513;
    private const int width = 513;
    private const int depth = 513;

    private UnityEvent chunkLoaded;
    
    private float[,] tempMap;
    private float[,] heightMap;
    private float[,] moistureMap;
    // TODO: Make splatMap
    
    private Terrain terrain;
    private TerrainData terrainData;

    private delegate void GenericDelegate();
    private delegate void GenericDelegate<T>(T variable);

    private void Awake()
    {
        chunkLoaded = TerrainLoader.Instance.chunkLoaded;
    }

    private void Start()
    {
        terrain = GetComponent<Terrain>();
        terrain.terrainData = Instantiate(baseTerrainData);
        terrainData = terrain.terrainData;
        terrainData.heightmapResolution = width;
        terrainData.size = new Vector3(width, depth, length);

        MakeNoise(20, 0, 5000, MapStepTwo);
    }

    private void MapStepTwo()
    {
        heightMap = tempMap;
        MakeNoise(20, 0, 10000, SetTerrain);
    }

    private void SetTerrain()
    {
        moistureMap = tempMap;

        // var pos = transform.position;
        if (transform.position.z == 0)
        {
            for (int x = 0; x < 513; x++)
            {
                for (int z = 256 - 20; z < 256 + 20; z++)
                {
                    float roadHeight = 20f/513f;
                    /*for (int i = -smoothFactor; i <= smoothFactor; i++)
                    {
                        var potentialHeight = CompileNoise(256, x + i, position);
                        if (potentialHeight >= (24f / 513f)) potentialHeight = (24f / 513f);
                        if (potentialHeight <= (12f / 513f)) potentialHeight = (12f/513f);
                        roadHeight += potentialHeight;
                    }
                    roadHeight /= (smoothFactor * 2 + 1);*/
                    heightMap[z, x] = roadHeight;
                }
            }
            
        }

        terrainData.SetHeights(0, 0, heightMap);
        
        TerrainCollider terrainCollider = GetComponent<TerrainCollider>();
        terrainCollider.terrainData = terrain.terrainData;
        
        terrain.detailObjectDistance = 1000;
        terrain.treeBillboardDistance = 5000;
        
        terrainPainter.PaintTerrain(terrain.terrainData, heightMap, moistureMap);
        terrainScatter.ScatterFoliage(terrain);
        
        chunkLoaded.Invoke();
    }
    

    // Helper functions
    private void MakeNoise(int roadwidth, int smoothFactor, int seed, GenericDelegate onFinishedCallback)
    {
        StartCoroutine(GenerateHeightsCoroutine(roadwidth, smoothFactor, seed, data =>
            {
                tempMap = data;
                onFinishedCallback?.Invoke();
            }
        ));
    }

    IEnumerator GenerateHeightsCoroutine(int roadwidth, int smoothFactor, int seed, GenericDelegate<float[,]> callback)
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
    }
}
