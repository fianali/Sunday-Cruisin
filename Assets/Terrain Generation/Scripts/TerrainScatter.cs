using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScatter : MonoBehaviour
{
    private TerrainLoader.SplatHeights[] splatHeights;
    
    public GameObject[] scatter;

    [SerializeField] private int treeCount;
    [Range(-1, 1)] [SerializeField] private float maxOffset;

    [SerializeField] private int grassDensity;
    [SerializeField] private int patchDetail;
    private float xOffset;
    private float zOffset;
    private float roadWidth;
    private Terrain terrain;
    public void ScatterFoliage(Terrain passedTerrain)
    {
        splatHeights = TerrainLoader.Instance.biomeHeights;

        terrain = passedTerrain;
        xOffset = transform.position.x;
        zOffset = transform.position.z;
        roadWidth = TerrainLoader.Instance.roadwidth;
        ScatterGrass();
        ScatterTrees();
    }

    void ScatterGrass()
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

                    if (!(zOffset + (potentialPosition.z) > (256 - roadWidth) &&
                          zOffset + (potentialPosition.z) < (256 + roadWidth)))
                    {
                        // Sample the height at this location (note GetHeight expects int coordinates corresponding to locations in the heightmap array)
                        float height = terrain.terrainData.GetHeight(j, i);
                        if (height < splatHeights[3].startingHeight && height>splatHeights[2].startingHeight) newMap[i, j] = 5;
                        else newMap[i, j] = 0;
                    }
                    else newMap[i, j] = 0;
                }
            }
            terrain.terrainData.SetDetailLayer(0, 0, 0, newMap);
        // }
    }

    void ScatterTrees()
    {


        for (int i = 0; i < treeCount; i++)
        {
            for (int j = 0; j < treeCount; j++)
            {
                Vector3 potentialPosition = new Vector3((i + Random.Range(-maxOffset, maxOffset)) / treeCount, 0, (j + Random.Range(-maxOffset, maxOffset)) / treeCount);
                
                if (!(zOffset + (potentialPosition.z * 513) > (256 - roadWidth) && 
                      zOffset + (potentialPosition.z * 513) < (256 + roadWidth)))
                {
                    //Debug.Log((int) (potentialPosition.z*512));
                    //Debug.Log((int) (potentialPosition.x*512));

                    float height = terrain.terrainData.GetHeight((int) (potentialPosition.x*513),(int) (potentialPosition.z*513));
                    //Debug.Log(height);
                    if (height < splatHeights[3].startingHeight && height>splatHeights[2].startingHeight) PlaceTree(potentialPosition);
                    
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
