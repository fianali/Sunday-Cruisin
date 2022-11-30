using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScatter : MonoBehaviour
{
    public GameObject[] scatter;

    [SerializeField] private int treeCount;
    [Range(-1, 1)] [SerializeField] private float maxOffset;

    [SerializeField] private int grassDensity;
    [SerializeField] private int patchDetail;

    private Terrain terrain;
    public void ScatterFoliage(Terrain passedTerrain)
    {
        terrain = passedTerrain;
        
        ScatterGrass();
        ScatterTrees();
    }

    void ScatterGrass()
    {
        terrain.terrainData.SetDetailResolution(grassDensity, patchDetail);
  
        int[,] newMap = new int[grassDensity, grassDensity];
  
        for (int i = 0; i < grassDensity; i++)
        {
            for (int j = 0; j < grassDensity; j++)
            {
                // Sample the height at this location (note GetHeight expects int coordinates corresponding to locations in the heightmap array)
                float height = terrain.terrainData.GetHeight( j, i );
                if (height < 300.0f)
                {
                    newMap[i, j] = 6;
                }
                else
                {
                    newMap[i, j] = 0;
                }
            }
        }
        terrain.terrainData.SetDetailLayer(0, 0, 0, newMap);
    }

    void ScatterTrees()
    {
        var xOffset = transform.position.x;
        var zOffset = transform.position.z;
        var roadwidth = TerrainLoader.Instance.roadwidth;

        for (int i = 0; i < treeCount; i++)
        {
            for (int j = 0; j < treeCount; j++)
            {
                Vector3 potentialPosition = new Vector3((i + Random.Range(-maxOffset, maxOffset)) / treeCount, 0, (j + Random.Range(-maxOffset, maxOffset)) / treeCount);
                
                if (!(zOffset + (potentialPosition.z * 513) > (256 - roadwidth) && zOffset + (potentialPosition.z * 513) < (256 + roadwidth)))
                {
                    PlaceTree(potentialPosition);
                }
            }
        }
    }

    void PlaceTree(Vector3 position)
    {
        TreeInstance treeInstance = new TreeInstance();
        treeInstance.prototypeIndex = Random.Range(0, 3);
        treeInstance.color = new Color32(255, 255, 255, 255);
        treeInstance.heightScale = 1;
        treeInstance.position = position;
        treeInstance.widthScale = 1;
        terrain.AddTreeInstance(treeInstance);
    }
}
