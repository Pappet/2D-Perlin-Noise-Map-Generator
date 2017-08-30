using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public Sprite DeepWaterSprite;
    public Sprite ShallowWaterSprite;
    public Sprite BeachSprite;
    public Sprite GrassSprite;
    public Sprite DesertSprite;
    public Sprite ForestSprite;
    public Sprite StoneSprite;
    public Sprite RockSprite;
    public Sprite SnowSprite;
    public Sprite BLANKSprite;

    public int MapWidth;
    public int MapHeight;
    int Seed = 334;
    float SeedOffset;
    float Frequency;
    float FrequencyModifier;
    float WaveLenghtModifier;
    float Redistribution;
    float SeaLevel;
    bool TerrainGenerationToggle = false;
    enum DrawMode { Terrain, Elevation, Humidity };
    DrawMode drawMode = DrawMode.Terrain;

    public Toggle ElevationToggle;
    public Toggle HumidityToggle;
    public Text seedText;
    public Text seedOffsetText;
    public Slider seedOffsetSlider;
    public Text redistributionText;
    public Slider redistributionSlider;
    public Text frequenzyText;
    public Slider frequenzySlider;
    public Text frequencyModifierText;
    public Slider frequencyModifierSlider;
    public Text WaveLenghtModifierText;
    public Slider WaveLenghtModifierSlider;
    public Text SeaLevelText;
    public Slider SeaLevelSlider;
    Map map;
    GameObject[,] goTiles;

    // Use this for initialization
    void Awake()
    {
        goTiles = new GameObject[MapWidth, MapHeight];
        GenerateNewMap();
        TerrainGenerationToggle = true;
    }

    void Update()
    {
        seedText.text = "Seed: " + Seed.ToString();

        SeedOffset = seedOffsetSlider.value;
        seedOffsetText.text = SeedOffset.ToString();

        Redistribution = redistributionSlider.value;
        redistributionText.text = Redistribution.ToString();

        Frequency = frequenzySlider.value;
        frequenzyText.text = Frequency.ToString();

        FrequencyModifier = frequencyModifierSlider.value;
        frequencyModifierText.text = FrequencyModifier.ToString();

        WaveLenghtModifier = WaveLenghtModifierSlider.value;
        WaveLenghtModifierText.text = WaveLenghtModifier.ToString();

        SeaLevel = SeaLevelSlider.value;
        SeaLevelText.text = SeaLevel.ToString();

        if (ElevationToggle.isOn && HumidityToggle.isOn)
        {
            drawMode = DrawMode.Terrain;
            ChangeSprites();
        }
        else if(ElevationToggle.isOn && !HumidityToggle.isOn){
            drawMode = DrawMode.Elevation;
            ChangeSprites();
        }
        else if(!ElevationToggle.isOn && HumidityToggle.isOn){
            drawMode = DrawMode.Humidity;
            ChangeSprites();
        }

        if(TerrainGenerationToggle){
            GenerateTerrain();
            ChangeSprites();
            TerrainGenerationToggle = false;
        }        
    }

    public void GenerateTerrain()
    {
        for (int x = 0; x < map.GetWidth(); x++)
        {
            for (int y = 0; y < map.GetHeight(); y++)
            {
                Tile t = map.GetTile(x, y);
                //t.SetTileElevation(GeneratePerlin(x, y));
                t.SetTileElevation(GeneratePerlinMix(x, y, SeedOffset, FrequencyModifier));
                
                t.SetTileHumidity(GeneratePerlinMix(x, y, SeedOffset * 2, FrequencyModifier * 2));
            }
        }        
    }

    public void ChangeSprites()
    {
        for (int x = 0; x < map.GetWidth(); x++)
        {
            for (int y = 0; y < map.GetHeight(); y++)
            {
                if (drawMode == DrawMode.Terrain)
                {                    
                    goTiles[x, y].GetComponent<SpriteRenderer>().sprite = SetTerrainSprites(map.GetTile(x, y));
                    goTiles[x, y].GetComponent<SpriteRenderer>().color = Color.white;
                }
                else if (drawMode == DrawMode.Elevation)
                {
                    goTiles[x, y].GetComponent<SpriteRenderer>().sprite = BLANKSprite;
                    goTiles[x, y].GetComponent<SpriteRenderer>().color = SetTileColor(map.GetTile(x, y),map.GetTile(x, y).GetTileElevation(),Color.white,Color.black);
                }
                else if (drawMode == DrawMode.Humidity)
                {
                    goTiles[x, y].GetComponent<SpriteRenderer>().sprite = BLANKSprite;
                    goTiles[x, y].GetComponent<SpriteRenderer>().color = SetTileColor(map.GetTile(x, y),map.GetTile(x, y).GetTileHumidity(),Color.white,Color.blue);
                }
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

    float GeneratePerlin(float x, float y)
    {
        float xCoord = (float)(x / MapWidth) * Frequency + Seed;
        float yCoord = (float)(y / MapHeight) * Frequency + Seed;
        float e = Mathf.PerlinNoise(xCoord, yCoord);
        e = Mathf.Clamp(e,0f,1f);
        return Mathf.Pow(e, Redistribution);
    }

    float GeneratePerlinMix(float x, float y, float seedOffset, float frequencyModifier)
    {
        float WaveLenght = (100 / Frequency) * WaveLenghtModifier;

        float xCoord = (float)((x / MapWidth) - 0.5f) * Frequency + Seed + seedOffset;
        float yCoord = (float)((y / MapHeight) - 0.5f) * Frequency + Seed + seedOffset;
        float p1 = Mathf.PerlinNoise(xCoord, yCoord);

        float xCoord2 = (float)((x / MapWidth) - 0.5f) * (Frequency * frequencyModifier) + Seed + seedOffset;
        float yCoord2 = (float)((y / MapHeight) - 0.5f) * (Frequency * frequencyModifier) + Seed + seedOffset;
        float p2 = WaveLenght * Mathf.PerlinNoise(xCoord2, yCoord2);

        float xCoord3 = (float)((x / MapWidth) - 0.5f) * (Frequency * (frequencyModifier * 2)) + Seed + seedOffset;
        float yCoord3 = (float)((y / MapHeight) - 0.5f) * (Frequency * (frequencyModifier * 2)) + Seed + seedOffset;
        float p3 = WaveLenght * Mathf.PerlinNoise(xCoord3, yCoord3);

        float e = p1 + p2 + p3;

        return Mathf.Pow(e, Redistribution);
    }

    Sprite SetTerrainSprites(Tile tile)
    {
        float e = tile.GetTileElevation();
        float h = tile.GetTileHumidity();

        if (e > SeaLevel + 0.6f)
        {
            if (h > 0.6f)
            {
                tile.SetTileType(TileType.SNOW);
                return SnowSprite;
            }
            tile.SetTileType(TileType.ROCK);
            return RockSprite;
        }
        else if (e >= SeaLevel + 0.4f)
        {
            tile.SetTileType(TileType.STONE);
            return StoneSprite;
        }
        else if (e >= SeaLevel + 0.05f)
        {
            if (h > 0.6f)
            {
                tile.SetTileType(TileType.FOREST);
                return ForestSprite;
            }
            else if (h < 0.1f)
            {
                tile.SetTileType(TileType.DESERT);
                return DesertSprite;
            }
            tile.SetTileType(TileType.GRASS);
            return GrassSprite;

        }
        else if (e >= SeaLevel)
        {
            tile.SetTileType(TileType.BEACH);
            return BeachSprite;
        }
        else if (e >= SeaLevel - 0.1f)
        {
            tile.SetTileType(TileType.WATER_SHALLOW);
            return ShallowWaterSprite;
        }
        tile.SetTileType(TileType.WATER_DEEP);
        return DeepWaterSprite;
    }
    Color SetTileColor(Tile tile, float i, Color c1, Color c2)
    {
        return Color.Lerp(c1, c2, i);
    }

    public Vector3 GetTilePosition(Vector3 pos)
    {
        int x = (int)Mathf.Clamp(pos.x, 0, MapWidth - 1);
        int y = (int)Mathf.Clamp(pos.y, 0, MapHeight - 1);
        return goTiles[x, y].transform.position;
    }
    public Tile GetTileInfo(Vector3 pos)
    {
        int x = (int)Mathf.Clamp(pos.x, 0, MapWidth - 1);
        int y = (int)Mathf.Clamp(pos.y, 0, MapHeight - 1);
        return map.GetTile(x, y);
    }
    public void ChangeSeed()
    {
        Seed = Random.Range(0, 999);
        TerrainGenerationToggle = true;
    }

    public void ToggleTerrainGeneration(){
        TerrainGenerationToggle = true;
    }
}