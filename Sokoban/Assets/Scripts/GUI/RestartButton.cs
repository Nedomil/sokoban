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

	void Start() {
		counter = 0;
	}

	void Update () {
		if (counter > 0)
			--counter;
		else {
			button.onClick.AddListener (delegate { restart (); });
			counter = 10000;
		}
	}

	private void restart() {
		Sokoban sokoban = driver.getActiveSokoban ();
		Board board = sokoban.getBoard ();
		board.removeFromBoard ();
		board.restoreStartSituation ();
	}

	public void hideObject() {
		button.gameObject.SetActive(false);
	}

	public void showObject() {
		button.gameObject.SetActive(true);
	}
}
