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

    private int xPlayerCell;
    private int zPlayerCell;
    
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
        // for (var i = 0; i < loadDistance; i++)
        // {
        //     for (var j = 0; j < loadDistance; j++)
        //     {
        //         Instantiate(terrain, new Vector3(i*513f, 0, j*513f), new Quaternion(0,0,0,0));
        //     }
        // }
        for (var x = 0 - loadDistance; x <= 0 + loadDistance; x++)
        {
            for (var z = 0 - loadDistance; z <= 0 + loadDistance; z++)
            {
                Instantiate(terrain, new Vector3(x*513f, 0, z*513f), new Quaternion(0,0,0,0));
            }
        }
    }

    private void Update()
    {
        var lastXCell = xPlayerCell;
        var lastZCell = zPlayerCell;
        xPlayerCell = (int) MathF.Truncate(transform.position.x / 512);
        zPlayerCell = (int) MathF.Truncate(transform.position.z / 512);
        Debug.Log("X cell: " + xPlayerCell + "Z cell: " + zPlayerCell);
        var deltaXCell = xPlayerCell - lastXCell;
        var deltaZCell = zPlayerCell - lastZCell;
        if (deltaXCell != 0)
        {
            LoadRow(deltaXCell);
        }
    }

    private void LoadRow(int deltaXCell)
    {
        for (var z = zPlayerCell - loadDistance; z <= zPlayerCell + loadDistance; z++)        
        {
            Instantiate(terrain, new Vector3((xPlayerCell + (loadDistance * deltaXCell)) * 513f, 0, z*513f), new Quaternion(0,0,0,0));
        }
    }
    private void LoadCellsAround(int xCell, int zCell)
    {
        for (var x = xCell - loadDistance; x <= xCell + loadDistance; x++)
        {
            for (var z = zCell - loadDistance; z <= zCell + loadDistance; z++)
            {
                Instantiate(terrain, new Vector3(x*513f, 0, z*513f), new Quaternion(0,0,0,0));
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
