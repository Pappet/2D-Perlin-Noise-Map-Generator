using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{

    TileType type;
    int x;
    int y;
    float elevation = 0f;

    //Tile Constructor with position and sprite
    public Tile(int _x, int _y, float elevation)
    {
        this.x = _x;
        this.y = _y;
        this.elevation = elevation;
    }

    public Vector2 GetTilePosition()
    {
        return new Vector2(x, y);
    }
    public float GetTileElevation()
    {
        return elevation;
    }
    public void SetTileElevation(float evelation)
    {
        this.elevation = evelation;
    }
}
