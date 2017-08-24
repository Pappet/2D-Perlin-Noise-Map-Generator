using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{

    public GameObject Selector;
    public MapGenerator map;
    Camera cam;
	public Text PositionText;
	public Text TerrainText;
	public Text ElevationText;
	public Text HumidityText;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
		Selector.SetActive(false);
		PositionText.text = "TilePosition: NO TILE";
		TerrainText.text = "Terrain: NO TILE";
		ElevationText.text = "Elevation: NO TILE";
		HumidityText.text = "Hunidity: NO TILE";
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Selector.transform.position;
        if (Input.GetMouseButtonDown(0))
        {
			Selector.SetActive(true);
			
			Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
			Tile t = map.GetTileInfo(mousePos);
            Vector3 tilePos = map.GetTilePosition(mousePos);

            pos = tilePos;
            pos.z = -5f;
            Selector.transform.position = pos;

			PositionText.text = "TilePosition: X " + t.GetTilePosition().x +" / Y " + t.GetTilePosition().y;
			TerrainText.text = "Terrain: " + t.GetTileType();
			ElevationText.text = "Elevation: " + t.GetTileElevation().ToString("F");
			HumidityText.text = "Hunidity: " + t.GetTileHumidity().ToString("F");
        }
		if(Input.GetKeyDown(KeyCode.Escape)){
			Selector.SetActive(false);
			PositionText.text = "TilePosition: NO TILE";
			TerrainText.text = "Terrain: NO TILE";
			ElevationText.text = "Elevation: NO TILE";
			HumidityText.text = "Hunidity: NO TILE";
		}
    }
}
