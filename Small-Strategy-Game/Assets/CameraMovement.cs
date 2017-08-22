using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	Camera cam;
	public MapGenerator mapGenerator;
	public float panSpeed = 5;
	public float boarderThikness = 0;
	public float scrollSpeed = 20;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = cam.transform.position;
		if(Input.GetKey(KeyCode.W)){
			pos.y += panSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.S)){
			pos.y += -panSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.A)){
			pos.x += -panSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.D)){
			pos.x += panSpeed * Time.deltaTime;
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0){
			cam.orthographicSize += scrollSpeed * Time.deltaTime; 
		}
		if(Input.GetAxis("Mouse ScrollWheel") > 0){
			cam.orthographicSize -= scrollSpeed * Time.deltaTime; 
		}
		pos.x = Mathf.Clamp(pos.x, 0 + boarderThikness, mapGenerator.MapWidth - boarderThikness);
		pos.y = Mathf.Clamp(pos.y, 0 + boarderThikness, mapGenerator.MapHeight - boarderThikness);
		cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 5f, 10f);
		cam.transform.position = pos;
	}
}
