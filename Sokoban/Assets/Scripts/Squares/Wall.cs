using UnityEngine;
using System.Collections;

public class Wall : Square {

	private static readonly string STRING_REPRESENTATION = "#";
	private static readonly string NAME = "Wall";
	private static readonly string MODLE_PATH = "Wall/Wand1";
	private static readonly bool WAKABLE = false;

	public Mesh objectToCreate;

	public Wall(int x, int y, Board board, Vector3 position) : base(x, y, board, position) {
		board.setSquareSize(board.objectsSize(MODLE_PATH));
		buildObject (NAME, MODLE_PATH);
	}

	public override string stringRepresentation() {
		return STRING_REPRESENTATION;
	}

	public override string ToString() {
		return "Mauer - Position (" + getX () + "," + getY () + ")";
	}

	public override bool askToLandHere(string direction, Square askingSquare) {
		return WAKABLE;
	}
}