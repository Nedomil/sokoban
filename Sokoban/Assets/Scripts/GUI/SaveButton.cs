using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SaveButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void save() {
		Driver driver = Camera.main.GetComponent<Driver> ();
		SaveLoad.Save ();
	}
}
