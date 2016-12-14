using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class Player : MovableSquare {
	private static readonly string STRING_REPRESENTATION = "P";
	private static readonly string NAME = "Player";
	private static readonly string MODLE_PATH = "Player/Spieler1";
	private static readonly string START_LOOKING_DIRECTION = "up";
    private string lookingDirection;

	private Vector3 startForward;

	public Player(int x, int y, Board board, Vector3 position) : base(x, y, board, position) {
		buildObject (NAME, MODLE_PATH);
		this.lookingDirection = START_LOOKING_DIRECTION;
		this.startForward = gameObject.transform.forward;
		this.gameObject.AddComponent<SpacePlayer> ();
	}

	public override string stringRepresentation() {
		return STRING_REPRESENTATION;
	}

	/**
	 * Dreht die Figur in eine neue Richtung.
	 * @param newDirection neue Richtung des Objektes.
	 */
	public override void changeDirection (string newDirection) {
		int actualDirection = convertDirectionToInt (lookingDirection);
		int nextDirection = convertDirectionToInt (newDirection);
		int result = nextDirection - actualDirection;
		int resultAbs = System.Math.Abs (result);
		for (int i = 0; i < resultAbs; i++) {
			if (result > 0)
				gameObject.transform.Rotate(0, 90, 0);
			if (result < 0)
				gameObject.transform.Rotate(0, -90, 0);
		}
		lookingDirection = newDirection;
	}

	/**
	 * Hilfsmethode um ein Objekt zu drehen. Wandelt die Richtungen in Zahlen
	 * um. Up wird zu 0, right zu 1, down zu 2 und left zu 3.
	 * @param direction Richtung, die umgewandelt werden soll.
	 * @return int-Wert, welcher der Richtung entspricht.
	 */
	private int convertDirectionToInt(string direction) {
		int i;
		switch (direction) {
		case "right":
			i = 0;
			break;
		case "down":
			i = 1;
			break;
		case "left":
			i = 2;
			break;
		case "up":
			i = 3;
			break;
		default:
			throw new InvalidMoveException ();
		}
		return i;
	}

	/**
	 * Stellt die Startsituation des Objektes wieder her. Lässt das
	 * den Vater tun und dreht den Spieler ausserdem in die richtige 
	 * Richtung.
	 */
	public override void restoreStartSituation() {
		int startX = activeBoard().getStartPositionPlayerX ();
		int startY = activeBoard().getStartPositionPlayerY (); 
		Square startSquare = activeBoard().getSquareFromBoard (startX, startY);
		startSquare.setMovableSquare (this);
		x = startX;
		y = startY;
		movePositionInWorld ();
		gameObject.SetActive (true);
		changeDirection(START_LOOKING_DIRECTION);
	}

	public void goToStartTransform() {
		x = activeBoard().getStartPositionPlayerX ();
		y = activeBoard().getStartPositionPlayerY ();
		distanceFromZero = activeBoard().getSquareFromBoard(0,0).getDistanceFromZero ();
		movePositionInWorld ();
		//gameObject.transform.position = startPosition;
		gameObject.transform.forward = startForward;
	}

	public string getStartDirection() {
		return START_LOOKING_DIRECTION;
	}

	public override string ToString() {
		return "Spieler - Position (" + getX () + "," + getY () + ")";
	}

	public string getDirection() {
		return lookingDirection;
	}

	public void setDriver(Driver driver) {
		Assert.IsTrue (driver != null);
		this.driver = driver;
		Assert.IsTrue (this.driver != null);
	}
}
