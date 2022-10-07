using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    //public RawImage debugImage;

    [Header("Dimensions")]
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public float[,] noiseMap;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        noiseMap = NoiseMap.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);

        /*Color[] pixels = new Color[mapWidth * mapHeight];
        int i = 0;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                pixels[i] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
                i++;
            }
        }

        Texture2D tex = new Texture2D(mapWidth, mapHeight);
        tex.SetPixels(pixels);
        tex.filterMode = FilterMode.Point;  //si vede un pixel come un quadrato
        tex.Apply();

        debugImage.texture = tex;

        // debug log
        //for (int x = 0; x < mapWidth; x++)
        //{
        //    for (int y = 0; y < mapHeight; y++)
        //    {
        //        Debug.Log(heightMap[x, y]);
        //    }
        //}*/
    }
}
