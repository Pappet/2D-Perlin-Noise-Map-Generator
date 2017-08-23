using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{

    TileType type;
    int x;
    int y;
    float elevation = 0f;
    float temperature = 0f;
    float humidity = 0;

    //Tile Constructor with position and sprite
    public Tile(int _x, int _y, float elevation, float temperature, float humidity)
    {
        this.x = _x;
        this.y = _y;
        this.elevation = elevation;
        this.temperature = temperature;
        this.humidity = humidity;
    }

    public Vector2 GetTilePosition()
    {
        return new Vector2(x, y);
    }
    public float GetTileElevation()
    {
        return elevation;
    }
    public float GetTileTemperature()
    {
        return temperature;
    }
    public float GetTileHumidity()
    {
        return humidity;
    }
    public void SetTileElevation(float evelation)
    {
        this.elevation = evelation;
    }
    public void SetTileTemperature(float temperature)
    {
        this.temperature = temperature;
    }
    public void SetTileHumidity(float humidity)
    {
        this.humidity = humidity;
    }
}
