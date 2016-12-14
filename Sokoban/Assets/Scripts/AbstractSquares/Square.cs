using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

/**
 * Fungiert als Grundgerüst für ein beliebiges Feld. Kennt seine Position und ist dafür
 * verantwortlich, die Objekte richtig darzustellen.
 * 
 * Verantwortung:
 * - Kennt Driver, Board, GameObject, Position auf dem Board, Position in der Welt
 * - Behandlet, ob das Feld besetzt ist oder nicht (von MovableSquare)
 * - Konstruiert das dazugehörende GemeObject
 */
public abstract class Square {
	protected GameObject gameObject;
	protected MovableSquare movableSquare;
	protected int x, y;
	protected Board board;
	private float positionInWorldX, positionInWorldY, positionInWorldZ;
	protected Vector3 distanceFromZero;
	protected Driver driver;

	/**
	 * Speichert die Koordinaten des Feldes ab.
	 * @param x Koordinate des Feldes.
	 * @param y Koordinate des Feldes.
	 */
	public Square (int x, int y, Board board, Vector3 distanceFromZero) {
		this.distanceFromZero = distanceFromZero;
		setX(x);
		setY(y);
		positionInWorldZ = 0; // <---------------------------------------------------------------Höhe des Feldes. Muss noch von Sokoban abhängig gemacht werden.
		this.board = board;
		driver = Camera.main.GetComponent<Driver> ();
	}

	/**
	 * Konstruiert das Objekt graphisch auf dem Spielfeld. Bedient sich dabei des eingegebenen
	 * Pfades um das richtige Objekt zu generieren.
	 * @param name Name, welches das Objekt haben soll.
	 * @param modelPath Pfad des Models, welches erstellt werden soll.
	 * @param x Koordinate auf dem Spielfeld
	 * @param y Koordinate auf dem Spielfeld
	 */
	protected void buildObject(string name, string modelPath) {
		Assert.IsTrue (board.getSquareSize() > 0);
		gameObject = new GameObject (name);

		Mesh wallMesh = objectsMesh(modelPath);
		Material wallMaterial = objectsMaterial (modelPath);
		setUpObjectsPosition();

		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter> ();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer> ();
		meshFilter.mesh = wallMesh;
		meshRenderer.material = wallMaterial;
		gameObject.transform.position = new Vector3 (positionInWorldX, positionInWorldZ, positionInWorldY) + distanceFromZero;

		gameObject.AddComponent<SphereCollider> ();
	}

	/**
	 * Gibt Mesh eines Objektes zurück.
	 * @param path Pfad des Objektes.
	 * @return Mesh des Objektes.
	 */
	private Mesh objectsMesh(string path) {
		return ((GameObject)Resources.Load (path)).GetComponent<MeshFilter> ().sharedMesh as Mesh;
	}

	/**
	 * Gibt Material eines Objektes zurück.
	 * @param pash Pfad des Objektes.
	 * @return Material des Objektes.
	 */
	private Material objectsMaterial(string path) {
		return ((GameObject)Resources.Load (path)).GetComponent<MeshRenderer> ().sharedMaterial as Material;
	}

	/**
	 * Ermittelt die Grösse der Objekte und anhand deren, wie weit die Objekte
	 * in der Welt von einander entfernt sein müssen.
	 */
	private void setUpObjectsPosition() {
		setPositionInWorld ();
	}

	/**
	 * Berechnet anhand der ArrayKoordinaten und der Grösse des Objektes, wo es
	 * in der Welt positioniert werden muss.
	 */
	private void setPositionInWorld() {
		positionInWorldX = x * board.getSquareSize();
		positionInWorldY = - y * board.getSquareSize();
	}

	/**
	 * Berechnet anhad der ArrayKoordinaten und der Grösse des Objektes, wo es
	 * in der Welt positioniert wird. Im Unterschied zu Square#movePositionInWorld aktualisiert
	 * die Metode die Position des Objektes.
	 */
	public void movePositionInWorld() {
		positionInWorldX = x * board.getSquareSize();
		positionInWorldY = - y * board.getSquareSize();
		gameObject.transform.position = new Vector3 (positionInWorldX, positionInWorldZ, positionInWorldY) + distanceFromZero;
	}

	public void setX(int x) {
		this.x = x;
	}

	public void setY(int y) {
		this.y = y;
	}

	public int getX() {
		return x;
	}

	public int getY() {
		return y;
	}

	public MovableSquare getMovableSquare() {
		return movableSquare;
	}

	public virtual void setMovableSquare(MovableSquare movableSquare) {
		this.movableSquare = movableSquare;
	}

	public bool hasPlayer() {
		return movableSquare != null && "Player".Equals(movableSquare.GetType().Name);
	}

	public bool hasBox() {
		return movableSquare != null && "Box".Equals(movableSquare.GetType().Name);
	}

    public bool hasBomb()
    {
        return movableSquare != null && "Bomb".Equals(movableSquare.GetType().Name);
    }

    public bool hasBreakableWall()
    {
        return movableSquare != null && "BreakableWall".Equals(movableSquare.GetType().Name);
    }

    public GameObject getGameObject() {
		return gameObject;
	}

	public Vector3 getDistanceFromZero() {
		return distanceFromZero;
	}

	public void removePlayer() {
		if ("Player".Equals (movableSquare.GetType ().Name)) {
			movableSquare.getGameObject ().SetActive (false);
			movableSquare = null;
		}
	}

	/**
	 * Gibt zurück, wie das Feld ursprünglich als String ausgesehen hat. Dient nur
	 * Testzwecken.
	 * @return Representation des ursprünglichen Stringes.
	 */
	public abstract string stringRepresentation();

	/**
	 * überprüft, ob ein anfragendes Objekt hier landen darf. Gibt im entsprechenden
	 * Fall true zurück.
	 * @param direction Richtung in der das anfragende Objekt unterwegs ist.
     * @param askingSquare Feld, dass um die Landung fragt.
	 * @return true falls das anfragende Objekt hier landen darf.
	 */
	public abstract bool askToLandHere(string direction, Square askingSquare);
}
