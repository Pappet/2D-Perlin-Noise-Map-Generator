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
    float Scale = 3;
    public Text seedText;

    Map map;

    // Use this for initialization
    void Awake()
    {
        GenerateNewMap();
    }

    void Update()
    {
        seedText.text = "Seed: " + Seed.ToString();
    }

    public void GenerateNewMap()
    {
        map = new Map(MapWidth, MapHeight);

        for (int x = 0; x < map.GetWidth(); x++)
        {
            for (int y = 0; y < map.GetHeight(); y++)
            {
                Tile t = map.GetTile(x, y);
                t.SetTileElevation(GeneratePerlin(x, y));

                GameObject g = new GameObject();
                g.AddComponent<SpriteRenderer>();
                g.GetComponent<SpriteRenderer>().sprite = SetSprite(t);
                g.transform.position = map.GetTile(x, y).GetTilePosition();
                g.transform.SetParent(this.transform);
                g.name = "Tile: " + x + "/ " + y;
            }
        }

    }

    float GeneratePerlin(float x, float y)
    {
        float xCoord = (float)(x / MapWidth) * Scale + Seed;
        float yCoord = (float)(y / MapHeight) * Scale + Seed;
        return Mathf.PerlinNoise(xCoord, yCoord);

    }

    Sprite SetSprite(Tile tile)
    {
        Sprite s = sprites[0];
        float e = tile.GetTileElevation();

        if (e > 0.8f)
        {
            s = sprites[7];
        }
        else if (e >= 0.7f)
        {
            s = sprites[6];
        }
        else if (e >= 0.5f)
        {
            s = sprites[5];
        }
        else if (e >= 0.4f)
        {
            s = sprites[4];
        }
        else if (e >= 0.35f)
        {
            s = sprites[3];
        }
        else if (e >= 0.3f)
        {
            s = sprites[2];
        }
        else if (e >= 0.25f)
        {
            s = sprites[1];
        }
        return s;
    }
    public void ChangeSeed()
    {
        Seed = Random.Range(0, 999);
    }
}

/*
if(map != null){
			Debug.Log("There exist a Map!");
			foreach(Transform child in this.transform){
				GameObject.Destroy(child.gameObject);
				Debug.Log("Destoyed Child");
			}
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
