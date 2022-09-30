using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGeneratora /*: MonoBehaviour*/
{
    /*// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float[,] Generate(int width, int height, float scale, Wave[] waves, Vector2 offset)
    {
        float[,] noiseMap = new float[width, height];   //array che contiene la mappa

        //loop per accedere ad ogni elemento della mappa
        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                //calcola le posizioni da usare
                float samplePosX = (float)x * scale + offset.x;
                float samplePosY = (float)y * scale + offset.y;

                float normalization = 0.0f;

                //loop per ogni wave
                foreach (Wave wave in waves)
                {
                    noiseMap[x, y] += wave.amplitude * Mathf.PerlinNoise(samplePosX * wave.frequency + wave.seed, samplePosY * wave.frequency + wave.seed);
                    normalization += wave.amplitude;
                }
                noiseMap[x, y] /= normalization;
            }
        }

        return noiseMap;
    }
}

public class Wave
{
    public float seed;          //casuaità della mappa
    public float frequency;     //frequenza della mappa
    public float amplitude;     //intensità
}*/
}
