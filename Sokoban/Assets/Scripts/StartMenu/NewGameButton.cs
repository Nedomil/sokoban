using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void startGame() {
		Application.LoadLevel (1);
	}
}
