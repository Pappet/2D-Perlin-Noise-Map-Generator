using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	public GameObject SelectorPrefab;
	public MapGenerator mapGenerator;
	bool selected = false;
	Tile tile;
	Camera cam;
	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			if(!selected){
				Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
				tile = mapGenerator.GetTileAt((int)pos.x,(int)pos.y);
				pos.x = Mathf.Abs(pos.x);
				pos.y = Mathf.Abs(pos.y);
				Instantiate(SelectorPrefab, pos, Quaternion.identity);
				selected = true;
			}
		}

	}
}
