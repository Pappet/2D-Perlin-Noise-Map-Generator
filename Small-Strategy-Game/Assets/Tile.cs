using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

	TileType type;
	int x;
	int y;

	//Tile Constructor with position and sprite
	public Tile(int _x, int _y, TileType type)
	{
		this.x = _x;
		this.y = _y;
		this.type = type;
	}

	public Vector2 GetTilePosition(){
		return new Vector2 (x,y);
	}

	public TileType GetTileType(){
		return type;
	}
}
