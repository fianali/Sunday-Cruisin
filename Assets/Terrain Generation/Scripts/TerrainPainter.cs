using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainPainter : MonoBehaviour
{  
    private TerrainLoader.BiomeInfo[] biomeInfo;

    public void PaintTerrain(TerrainData terrainData, int[,] biomeMap)
    {
        biomeInfo = TerrainLoader.Instance.biomeInfo;

        float[, ,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];
        
        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int z = 0; z < terrainData.alphamapWidth; z++)
            {
                float[] splat = new float[terrainData.alphamapLayers];

                int biome = biomeMap[z,y];
                for (int i = 1; i < biomeInfo.Length; i++)
                {
                    for (int j = 1; j < 4; j++)
                    {
                        if (biome == 0)
                        {
                            splat[0] = 1;
                        }
                        else if (biome == biomeInfo[i].moistureInfo[j].biomeIndex)
                        {
                            splat[biomeInfo[i].moistureInfo[j].splat] = 1f;
                        }
                    }
                }
                for (int i = 0; i < splat.Length; i++)
                {
                    splatmapData[z, y, i] = splat[i];
                }
            }
        }
        
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }
}
