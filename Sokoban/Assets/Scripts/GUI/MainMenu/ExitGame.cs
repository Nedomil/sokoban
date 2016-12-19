using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour {

	private bool COOLDOWN = false;
	private int TIMER = 60;

	private bool cooldown;
	private int timer; 

	// Use this for initialization
	void Start () {
		resetTimer ();
	}

	void Update() {
		if (cooldown) {
			if (timer <= 0) {
				resetTimer ();
				Application.LoadLevel (0);
			}
			timer--;
		}
	}

	public void exitGame() {
		cooldown = true;
	}

	private void resetTimer() {
		timer = TIMER;
		cooldown = COOLDOWN;
	}
}
