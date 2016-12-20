using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameButton : MonoBehaviour {

	private bool COOLDOWN = false;
	private int TIMER = 100;

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
				Application.LoadLevel (1);
			}
			timer--;
		}
	}

	public void startGame() {
		cooldown = true;
	}

	private void resetTimer() {
		timer = TIMER;
		cooldown = COOLDOWN;
	}
}
