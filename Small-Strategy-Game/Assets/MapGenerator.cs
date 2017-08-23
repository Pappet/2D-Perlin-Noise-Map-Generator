using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    public int MapWidth;
    public int MapHeight;
    float Seed = 334;
    float Scale = 2;
    public Text seedText;
    public Text tilesText;
    Map map;
    GameObject[,] goTiles;

    // Use this for initialization
    void Awake()
    {
        goTiles = new GameObject[MapWidth, MapHeight];
        GenerateNewMap();
        GenerateTerrain();
        ChangeSprites();
    }

    void Update()
    {
        seedText.text = "Seed: " + Seed.ToString();
        tilesText.text = "Tiles: " + transform.childCount;
    }

    public void ChangeSprites()
    {
        for (int x = 0; x < map.GetWidth(); x++)
        {
            for (int y = 0; y < map.GetHeight(); y++)
            {
                goTiles[x, y].GetComponent<SpriteRenderer>().sprite = SetSprite(map.GetTile(x, y));
            }
        }
    }

    void GenerateNewMap()
    {
        map = new Map(MapWidth, MapHeight);
        for (int x = 0; x < map.GetWidth(); x++)
        {
            for (int y = 0; y < map.GetHeight(); y++)
            {
                GameObject g = new GameObject();
                g.AddComponent<SpriteRenderer>();
                g.transform.position = map.GetTile(x, y).GetTilePosition();
                g.transform.SetParent(this.transform);
                g.name = "Tile: " + x + "/ " + y;
                goTiles[x, y] = g;
            }
        }
    }

    public void GenerateTerrain()
    {
        for (int x = 0; x < map.GetWidth(); x++)
        {
            for (int y = 0; y < map.GetHeight(); y++)
            {
                Tile t = map.GetTile(x, y);
                //t.SetTileElevation(GeneratePerlinMix(x, y, 0, 0.125f));
                //t.SetTileTemperature(GeneratePerlinMix(x, y, 0, 0.125f));
                t.SetTileHumidity(GeneratePerlinMix(x, y, 5, 0.25f));
            }
        }
    }

    float GeneratePerlin(float x, float y)
    {
        float xCoord = (float)(x / MapWidth) * Scale + Seed;
        float yCoord = (float)(y / MapHeight) * Scale + Seed;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    float GeneratePerlinMix(float x, float y, float SeedOffset, float Intensity)
    {
        float xCoord = (float)(x / MapWidth - 0.5f) * Scale + Seed + SeedOffset;
        float yCoord = (float)(y / MapHeight - 0.5f) * Scale + Seed + SeedOffset;
        float p1 = Mathf.PerlinNoise(xCoord, yCoord);

        float xCoord2 = (float)(x / MapWidth - 0.5f) * 5 * Scale + Seed + SeedOffset;
        float yCoord2 = (float)(y / MapHeight - 0.5f) * 5 * Scale + Seed + SeedOffset;
        float p2 = Mathf.PerlinNoise(xCoord2, yCoord2);

        float xCoord3 = (float)(x / MapWidth - 0.5f) * 10 * Scale + Seed + SeedOffset;
        float yCoord3 = (float)(y / MapHeight - 0.5f) * 10 * Scale + Seed + SeedOffset;
        float p3 = Mathf.PerlinNoise(xCoord3, yCoord3);

        return p1 + (p2 * Intensity) + (p3 * (Intensity/2));
    }

    Sprite SetSprite(Tile tile)
    {
        Sprite s = sprites[0];
        float e = tile.GetTileElevation();
        float h = tile.GetTileHumidity();
        float t = tile.GetTileTemperature();

        if (e > 0.8f)
        {
            if (t < 0.3f)
                return sprites[8];

            return sprites[7];
        }
        else if (e >= 0.75f)
        {
            return sprites[6];
        }
        else if (e >= 0.45f)
        {
            if (h > 0.5f && t > 0.5f)
            {
                return sprites[5];
            }
            else if (t > 0.5f && h < 0.4f)
            {
                return sprites[3];
            }
            return sprites[4];

        }
        else if (e >= 0.4f)
        {
            return sprites[2];
        }
        else if (e >= 0.35f)
        {
            return sprites[1];
        }
        return s;
    }
    public void ChangeSeed()
    {
        Seed = Random.Range(0, 999);
    }
}

/*

			map = new Map(MapWidth,MapHeight);
			for(int x = 0; x < map.GetWidth(); x++){
				for(int y = 0; y < map.GetHeight(); y++){
					map.SetTile(x,y,new Tile(x,y,GeneratePerlin(x,y)));
					GameObject g = new GameObject();
					g.AddComponent<SpriteRenderer>();
					g.GetComponent<SpriteRenderer>().sprite = sprites[(int)map.GetTile(x,y).GetTileType()];
					g.transform.position = map.GetTile(x,y).GetTilePosition();
					g.transform.SetParent(this.transform);
					g.name = "Tile: " + x + "/ " + y;				
				}
			}
		}
		else{
			map = new Map(MapWidth,MapHeight);		
			for(int x = 0; x < map.GetWidth(); x++){
				for(int y = 0; y < map.GetHeight(); y++){
					map.SetTile(x,y,new Tile(x,y,GeneratePerlin(x,y)));
					GameObject g = new GameObject();
					g.AddComponent<SpriteRenderer>();
					g.GetComponent<SpriteRenderer>().sprite = sprites[(int)map.GetTile(x,y).GetTileType()];
					g.transform.position = map.GetTile(x,y).GetTilePosition();
					g.transform.SetParent(this.transform);
					g.name = "Tile: " + x + "/ " + y;				
				}
			}	
		}

		*/
