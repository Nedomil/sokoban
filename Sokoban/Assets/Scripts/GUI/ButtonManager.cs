using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

/**
 * Zeigt und Versteckt die Buttons des Interfaces.
 * 
 * Verantwortung:
 * - Kennt alle Buttons
 * - Kennt Unterschied zwischen Sokoban- und Space-Buttons.
 * - Hat Zugriff auf die Buttons und kann sie ein und ausschalten.
 */
public class ButtonManager : MonoBehaviour {

	public LeaveButton leaveButton;
	public RestartButton restartButton;
	public LandButton landButton;
	public Driver driver;

	private bool mainMenuOn;

	void Start() {
	}

	void Update() {
		showOrHideLandButton ();

		if (Input.GetKeyDown (KeyCode.Escape)) {
			showOrHideMainMenu ();
		}
	}

	/**
	 * Berechnet die Länge des gegebenen Vektors.
	 * @param Vektor, dessen Länge berechnet werden soll.
	 * @return länge des Vektors.
	 */
	public float vectorLength(Vector3 v) {
		return (float)System.Math.Sqrt (System.Math.Pow (v.x, 2) + System.Math.Pow (v.y, 2) + System.Math.Pow (v.z, 2));
	}

	public void hideSokobanButtons() {
		leaveButton.hideObject ();
		restartButton.hideObject ();
	}

	public void showSokobanButtons() {
		leaveButton.showObject ();
		restartButton.showObject ();
	}

	private void showOrHideLandButton() {
		Player player = driver.getPlayer ();
		ArrayList sokobans = driver.getSokobans ();
		bool showLandButton = false;
		foreach (Sokoban sokoban in sokobans) {
			Vector3 distanceToSokobanV = player.getGameObject ().transform.position - sokoban.getPositionCenterSokoban ();
			float distanceToSokoban = vectorLength (distanceToSokobanV);
			if (distanceToSokoban < 1 && driver.isInSpace ()) {
				showLandButton = true;
				driver.setActiveSokoban (sokoban);
			}
			if (showLandButton)			
				landButton.showObject ();
			else
				landButton.hideObject ();
		}
	}

	private void showOrHideMainMenu() {
		changeMainMenuOn ();
		showOrHideGUI ();
	}

	private void showOrHideGUI() {
		if (mainMenuOn)
			hideAllButtons ();
		else
			showAllButtons();
	}

	private void changeMainMenuOn() {
		bool temp = mainMenuOn;
		if (mainMenuOn)
			mainMenuOn = false;
		else
			mainMenuOn = true;

		Assert.IsTrue (mainMenuOn != temp);
	}

	private void showAllButtons() {
		showSokobanButtons ();
		showOrHideLandButton ();
	}

	private void hideAllButtons() {
		hideSokobanButtons ();
		landButton.hideObject ();
	}
}
