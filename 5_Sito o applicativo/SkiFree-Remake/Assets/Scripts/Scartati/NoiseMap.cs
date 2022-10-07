using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseMap
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale)
    {
        //creazione mappa
        float[,] noiseMap = new float[mapWidth, mapHeight];

        //loop per gli elementi della mappa
        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                //calcola le posizioni di sample
                float sampleX = x / scale;
                float sampleY = y / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }
        return noiseMap;
    }
}
