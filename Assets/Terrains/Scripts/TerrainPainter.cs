using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainPainter : MonoBehaviour
{
    
    [System.Serializable]
    public class SplatHeights
    {
        public int textureIndex;
        public int startingHeight;
    }

    public SplatHeights[] splatHeights;
    public void PaintTerrain(TerrainData terrainData)
    {
        // TerrainData terrainData = Terrain.activeTerrain.terrainData;
        float[, ,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                float terrainHeight = terrainData.GetHeight(y,x);

                float[] splat = new float[splatHeights.Length];

                for (int i = 0; i < splatHeights.Length; i++)
                {
                    if (i + 1 < splatHeights.Length)
                    {
                        if (terrainHeight >= splatHeights[i].startingHeight && 
                            terrainHeight <= splatHeights[i+1].startingHeight)
                        {
                            splat[i] = 1f;
                        }
                    }
                    else
                    {
                        if (terrainHeight >= splatHeights[i].startingHeight)
                        {
                            splat[i] = 1f;
                        }
                    }
                    

                }

                for (int i = 0; i < splatHeights.Length; i++)
                {
                    splatmapData[x, y, i] = splat[i];
                }
            }
        }
        
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }
}
