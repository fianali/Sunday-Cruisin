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
    public bool finished = false;

    [SerializeField] private int width;
    [SerializeField] private int length;
    [SerializeField] private int depth;
    
    [SerializeField] private TerrainPainter terrainPainter;
    [SerializeField] private TerrainData baseTerrainData;
    [SerializeField] private TerrainScatter terrainScatter;
    private int _seed;

    public float[] octaves;
    public float redistributionFactor;

    /*void JulienTest()
    {
        // NoiseManager.SimplexPerlin.GetValue(0f);
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = Instantiate(baseTerrainData);
        GenerateTerrain(terrain.terrainData, 1, 1, DoneLoadingCallback);
    }*/

    private void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = Instantiate(baseTerrainData);

        // var roadwidth = TerrainLoader.Instance.roadwidth;
        // int smoothFactor = TerrainLoader.Instance.roadSmoothFactor;
        
        GenerateTerrain(terrain.terrainData, 20, 50, DoneLoadingCallback);
    }
    
    void DoneLoadingCallback()
    {
        Terrain terrain = GetComponent<Terrain>();
        TerrainCollider terrainCollider = GetComponent<TerrainCollider>();
        
        terrainCollider.terrainData = terrain.terrainData;
        terrain.detailObjectDistance = 1000;

        terrain.treeBillboardDistance = 5000;
        // terrainPainter.PaintTerrain(terrain.terrainData);
        terrainScatter.ScatterFoliage(terrain);
        Debug.Log("Finished!");
        finished = true;
    }

    // TerrainData GenerateTerrain(TerrainData terrainData, int roadwidth, int smoothFactor)
    // {
    //     terrainData.heightmapResolution = width;
    //     
    //     terrainData.size = new Vector3(width, depth, length);
    //     
    //     terrainData.SetHeights(0,0, GenerateHeights(roadwidth, smoothFactor));
    //
    //     return terrainData;
    // }
    
    void GenerateTerrain(TerrainData terrainData, int roadwidth, int smoothFactor, GenericDelegate onFinishedCallback)
    {
        terrainData.heightmapResolution = width;
        
        terrainData.size = new Vector3(width, depth, length);

        StartCoroutine(GenerateHeightsCoroutine(roadwidth, smoothFactor, (data) =>
            {
                terrainData.SetHeights(0, 0, data);
                onFinishedCallback?.Invoke();
            }
        ));
    }

    private delegate void GenericDelegate();
    private delegate void GenericDelegate<T>(T variable);
    
    IEnumerator GenerateHeightsCoroutine(int roadwidth, int smoothFactor, GenericDelegate<float[,]> callback)
    {
        System.Diagnostics.Stopwatch timer = new Stopwatch();
        timer.Start();
        
        float[,] heights = new float[width, length];

        Vector3 position = transform.position;
        var xOffset = position.x;
        var zOffset = position.z;

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
                if (zOffset + z > (256 - roadwidth) && zOffset + z < (256 + roadwidth))
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
                    heights[z, x] = roadHeight;
                }
                else
                {
                    heights[z, x] = CompileNoise(z, x, position);
                }
            }
        }
        timer.Stop();
        Debug.Log("Total time: " + timer.ElapsedMilliseconds);

        callback( heights);
    }

    /*float[,] GenerateHeights(int roadwidth, int smoothFactor)
    {
        System.Diagnostics.Stopwatch timer = new Stopwatch();
        timer.Start();
        
        float[,] heights = new float[width, length];
    
        Vector3 position = transform.position;
        var xOffset = position.x;
        var zOffset = position.z;
    
        for (int z = 0; z < width; z++)
        {
            for (int x = 0; x < length; x++)
            {
                if (zOffset + z > (256 - roadwidth) && zOffset + z < (256 + roadwidth))
                {
                    float roadHeight = 0;
                    for (int i = -smoothFactor; i <= smoothFactor; i++)
                    {
                        roadHeight += CompileNoise(256, x + i, position);
                    }
                    roadHeight = roadHeight / (smoothFactor * 2 + 1);
                    if (roadHeight <= (12f/513f)) roadHeight = (12f/513f);
                    heights[z, x] = roadHeight;
                }
                else
                {
                    heights[z, x] = CompileNoise(z, x, position);
                }
            }
        }
        timer.Stop();
        Debug.Log("Total time: " + timer.ElapsedMilliseconds);
    
        return heights;
    }*/

    float CompileNoise(int x, int y, Vector3 position)
    {
        // Sea level
        float height = 0;
        float octaveSum = 0f;

        float xNorm = (x + position.z - (position.z / 513));
        float yNorm = (y + position.x - (position.x / 513));
        
        for (int i = 0; i < octaves.Length; i++)
        {
            float octave = octaves[i];
            
            height += (1/octave) * CalculateNoise(xNorm, yNorm, octave);
            octaveSum += 1/octave;
        }
        height /= octaveSum;

        height = Mathf.Pow(height, redistributionFactor);
        
        return height;
    }
    
    /*float CompileNoise(int x, int y)
    {
        // Sea level
        float height = 0;
        float biomeScale = TerrainLoader.Instance.biomeScale;
        // Mountainsad
        float oceansArea = Mathf.Pow(CalculateNoise(x, y, biomeScale/2), 0.35f);
        float oceans = oceansArea * 0.35f - 0.1f;
        height += oceans;
        
        float mountainsArea = Mathf.Pow(CalculateNoise(x, y, biomeScale), 4f);
        mountainsArea *= oceansArea;
        float mountains = CalculateNoise(x, y, TerrainLoader.Instance.macroScale);
        float mountainNoise = CalculateNoise(x, y, TerrainLoader.Instance.hfScale/6, _seed) / 25;
        mountainNoise += CalculateNoise(x, y, TerrainLoader.Instance.hfScale/2, _seed) / 75;
        mountains += mountainNoise;
        mountains *= mountainsArea;
        height += mountains;

        float highFrequencyNoise = (CalculateNoise(x, y, TerrainLoader.Instance.hfScale, _seed + 1000) + CalculateNoise(x, y, TerrainLoader.Instance.hfScale, _seed + 2000)) / TerrainLoader.Instance.hfSize;
        height += highFrequencyNoise;
        
        return height;
    }*/

    float CalculateNoise(float xNorm, float yNorm, float scale)
    {
        /*float xNorm = (float) (x + position.z - (position.z / 513)) / width * scale + _seed ;
        float yNorm = (float) (y + position.x - (position.x / 513)) / length * scale + _seed ;*/
        
        xNorm = xNorm / width * scale;
        yNorm = yNorm / length * scale;

        // return (NoiseManager.SimplexPerlin.GetValue(xNorm, yNorm) + 1)/1;
        return Mathf.PerlinNoise(xNorm, yNorm);
    }
    
    // float CalculateNoise(int x, int y, float scale, float seed)
    // {
    //     var position = transform.position;
    //     float xNorm = (float) (x + position.z - (position.z / 513)) / width * scale + seed ;
    //     float yNorm = (float) (y + position.x - (position.x / 513)) / length * scale + seed ;
    //     
    //     return (TerrainLoader.Instance.simplexPerlin.GetValue(xNorm, yNorm) + 1)/1;
    //     return Mathf.PerlinNoise(xNorm, yNorm);
    // }
    
}