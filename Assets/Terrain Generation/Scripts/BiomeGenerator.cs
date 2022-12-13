using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeGenerator : MonoBehaviour
{
    private TerrainLoader.BiomeInfo[] biomeInfo;
    
    private float[,] heightMap;
    private float[,] moistureMap;
    private Vector3 position;
    
    public int[,] GenerateBiomes(float[,] heightMap, float[,] moistureMap)
    {
        biomeInfo = TerrainLoader.Instance.biomeInfo;
        
        this.heightMap = heightMap;
        this.moistureMap = moistureMap;
        position = transform.position;

        int length = 513;
        int[,] biomeMap = new int[length, length];
        
        for (int x = 0; x < length; x++)
        {
            for (int z = 0; z < length; z++)
            {
                biomeMap[x,z] = FindBiome(x,z);
            }
        }

        return biomeMap;
    }

    public int FindBiome(int x, int z)
    {
        int center = 256;
        int roadWidth = 20;
        float height = heightMap[x, z] * 513;
        float moisture = moistureMap[x, z];

        if (position.z / 512 == 0)
        {
            if (x > center - roadWidth &&
                x < center + roadWidth)
                return 0;
        }

        for (int i = 1; i < biomeInfo.Length; i++)
        {
            if (height < biomeInfo[i].maxHeight && height > biomeInfo[i-1].maxHeight)
            {
                for (int j = 1; j < biomeInfo[i].moistureInfo.Length; j++)
                {
                    if (moisture < biomeInfo[i].moistureInfo[j].moistureLevel && moisture > biomeInfo[i].moistureInfo[j-1].moistureLevel)
                    {
                        biomeInfo[i].moistureInfo[j].biomeIndex = i * 3 + j - 3;
                        return biomeInfo[i].moistureInfo[j].biomeIndex;
                    }
                }
            }
                
        }

        return -1;
    }
}
