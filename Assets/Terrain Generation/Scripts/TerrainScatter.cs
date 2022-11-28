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
        
        var xOffset = transform.position.x;
        var zOffset = transform.position.z;
        var roadwidth = TerrainLoader.Instance.roadwidth;

        for (int i = 0; i < treeCount; i++)
        {
            for (int j = 0; j < treeCount; j++)
            {
                var potentialPosition = new Vector3((i + Random.Range(-maxOffset, maxOffset)) / treeCount, 0, (j + Random.Range(-maxOffset, maxOffset)) / treeCount);
                
                if (!(zOffset + (potentialPosition.z * 513) > (256 - roadwidth) && zOffset + (potentialPosition.z * 513) < (256 + roadwidth)))
                {
                    TreeInstance treeInstance = new TreeInstance();
                    treeInstance.prototypeIndex = Random.Range(0, 3);
                    treeInstance.color = new Color32(255, 255, 255, 255);
                    treeInstance.heightScale = 1;
                    treeInstance.position = potentialPosition;
                    treeInstance.widthScale = 1;
                    terrain.AddTreeInstance(treeInstance);
                }
            }
        }
    }
}
