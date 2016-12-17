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
	bool upperMenuActive;
	bool hadUpperMenuAndWasActive;
	bool active;

	/**
	 * Counter wird auf 0 gesetzt und der Button versteckt.
	 */
	void Start () {
		counter = 0;
		hadUpperMenuAndWasActive = false;
		active = false;
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

	public void showOrHide() {
		if (!upperMenuActive) {
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
					showObject ("default");
				else
					hideObject ();
			}
		}
	}

	public void setUpperMenuActive(bool upperMenuActive) {
		this.upperMenuActive = upperMenuActive;
		if (upperMenuActive)
			hideObject ();
		else
			showObject ("upperMenu");
	}

	private void land() {
		Sokoban sokoban = driver.getActiveSokoban ();
		driver.setInSpace (false);
		driver.setMainCamToStartTranslate ();
		sokoban.getBoard ().removeFromBoard ();
		sokoban.getBoard ().restoreStartSituation ();
		sokoban.getPlayer ().goToStartTransform ();
		setPlayersSpeedZero ();
		buttonManager.showSokobanButtons ("land");
		hideObject ();
	}

	private void setPlayersSpeedZero() {
		driver.getSpacePlayer ().resetSpeed ();
	}

	public void hideObject() {
		hadUpperMenuAndWasActive = upperMenuActive && active;
		active = false;
		button.gameObject.SetActive(false);
	}

	public void showObject(string reason) {
		if (hadUpperMenuAndWasActive || reason.Equals("default")) {
			button.gameObject.SetActive (true);
			active = true;
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
}
