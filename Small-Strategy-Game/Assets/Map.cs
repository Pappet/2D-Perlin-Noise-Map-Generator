using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{

    int width;
    int height;
    Tile[,] tiles;


    // Use this for initialization
    public Map(int width, int height)
    {
        this.width = width;
        this.height = height;
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(x, y, 0f, 0f);
            }
        }
    }

    public Tile GetTile(int x, int y)
    {
        return tiles[x, y];
    }

    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }

}
