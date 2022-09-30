
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PerlinNoiseMap : MonoBehaviour
{
    Dictionary<int, GameObject> tileset;
    Dictionary<int, GameObject> tile_groups;

    public GameObject[] prefabs;

    float map_width = 0;
    float map_height = 0;

    List<List<int>> noise_grid = new List<List<int>>();
    List<List<GameObject>> tile_grid = new List<List<GameObject>>();

    float magnification = 0.0f;

    int x_offset = 0; // <- +>
    int y_offset = 0; // v- +^

    void Start()
    {
        map_width = Random.Range(160.0f, 1000.0f);
        map_height = map_width*2;
        magnification = Random.Range(5.0f, 25.0f);
        CreateTileset();
        CreateTileGroups();
        GenerateMap();
    }

    void CreateTileset()
    {
        /** Assegna degli id ad ogni oggetto. **/

        tileset = new Dictionary<int, GameObject>();
        for(int i = 0; i < prefabs.Length; i++)
        {
            tileset.Add(i, prefabs[i]);
        }
    }

    void CreateTileGroups()
    {
        /** Crea oggetti vuoti per contenere tutti gli ostacoli in modo ordinato **/

        tile_groups = new Dictionary<int, GameObject>();
        foreach (KeyValuePair<int, GameObject> prefab_pair in tileset)
        {
            GameObject tile_group = new GameObject(prefab_pair.Value.name);
            tile_group.transform.parent = gameObject.transform;
            tile_group.transform.localPosition = new Vector3(0, 0, 0);
            tile_groups.Add(prefab_pair.Key, tile_group);
        }
    }

    void GenerateMap()
    {
        /** Genera la griglia 2d con i vari ostacoli **/

        for (int x = 0; x < map_width; x++)
        {
            noise_grid.Add(new List<int>());
            tile_grid.Add(new List<GameObject>());

            for (int y = 0; y < map_height; y++)
            {
                if(Random.Range(0.0f, 1.0f) > 0.90f)
                {
                    int tile_id = GetIdUsingPerlin(x, y);
                    noise_grid[x].Add(tile_id);
                    CreateTile(tile_id, x, y);
                }
                
            }
        }
    }

    int GetIdUsingPerlin(int x, int y)
    {
        /** Using a grid coordinate input, generate a Perlin noise value to be
            converted into a tile ID code. Rescale the normalised Perlin value
            to the number of tiles available. **/

        float raw_perlin = Mathf.PerlinNoise(
            (x - x_offset) / magnification,
            (y - y_offset) / magnification
        );
        float clamp_perlin = Mathf.Clamp01(raw_perlin); 
        float scaled_perlin = clamp_perlin * tileset.Count;

        // tileset.Count così ogni volta che si aggiunge un oggetto si aggiorna automaticamente
        if (scaled_perlin == tileset.Count)
        {
            scaled_perlin = (tileset.Count - 1);
        }
        return Mathf.FloorToInt(scaled_perlin);
    }

    void CreateTile(int tile_id, int x, int y)
    {
        /** crea l'oggetto e lo istanzia **/

        GameObject tile_prefab = tileset[tile_id];
        GameObject tile_group = tile_groups[tile_id];
        GameObject tile = Instantiate(tile_prefab, tile_group.transform);

        tile.name = string.Format("tile_x{0}_y{1}", x, y);
        tile.transform.localPosition = new Vector3(x, y, 0);

        tile_grid[x].Add(tile);
    }
}