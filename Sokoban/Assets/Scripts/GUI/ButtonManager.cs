using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
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
	public Button MPlayButton;
	public Button MSaveButton;
	public Button MExitButton;
	private LeaveButton sLeaveButton;
	private RestartButton sRestartButton;
	private LandButton sLandButton;
	public Driver driver;

	private bool mainMenuOn;

	void Start() {
		Assert.IsNotNull (landButton);
		Assert.IsNotNull (restartButton);
		Assert.IsNotNull (leaveButton);
		sLeaveButton = leaveButton.GetComponent<LeaveButton>();
		sRestartButton = restartButton.GetComponent<RestartButton>();
		sLandButton = landButton.GetComponent<LandButton>();
		mainMenuOn = false;
		showAllMainMenuElements (false);
	}

	void Update() {
		sLandButton.showOrHide ();
		if (Input.GetKeyDown (KeyCode.Escape)) {
			showOrHideMainMenu ();
		}
	}

	private void showAllMainMenuElements(bool boolean) {
		MPlayButton.gameObject.SetActive (boolean);
		MSaveButton.gameObject.SetActive (boolean);
		MExitButton.gameObject.SetActive (boolean);
	}

	public void hideSokobanButtons() {
		leaveButton.hideObject ();
		restartButton.hideObject ();
	}

	public void showSokobanButtons(string reason) {
		leaveButton.showObject (reason);
		restartButton.showObject (reason);
	}

	public void showOrHideMainMenu() {
		changeMainMenuOn ();
		showOrHideGUI ();
	}

	private void changeMainMenuOn() {
		bool temp = mainMenuOn;
		if (mainMenuOn) {
			mainMenuOn = false;
			showAllMainMenuElements (false);
		} else {
			mainMenuOn = true;
			showAllMainMenuElements (true);
		}

		Assert.IsTrue (mainMenuOn != temp);
	}

	private void showOrHideGUI() {
		if (mainMenuOn)
			mainMenuAsUpperMenu (true);
		else
			mainMenuAsUpperMenu(false);
	}

	private void mainMenuAsUpperMenu(bool boolean) {
		sLandButton.setUpperMenuActive (boolean);
		sLeaveButton.setUpperMenuActive (boolean);
		sRestartButton.setUpperMenuActive (boolean);
	}

	public bool getMainMenuOn() {
		return mainMenuOn;
	}
}
