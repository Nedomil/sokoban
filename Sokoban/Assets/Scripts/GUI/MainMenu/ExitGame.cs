using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void exitGame() {
		Application.LoadLevel (0);
	}
}
