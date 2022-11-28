using System;
using UnityEngine;
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

    public float[] octaves;
    public float redistributionFactor;
    private void Start()
    {
        _seed = TerrainLoader.Instance.seed;

        
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = Instantiate(baseTerrainData);
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        
        TerrainCollider terrainCollider = GetComponent<TerrainCollider>();
        terrainCollider.terrainData = terrain.terrainData;

        terrain.treeBillboardDistance = 1000;
        terrainPainter.PaintTerrain(terrain.terrainData);
        terrainScatter.ScatterFoliage(terrain);
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

        var xOffset = transform.position.x;
        var zOffset = transform.position.z;

        var roadwidth = 20;
        int smoothFactor = 50;

        for (int z = 0; z < width; z++)
        {
            for (int x = 0; x < length; x++)
            {
                if (zOffset + z > (256 - roadwidth) && zOffset + z < (256 + roadwidth))
                {
                    float roadHeight = 0;
                    for (int i = -smoothFactor; i <= smoothFactor; i++)
                    {
                        roadHeight += CompileNoise(256, x + i);
                    }
                    heights[z, x] = roadHeight / (smoothFactor * 2 + 1);
                }
                else
                {
                    heights[z, x] = CompileNoise(z, x);
                }
            }
        }

        return heights;
    }

    float CompileNoise(int x, int y)
    {
        // Sea level
        float height = 0;
        float octaveSum = 0f;
        
        for (int i = 0; i < octaves.Length; i++)
        {
            float octave = octaves[i];
            
            height += (1/octave) * CalculateNoise(x, y, octave);
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