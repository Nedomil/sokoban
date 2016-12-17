using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Timers;

/**
 * Erlaubt es dem Spieler, das Spiel neu zu starten. Wird nur angezeigt, wenn
 * der Spieler auf einem Spielfeld ist.
 * 
 * Verantwortung:
 * - Rücksetzung der Objekte an ihre Startposition. Rüft dafür 'Board#removeFromBoard' und
 * 'Board#restoreStartSituation' auf.
 */
public class RestartButton : MonoBehaviour, IGUIHide {
	public ButtonManager buttonManager;
	public Button button;
	public Driver driver;
	private int counter;
	bool active;
	bool upperMenuActive;
	bool hadUpperMenuAndWasActive;

	void Start() {
		counter = 0;
		hadUpperMenuAndWasActive = false;
		active = true;
	}

	void Update () {
		if (counter > 0)
			--counter;
		else {
			button.onClick.AddListener (delegate { restart (); });
			counter = 10000;
		}
	}

	public void setUpperMenuActive(bool upperMenuActive) {
		this.upperMenuActive = upperMenuActive;
		if (upperMenuActive)
			hideObject ();
		else
			showObject ("upperMenu");
	}

	private void restart() {
		Sokoban sokoban = driver.getActiveSokoban ();
		Board board = sokoban.getBoard ();
		board.removeFromBoard ();
		board.restoreStartSituation ();
	}

	public void hideObject() {
		hadUpperMenuAndWasActive = upperMenuActive && active;
		active = false;
		button.gameObject.SetActive(false);
	}

	public void showObject(string reason) {
		if (hadUpperMenuAndWasActive || reason.Equals("land")) {
			button.gameObject.SetActive (true);
			active = true;
		}
	}
}
