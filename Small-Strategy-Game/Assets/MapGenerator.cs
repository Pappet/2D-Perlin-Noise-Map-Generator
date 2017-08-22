using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour {
	public List<Sprite>sprites = new List<Sprite>();
	public int MapWidth;
	public int MapHeight;
	float Seed;
	float Scale = 3;

	public Text seedText;
	Map map;
	// Use this for initialization
	void Awake () {
		GenerateNewMap();
	}

	void Update(){
		seedText.text = "Seed: " + Seed.ToString();
	}
	
	public void GenerateNewMap(){
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
	}

	TileType GeneratePerlin(float x, float y){
		TileType t = TileType.WATER_DARK;
		float xCoord = (float)((x / MapWidth) * Scale + Seed) ;
		float yCoord = (float)((y / MapHeight) * Scale + Seed) ;
		float perlinMap = Mathf.PerlinNoise(xCoord, yCoord) ;
		//Debug.Log(perlinMap);

		if(perlinMap > 0.8f){
			t = TileType.STONE_DARK;
		}
		else if(perlinMap >= 0.7f){
			t = TileType.STONE;
		}
		else if(perlinMap >= 0.5f){
			t = TileType.GRASS_DARK;
		}
		else if(perlinMap >= 0.4f){
			t = TileType.GRASS;
		}
		else if(perlinMap >= 0.35f){
			t = TileType.SAND_DARK;
		}
		else if(perlinMap >= 0.3f){
			t = TileType.SAND;
		}
		else if(perlinMap >= 0.25f){
			t = TileType.WATER;
		}
		return t;
	}

	public void ChangeSeed(){
		Seed = Random.Range(0,999);
	}
}
