using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public static class NoiseGenerator
{
    /*public static float[,] GenerateNoise (int width, int height, int seed, float scale, int octaves, float persistence, float lacunarity, Vector2Int offset)
    {
        float[,] noise = new float[width, height];
        System.Random rand = new System.Random(seed);

        Vector2[] octavesOffset = new Vector2[octaves];

        for(int i = 0; i < octaves; i++)
        {
            float xOffset = rand.Next(-100000, 1000000) + offset.x * (width / scale);
            float yOffset = rand.Next(-100000, 1000000) + offset.y * (height / scale);

            octavesOffset[i] = new Vector2(xOffset / width, yOffset / height);

        }

        if (scale < 0) scale = 0.0001f;

        float halfWidth = width / 2.0f;
        float halfHeight = height / 2.0f;

        for(int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                float superpositionCompensation =0;

                for(int i = 0; i < octaves; i++)
                {
                    float xResult = (x - halfWidth) / scale * frequency + octavesOffset[i].x * frequency;
                    float yResult = (y - halfHeight) / scale * frequency + octavesOffset[i].y * frequency;

                    float generateValue = Mathf.PerlinNoise(xResult, yResult);

                    noiseHeight += generateValue * amplitude;
                    noiseHeight -= superpositionCompensation;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                    superpositionCompensation = amplitude / 2;
                }
                noise[x, y] = Mathf.Clamp01(noiseHeight);
            }
        }
        return noise;
    }
    
}

public class GenerateMap : MonoBehaviour
{
    [SerializeField] private SpriteTileMode tile;
    [SerializeField] private Tilemap[] tilemap;
    private int idxTilemap = 0;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [Range(0, 50)][SerializeField] private float panSpeed = 5.0f;
    private Vector3 panMovement;
    private Vector2Int posCam;
    private Vector3 direct;

    [Header("Noise Configuration")]
    [SerializeField] private int seed = 100;
    [Range(1, 100)][SerializeField] private float scale = 30.0f;
    [Range(1, 5)][SerializeField] private int octaves = 1;
    [Range(0, 1)][SerializeField] private float persistence = 0.5f;
    [SerializeField] private float lacunarity = 1.0f;


    private struct ColorLevel
    {
        public float height;
        public Color color;
    }

    //Terrain Color Configuration
    [SerializeField] private List<ColorLevel> ColorMap = new List<ColorLevel>();

    private int noiseDimensionalX = 150;
    private int noiseDimensionalY = 100;
    private float[,] noiseMap;

    void TilemapInit()
    {
        noiseMap = new float[noiseDimensionalX, noiseDimensionalY];

        posCam = new Vector2Int( (int) cam.transform.position.x, (int) cam.transform.position.y );
        noiseMap = NoiseGenerator.GenerateNoise(noiseDimensionalX, noiseDimensionalY, seed, scale, octaves, persistence, lacunarity, posCam);

        for(int y = -(noiseDimensionalY / 2); y < (noiseDimensionalY/2); y++)
        {
            for(int x = -(noiseDimensionalX / 2); x < (noiseDimensionalX / 2); x++)
            {
                tilemap[0].SetTile(new Vector3Int(x, y, 0), tile);
                tilemap[1].SetTile(new Vector3Int(x, y, 0), tile);

                tilemap[0].SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
                tilemap[1].SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);

                tilemap[0].SetColor(new Vector3Int(x, y, 0), TileFlags.None);

                foreach(var level in ColorMap)
                {
                    if (noiseMap[x+(noiseDimensionalX/2), y+(noiseDimensionalY/2)] < level.height)
                    {
                        tilemap[0].SetColor(new Vector3Int(x, y, 0), level.color);
                        break;
                    }
                }
            }
        }
        tilemap[0].gameObject.SetActive(true);
    }
    
    void Awake()
    {
        if(ColorMap.Count == 0)
        {
            ColorLevel monoTerrain;
            monoTerrain.height = 0.5f;
            monoTerrain.color = new Color(0.5f, 0.5f, 0.5f);
            ColorMap.Add(monoTerrain);
        }

        direct = Vector3.up;

        TilemapInit();
        StartCoroutine(GenerateTileMap());
    }

    void Update()
    {
        panMovement = new Vector3(0,0, -10);

        if (Input.GetKey(KeyCode.S)) direct = Vector3.down;
        if (Input.GetKey(KeyCode.W)) direct = Vector3.up;
        if (Input.GetKey(KeyCode.A)) direct = Vector3.left;
        if (Input.GetKey(KeyCode.D)) direct = Vector3.right;

        panMovement += direct * panSpeed * Time.deltaTime;
        cam.transform.Translate(new Vector3(panMovement.x, panMovement.y, 0), Space.World);
        if(Math.Abs((posCam.x - cam.transform.position.x)>panSpeed-1 || Math.Abs(posCam.y - cam.transform.position.y) > panSpeed - 1))
        {
            StartCoroutine(GenerateTileMap());
        }
    }

    IEnumerator GenerateTileMap()
    {
        posCam = new Vector2Int((int)cam.transform.position.x, (int)cam.transform.position.y);
        noiseMap = NoiseGenerator.GenerateNoise(noiseDimensionalX, noiseDimensionalY, seed, scale, octaves, persistence, lacunarity, posCam);

        idxTilemap = idxTilemap == 0 ? 1 : 0;
        tilemap[idxTilemap].gameObject.SetActive(false);

        for (int y = -(noiseDimensionalY / 2); y < (noiseDimensionalY / 2); y++)
        {
            for (int x = -(noiseDimensionalX / 2); x < (noiseDimensionalX / 2); x++)
            {
                tilemap[idxTilemap].SetColor(new Vector3(x, y, 0), ColorMap[ColorMap.Count - 1].color);

                foreach (var level in ColorMap)
                {
                    if (noiseMap[x + (noiseDimensionalX / 2), y + (noiseDimensionalY / 2)] < level.height)
                    {
                        tilemap[idxTilemap].SetColor(new Vector3Int(x, y, 0), level.color);
                        break;
                    }
                }
            }
            tilemap[idxTilemap].gameObject.transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y);
            yield return null;
        }
        tilemap[idxTilemap].gameObject.SetActive(true);
    }*/
}
