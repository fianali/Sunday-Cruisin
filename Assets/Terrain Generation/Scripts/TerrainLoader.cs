using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLoader : MonoBehaviour
{
    public static TerrainLoader Instance;
    public float seed;

    public float macroScale;
    public float biomeScale;
    public float hfScale;
    public float hfSize;
    
    // public GameObject[,] loadedChunks;
    [SerializeField] private GameObject terrain;
    [SerializeField] private int loadDistance;
    // [SerializeField] private Transform player;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        for (var i = 0; i < loadDistance; i++)
        {
            for (var j = 0; j < loadDistance; j++)
            {
                Instantiate(terrain, new Vector3(i*513f, 0, j*513f), new Quaternion(0,0,0,0));
            }
        }
    }
    
    // void GenerateTerrain(int x, int y)
    // {
    //     TerrainData terrainData = new TerrainData();
    //     // terrainData.size = new Vector3(512,512,512);
    //     terrainData.size = new Vector3(width, depth, length);
    //     terrainData.heightmapResolution = width;
    //     terrainData.SetHeights(0,0, GenerateHeights());
    //     Terrain.CreateTerrainGameObject(terrainData);
    // }
    //
    // float[,] GenerateHeights()
    // {
    //     float[,] heights = new float[width, length];
    //
    //     for (int x = 0; x < width; x++)
    //     {
    //         for (int y = 0; y < length; y++)
    //         {
    //             heights[x, y] = CalculateNoise(x,y,TerrainLoader.Instance.seed,macroScale);
    //         }
    //     }
    //
    //     return heights;
    // }
    //
    // float CalculateNoise(int x, int y, float seed, float scale)
    // {
    //     var position = transform.position;
    //     float xNorm = (float) x / width * scale + seed + position.x;
    //     float yNorm = (float) y / length * scale + seed + position.y;
    //     
    //     return Mathf.PerlinNoise(xNorm, yNorm);
    // }

    // Update is called once per frame
    // void Update()
    // {
    //     // Set position to the same position as the player
    //     var playerPosition = player.position;
    //     transform.position = new Vector3(playerPosition.x, 0, playerPosition.z);
    //     
    //     
    // }
}
