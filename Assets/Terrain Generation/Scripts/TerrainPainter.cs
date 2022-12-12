using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainPainter : MonoBehaviour
{

    private TerrainLoader.SplatInfo[] splatInfo;

    public void PaintTerrain(TerrainData terrainData, float[,] heightMap, float[,] moistureMap)
    {
        splatInfo = TerrainLoader.Instance.splatInfo;
        float[, ,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

        var zOffset = transform.position.z;
        var roadwidth = 20;
        
        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int z = 0; z < terrainData.alphamapWidth; z++)
            {
                float[] splat = new float[splatInfo.Length];
                
                if (zOffset + z >= (255 - roadwidth) && zOffset + z <= (255 + roadwidth))
                {
                    splat[0] = 1f;
                }
                else
                {
                    var height = heightMap[z,y];
                    var moisture = moistureMap[y, z];

                    for (int i = 1; i < splatInfo.Length; i++)
                    {
                        if (height >= splatInfo[i].heightStart &&
                            height <= splatInfo[i].heightEnd &&
                            moisture >= splatInfo[i].moistureStart && 
                            moisture <= splatInfo[i].moistureEnd)
                        {
                            splat[i] = 1f;
                        }
                    }
                }
                
                for (int i = 0; i < splatInfo.Length; i++)
                {
                    splatmapData[z, y, i] = splat[i];
                }
            }
        }
        
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }
}
