using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainPainter : MonoBehaviour
{
    [SerializeField] private int[] splatBiomes;

    public void PaintTerrain(TerrainData terrainData, int[,] biomeMap)
    {
        float[, ,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];
        
        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int z = 0; z < terrainData.alphamapWidth; z++)
            {
                float[] splat = new float[splatBiomes.Length];

                int biome = biomeMap[z,y];
                for (int i = 0; i < splatBiomes.Length; i++)
                {
                    if (biome == splatBiomes[i])
                    {
                        splat[i] = 1f;
                    }
                }
                
                for (int i = 0; i < splatBiomes.Length; i++)
                {
                    splatmapData[z, y, i] = splat[i];
                }
            }
        }
        
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }
}
