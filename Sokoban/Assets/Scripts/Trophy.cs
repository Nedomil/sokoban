using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy {
	private static readonly string NAME_OPEN = "TrophyOpen";
	private static readonly string NAME_SOLVED = "TrophySolved";
	private static readonly string OPEN_PATH = "Trophies/Open/TrophyOpen";
	private static readonly string SOLVED_PATH = "Trophies/Solved/TrophySolved";
	private static readonly float HIGHT = 4.0f;
	protected GameObject gameObject;
	private Vector3 position;
	private Board board;
	private bool solved;
	private bool trophySolved;

	public Trophy(Board board, bool solved) 
	{
		this.board = board;
		this.solved = solved;
		this.trophySolved = false;
		buildObject(NAME_OPEN, OPEN_PATH);
	}

	private void changeTrophyToSolved () {
		this.trophySolved = true;
		GameObject.Destroy (gameObject);
		buildObject (NAME_SOLVED, SOLVED_PATH);
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
		gameObject = new GameObject (name);
		gameObject = (GameObject) GameObject.Instantiate(Resources.Load(modelPath));
		//Mesh wallMesh = objectsMesh(modelPath);
		//Material wallMaterial = objectsMaterial (modelPath);
		setUpObjectsPosition();

		//MeshFilter meshFilter = gameObject.AddComponent<MeshFilter> ();
		//MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer> ();
		//meshFilter.mesh = wallMesh;
		//meshRenderer.material = wallMaterial;
		gameObject.transform.position = position;
		gameObject.GetComponent<Collider>().enabled = false;
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

	private void setUpObjectsPosition() {
		Square[][] squares = board.getBoard ();
		Square smallestSquare = squares [0] [0];
		Square biggestSquare = squares [squares.Length - 1] [squares [0].Length - 1];
		GameObject gOSmallestSquare = smallestSquare.getGameObject ();
		GameObject gOBiggestSquare = biggestSquare.getGameObject ();

		Vector3 pos = new Vector3 ();
		pos = (gOBiggestSquare.transform.position + gOSmallestSquare.transform.position)/2;
		pos = pos + new Vector3 (0, HIGHT, 0);

		this.position = pos;
	}

	public void setSolved(bool solved) {
		if (solved && !trophySolved)
			changeTrophyToSolved ();
		this.solved = solved;
	}

	public bool getSolved() {
		return solved;
	}
}
