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

    public int MapWidth;
    public int MapHeight;
    int Seed = 334;
    float Frequency = 2f;
    float WaveLenghtModifier = 0.004f;
    float Redistribution = 3f;
    public Text seedText;
    public Text redistributionText;
    public Slider redistributionSlider;
    public Text frequenzyText;
    public Slider frequenzySlider;
    public Text WaveLenghtModifierText;
    public Slider WaveLenghtModifierSlider;
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

        Redistribution = redistributionSlider.value;
        redistributionText.text = Redistribution.ToString();

        Frequency = frequenzySlider.value;
        frequenzyText.text = Frequency.ToString();

        WaveLenghtModifier = WaveLenghtModifierSlider.value;
        WaveLenghtModifierText.text = WaveLenghtModifier.ToString();
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
                //t.SetTileElevation(GeneratePerlin(x, y));
                t.SetTileElevation(GeneratePerlinMix(x, y, 0, 5));
                t.SetTileHumidity(GeneratePerlinMix(x, y, 50,10));
            }
        }
    }

    float GeneratePerlin(float x, float y)
    {
        float xCoord = (float)(x / MapWidth) * Frequency + Seed;
        float yCoord = (float)(y / MapHeight) * Frequency + Seed;
        float e = Mathf.PerlinNoise(xCoord, yCoord);

        return Mathf.Pow(e, Redistribution);
    }

    float GeneratePerlinMix(float x, float y, float SeedOffset, float FrequencyModifier)
    {
        float WaveLenght = (100 / Frequency) * WaveLenghtModifier;

        float xCoord = (float)((x / MapWidth) - 0.5f) * Frequency + Seed + SeedOffset;
        float yCoord = (float)((y / MapHeight) - 0.5f) * Frequency + Seed + SeedOffset;
        float p1 = Mathf.PerlinNoise(xCoord, yCoord);

        float xCoord2 = (float)((x / MapWidth) - 0.5f) * (Frequency * FrequencyModifier) + Seed + SeedOffset;
        float yCoord2 = (float)((y / MapHeight) - 0.5f) * (Frequency * FrequencyModifier) + Seed + SeedOffset;
        float p2 = WaveLenght * Mathf.PerlinNoise(xCoord2, yCoord2);

        float xCoord3 = (float)((x / MapWidth) - 0.5f) * (Frequency * (FrequencyModifier * 2)) + Seed + SeedOffset;
        float yCoord3 = (float)((y / MapHeight) - 0.5f) * (Frequency * (FrequencyModifier * 2)) + Seed + SeedOffset;
        float p3 = WaveLenght * Mathf.PerlinNoise(xCoord3, yCoord3);

        float e = p1 + p2 + p3;

        return Mathf.Pow(e, Redistribution);
    }

    Sprite SetSprite(Tile tile)
    {
        float e = tile.GetTileElevation();
        float h = tile.GetTileHumidity();

        if (e > 0.9f)
        {
            if (h > 0.6f)
                return SnowSprite;

            return RockSprite;
        }
        else if (e >= 0.7f)
        {
            return StoneSprite;
        }
        else if (e >= 0.4f)
        {
            if (h > 0.6f)
            {
                return ForestSprite;
            }
            else if (h < 0.1f)
            {
                return DesertSprite;
            }
            return GrassSprite;

        }
        else if (e >= 0.3f)
        {
            return BeachSprite;
        }
        else if (e >= 0.2f)
        {
            return ShallowWaterSprite;
        }
        return DeepWaterSprite;
    }
    public void ChangeSeed()
    {
        Seed = Random.Range(0, 999);
    }
}