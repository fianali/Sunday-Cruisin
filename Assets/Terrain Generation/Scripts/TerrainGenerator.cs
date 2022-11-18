using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int length;
    [SerializeField] private int depth;
    
    [SerializeField] private TerrainPainter terrainPainter;
    [SerializeField] private TerrainData baseTerrainData;
    [SerializeField] private TerrainScatter terrainScatter;
    private float _seed;

    private void Start()
    {
        _seed = TerrainLoader.Instance.seed;

        System.Diagnostics.Stopwatch timer = new Stopwatch();
        timer.Start();
        long time1, time2, time3, time4;
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = Instantiate(baseTerrainData);
        time1 = timer.ElapsedMilliseconds;
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        time2 = timer.ElapsedMilliseconds;
        TerrainCollider terrainCollider = GetComponent<TerrainCollider>();
        terrainCollider.terrainData = terrain.terrainData;

        terrain.treeBillboardDistance = 1000;
        terrainPainter.PaintTerrain(terrain.terrainData);
        time3 = timer.ElapsedMilliseconds;
        terrainScatter.ScatterFoliage(terrain);
        time4 = timer.ElapsedMilliseconds;
        Debug.Log("Finished");
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width;
        
        terrainData.size = new Vector3(width, depth, length);
        
        terrainData.SetHeights(0,0, GenerateHeights());

        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, length];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                if (x > (256 - 50) && x < (256 + 50))
                {
                    float roadHeight = 0;
                    int smoothFactor = 10;
                    for (int i = -smoothFactor; i <= smoothFactor; i++)
                    {
                        roadHeight += CompileNoise(256, y + i);
                    }
                    heights[x, y] = roadHeight / (smoothFactor * 2 + 1);
                }
                else
                {
                    heights[x, y] = CompileNoise(x, y);
                }
                // heights[x, y] = CalculateNoise(x, y, _tempSeed, macroScale);
            }
        }

        return heights;
    }

    float CompileNoise(int x, int y)
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
    }

    float CalculateNoise(int x, int y, float scale)
    {
        var position = transform.position;
        float xNorm = (float) (x + position.z - (position.z / 513)) / width * scale + _seed ;
        float yNorm = (float) (y + position.x - (position.x / 513)) / length * scale + _seed ;
        
        return Mathf.PerlinNoise(xNorm, yNorm);
    }
    
    float CalculateNoise(int x, int y, float scale, float seed)
    {
        var position = transform.position;
        float xNorm = (float) (x + position.z - (position.z / 513)) / width * scale + seed ;
        float yNorm = (float) (y + position.x - (position.x / 513)) / length * scale + seed ;
        
        return Mathf.PerlinNoise(xNorm, yNorm);
    }
    
}