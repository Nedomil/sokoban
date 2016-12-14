using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Erlaubt es dem Spieler auf deinem Sokoban-Spielfeld zu landen. ButtonManager entscheidet darüber,
 * ob dieser Button aktiv (nicht gleichzusetzen mit angezeigt) wird.
 * 
 * Verantwortung (wenn aktiv):
 * - Entscheidet, ob der Spieler genügend nahe an einem Sokoban ist, um selbst angezeigt zu werden.
 * - Leitet die Landung ein und sorgt dafür, dass die Kamerasicht angepasst wird und die Sokoban-Elemente
 * am richtigen Ort angezeigt werden.
 */
public class LandButton : MonoBehaviour, IGUIHide {
	public ButtonManager buttonManager;
	public Button button;
	public Driver driver;
	/* Nachdem der Button geklickt wird, sird der counter auf 10000 gesetzt, damit 'LandButton#land'
	 * nur einmal ausgeführt wird.
	 */
	private int counter;

	/**
	 * Counter wird auf 0 gesetzt und der Button versteckt.
	 */
	void Start () {
		counter = 0;
		hideObject ();
	}

	/**
	 * Leitet 'LandButton#land' ein, falls der Button geklickt wird und der counter 0 ist (was immer zutrifft,
	 * wenn zum ersten Mal geklickt wird).
	 */
	void Update() {
		if (counter > 0)
			--counter;
		else {
			button.onClick.AddListener (delegate { land (); });
			counter = 10000;
		}
	}

	private void land() {
		Sokoban sokoban = driver.getActiveSokoban ();
		driver.setInSpace (false);
		driver.setMainCamToStartTranslate ();
		sokoban.getBoard ().removeFromBoard ();
		sokoban.getBoard ().restoreStartSituation ();
		sokoban.getPlayer ().goToStartTransform ();
		setPlayersSpeedZero ();
		buttonManager.showSokobanButtons ();
		hideObject ();
	}

	private void setPlayersSpeedZero() {
		driver.getSpacePlayer ().resetSpeed ();
	}

	public void hideObject() {
		button.gameObject.SetActive(false);
	}

	public void showObject() {
		button.gameObject.SetActive(true);
	}
}
