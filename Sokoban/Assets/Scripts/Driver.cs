using UnityEngine;
using System.Collections;

/**
 * Kontrolliert das gesamte Spiel. Generiert die Sokobans und den Spieler.
 * 
 * Verantwortung:
 * - Konstruiert Sokobans
 * - Konstrueirt Spieler
 * - Leitet die Wichtigen Objekte weiter (Board, Player usw.)
 */
public class Driver : MonoBehaviour {
	private ArrayList sokobans;
	private Player player;
	private bool inSpace;
	private ButtonManager buttonManager;

	private Sokoban activeSokoban;

	// Use this for initialization
	void Start () {
		sokobans = new ArrayList ();
		setUpSokobans ();
		connectSpacePlayer ();
		activeSokoban = (Sokoban) sokobans [0];
		inSpace = false;
		buttonManager = FindObjectOfType<ButtonManager> ();
	}

	private void setUpSokobans() {
		setUpSokoban (new Vector3 (0, 0, 0), "Levels/level1", true, new Vector3 (0.57f, 1.44f, -1.45f), new Vector3 (0, -0.819252f, 0.5735f));
		Board board = ((Sokoban)sokobans[0]).getBoard ();
		player = new Player (board.getStartPositionPlayerX (), board.getStartPositionPlayerY (), board, new Vector3 (0, 0, 0));


		setUpSokoban (new Vector3 (10, 0, 20), "Levels/level2", true, new Vector3 (10.891f, 1.412f, 18.436f), new Vector3 (0, -0.819252f, 0.5735f));

		setSamePlayerInSokobans ();
	}

	private void setUpSokoban(Vector3 placeInWorld, string levelPath, bool newPlayer, Vector3 camPosition, Vector3 camForward) {
		Sokoban sokoban = new Sokoban (placeInWorld, levelPath, newPlayer, this);
		sokoban.setMainCamStartPosition(camPosition);
		sokoban.setMainCamStartForward(camForward);
		sokobans.Add (sokoban);
	}

	public bool gamePaused() {
		return buttonManager.getMainMenuOn ();
	}

	private void setSamePlayerInSokobans() {
		foreach (Sokoban sokoban in sokobans)
			sokoban.setPlayer (player);
	}

	// Update is called once per frame
	void Update () {
        if (!inSpace)
			activeSokoban.sokobanListener ();
	}

	public void setInSpace(bool boolean) {
		inSpace = boolean;
	}

	public void setMainCamToStartTranslate() {
		Camera.main.transform.position = activeSokoban.mainCamStartPosition;
		Camera.main.transform.forward = activeSokoban.mainCamStartForward;
	}

	public void connectSpacePlayer() {
		player.getGameObject ().GetComponent<SpacePlayer> ().driver = this;
		player.setDriver (this);
	}

	public void setActiveSokoban(Sokoban sokoban) {
		activeSokoban = sokoban;
	}

	public void setPlayer (Player player) {
		this.player = player;
	}

	public Sokoban getActiveSokoban() {
		return activeSokoban;
	}

	public Board getActiveBoard() {
		return activeSokoban.getBoard ();
	}

	public Player getPlayer() {
		return player;
	}

	public bool isInSpace() {
		return inSpace;
	}

	public ArrayList getSokobans() {
		return sokobans;
	}

	public SpacePlayer getSpacePlayer() {
		return player.getGameObject ().GetComponent<SpacePlayer> ();
	}
}
