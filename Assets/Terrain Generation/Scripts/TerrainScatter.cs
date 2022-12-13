using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainScatter : MonoBehaviour
{
    private TerrainLoader.SplatInfo[] splatInfo;
    
    public GameObject[] scatter;

    [SerializeField] private int treeCount;
    [Range(-1, 1)] [SerializeField] private float maxOffset;

    [System.Serializable]
    public class TreeSplat
    {
        public int[] trees;
    }
    [SerializeField] public TreeSplat[] treeSplats;
    [SerializeField] private int[] grassBiomes;
    
    [SerializeField] private int grassDensity;
    [SerializeField] private int patchDetail;
    private float xOffset;
    private float zOffset;
    private float roadWidth;
    private Terrain terrain;
    public void ScatterFoliage(Terrain passedTerrain, int[,] biomeMap)
    {
        splatInfo = TerrainLoader.Instance.splatInfo;

        terrain = passedTerrain;
        xOffset = transform.position.x;
        zOffset = transform.position.z;
        roadWidth = TerrainLoader.Instance.roadwidth;
        ScatterGrass(biomeMap);
        ScatterTrees(biomeMap);
    }

    void ScatterGrass(int[,] biomeMap)
    {
        terrain.terrainData.SetDetailResolution(grassDensity, patchDetail);
  
        int[,] newMap = new int[grassDensity, grassDensity];

        for (int i = 0; i < grassDensity; i++)
        {
            for (int j = 0; j < grassDensity; j++)
            {
                var biome = biomeMap[i, j];
                for (int k = 0; k < grassBiomes.Length; k++)
                {
                    if (biome == grassBiomes[k])
                    {
                        newMap[i, j] = k;
                    }
                }
            }
        }
        
        terrain.terrainData.SetDetailLayer(0, 0, 0, newMap);
    }

    void ScatterTrees(int[,] biomeMap)
    {
        for (int i = 0; i < treeCount; i++)
        {
            for (int j = 0; j < treeCount; j++)
            {
                Vector3 potentialPosition = new Vector3((i + Random.Range(-maxOffset, maxOffset)) / treeCount, 0, (j + Random.Range(-maxOffset, maxOffset)) / treeCount);
                TestTree(potentialPosition, biomeMap);
            }
        }
    }

    void TestTree(Vector3 potentialPosition, int[,] biomeMap)
    {
        Vector3Int potentialPositionInt = Vector3Int.FloorToInt(potentialPosition * 513);

        // If position is off terrain don't place
        if (potentialPositionInt.x < 0 || potentialPositionInt.z < 0 ||
            potentialPositionInt.x > 513 || potentialPositionInt.z > 513)
            return;

        float biome = biomeMap[potentialPositionInt.z, potentialPositionInt.x];

        for (int i = 0; i < treeSplats.Length; i++)
        {
            if (i == biome && treeSplats[i].trees.Length > 0)
            {
                PlaceTree(potentialPosition, treeSplats[i].trees);
                return;
            }
        }
    }

    void PlaceTree(Vector3 position, int[] trees)
    {
        TreeInstance treeInstance = new TreeInstance();
        treeInstance.prototypeIndex = trees[Random.Range(0, trees.Length)];

        treeInstance.heightScale = 1;
        treeInstance.widthScale = 1;
        treeInstance.color = new Color32(255, 255, 255, 255);
        treeInstance.position = position;
        
        terrain.AddTreeInstance(treeInstance);
    }
}
