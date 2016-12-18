using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public Camera cam;
	public static readonly float ROTATION_SPEED = 0.005f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		cam.transform.Rotate (0, -ROTATION_SPEED, 0);		
	}
}
