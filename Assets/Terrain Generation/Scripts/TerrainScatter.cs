using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScatter : MonoBehaviour
{
    private TerrainLoader.SplatInfo[] splatInfo;
    
    public GameObject[] scatter;

    [SerializeField] private int treeCount;
    [Range(-1, 1)] [SerializeField] private float maxOffset;

    [SerializeField] private int grassDensity;
    [SerializeField] private int patchDetail;
    private float xOffset;
    private float zOffset;
    private float roadWidth;
    private Terrain terrain;
    public void ScatterFoliage(Terrain passedTerrain, float[,] heightMap, float[,] moistureMap)
    {
        splatInfo = TerrainLoader.Instance.splatInfo;

        terrain = passedTerrain;
        xOffset = transform.position.x;
        zOffset = transform.position.z;
        roadWidth = TerrainLoader.Instance.roadwidth;
        ScatterGrass(heightMap, moistureMap);
        ScatterTrees(heightMap, moistureMap);
    }

    void ScatterGrass(float[,] heightMap, float[,] moistureMap)
    {
        terrain.terrainData.SetDetailResolution(grassDensity, patchDetail);
  
        int[,] newMap = new int[grassDensity, grassDensity];
        
        // for (int s = 0; s < splatHeights.Length; s++)
        // {
            for (int i = 0; i < grassDensity; i++)
            {
                for (int j = 0; j < grassDensity; j++)
                {
                    Vector3 potentialPosition = new Vector3(j, 0, i);
                    newMap[i, j] = TestGrass(potentialPosition, heightMap, moistureMap);
                }
            }
        // }
        terrain.terrainData.SetDetailLayer(0, 0, 0, newMap);
    }

    int TestGrass(Vector3 potentialPosition, float[,] heightMap, float[,] moistureMap)
    {
        if (zOffset + potentialPosition.z > 256 - roadWidth &&
            zOffset + potentialPosition.z < 256 + roadWidth)
            return 0;

        return 5;
        // Sample the height at this location (note GetHeight expects int coordinates corresponding to locations in the heightmap array)
        // float height = terrain.terrainData.GetHeight(j, i);
        // if (height < splatInfo[3].heightStart && height>splatInfo[2].heightStart) 
            // else 
            // newMap[i, j] = 0;
        
    }

    void ScatterTrees(float[,] heightMap, float[,] moistureMap)
    {
        for (int i = 0; i < treeCount; i++)
        {
            for (int j = 0; j < treeCount; j++)
            {
                Vector3 potentialPosition = new Vector3((i + Random.Range(-maxOffset, maxOffset)) / treeCount, 0, (j + Random.Range(-maxOffset, maxOffset)) / treeCount);
                TestTree(potentialPosition, heightMap, moistureMap);
            }
        }
    }

    void TestTree(Vector3 potentialPosition, float[,] heightMap, float[,] moistureMap)
    {
        Vector3Int potentialPositionInt = Vector3Int.FloorToInt(potentialPosition * 513);

        // If position is off terrain don't place
        if (potentialPositionInt.x < 0 || potentialPositionInt.z < 0 ||
            potentialPositionInt.x > 513 || potentialPositionInt.z > 513)
            return;
        // If position is on road don't place
        if (zOffset + (potentialPosition.z * 513) > (256 - roadWidth) &&
            zOffset + (potentialPosition.z * 513) < (256 + roadWidth))
            return;

        float height = heightMap[ potentialPositionInt.z , potentialPositionInt.x ];
        if (height < 0.1)
            PlaceTree(potentialPosition, 0);
        else if (height > 0.1)
            PlaceTree(potentialPosition, 1);
    }

    void PlaceTree(Vector3 position, int type)
    {
        TreeInstance treeInstance = new TreeInstance();
        treeInstance.heightScale = 1;
        treeInstance.widthScale = 1;
        
        if (type == 0)
        {
            treeInstance.prototypeIndex = Random.Range(0, 3);
        }
        else if (type == 1)
        {
            treeInstance.prototypeIndex = Random.Range(3, 8);
        }
        
        treeInstance.color = new Color32(255, 255, 255, 255);
        treeInstance.position = position;
        
        terrain.AddTreeInstance(treeInstance);
    }
}
