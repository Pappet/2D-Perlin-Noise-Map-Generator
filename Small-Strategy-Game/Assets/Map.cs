using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

	int width;
	int height;
	Tile[,] tiles;
	

	// Use this for initialization
	public Map(int width, int height) {
		this.width = width;
		this.height = height;
		tiles = new Tile[width,height];
	}

	public Tile GetTile(int x, int y){
		return tiles[x,y];
	}
	public void SetTile(int x, int y, Tile tile){
		tiles[x,y] = tile;
	}

	public int GetWidth(){
		return width;
	}
	public int GetHeight(){
		return height;
	}
	
}
