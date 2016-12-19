using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ContinueButton : MonoBehaviour {

	public bool cont;
	public Driver driver;

	// Use this for initialization
	void Start () {
		cont = false;
	}
	
	public void load() {
		SaveLoad.Load ();
		Assert.IsNotNull (SaveLoad.savedGames);
		driver = SaveLoad.savedGames [0];
		Assert.IsNotNull (driver);
		Application.LoadLevel (1);
	}
}
