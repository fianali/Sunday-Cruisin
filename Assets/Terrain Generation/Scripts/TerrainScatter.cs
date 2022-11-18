using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScatter : MonoBehaviour
{
    public GameObject[] scatter;

    [SerializeField] private int treeCount;
    [Range(-1, 1)] [SerializeField] private float maxOffset; 
    public void ScatterFoliage(Terrain terrain)
    {
        
        
        int grassDensity = 100;
        int patchDetail = 100;
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
        
        for (int i = 0; i < treeCount; i++)
        {
            for (int j = 0; j < treeCount; j++)
            {
                TreeInstance treeInstance = new TreeInstance();
                treeInstance.prototypeIndex = Random.Range(0, 6);
                treeInstance.color = new Color32(255, 255, 255, 255);
                treeInstance.heightScale = 10000;
                treeInstance.position = new Vector3((i + Random.Range(-maxOffset, maxOffset)) / treeCount, 0, (j + Random.Range(-maxOffset, maxOffset)) / treeCount);
                treeInstance.widthScale = 10000;
                terrain.AddTreeInstance(treeInstance);
            }
        }
    }
}
