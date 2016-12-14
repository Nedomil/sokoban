using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Erlaubt es dem Spieler vom Spielfeld abzuheben.
 * 
 * Verantwortung:
 * - Lässt den Spieler in den Weltall-Modus wechseln ('Driver#inspace') und bearbeitet die
 * Kameraperspektive dementsprechend.
 * - Kann das Objekt anzeigen und verstecken.
 */
public class LeaveButton : MonoBehaviour, IGUIHide {
	public ButtonManager buttonManager;
	public Button button;
	public Driver driver;
	public Camera mainCam;
	private int counter;

	void Start () {
		counter = 0;
	}

	void Update () {
		if (counter > 0)
			--counter;
		else {
			button.onClick.AddListener (delegate { leave (); });
			counter = 10000;
		}
	}

	private void leave() {
		driver.setInSpace (true);
		changeCamViewToPlayerVew ();
		buttonManager.hideSokobanButtons ();
	}

	private void changeCamViewToPlayerVew() {
		Sokoban sokoban = driver.getActiveSokoban ();
		Player player = sokoban.getPlayer ();
		GameObject goPlayer = player.getGameObject();

		Camera.main.transform.position = goPlayer.transform.localPosition - 2 * goPlayer.transform.forward + Vector3.up;
		Camera.main.transform.forward = goPlayer.transform.forward;
		Camera.main.transform.Rotate(30, 0, 0);
	}

	public void hideObject() {
		button.gameObject.SetActive(false);
	}

	public void showObject() {
		button.gameObject.SetActive(true);
	}
}
