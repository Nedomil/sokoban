using UnityEngine;
using System.Collections;

public class Box : MovableSquare {

	private static readonly string STRING_REPRESENTATION = "B";
	private static readonly string NAME = "Box";
	private static readonly string MODLE_PATH = "Box/Box1";

	public Box(int x, int y, Board board, Vector3 position) : base(x, y, board, position) {
		buildObject (NAME, MODLE_PATH);
	}

	public override string stringRepresentation() {
		return STRING_REPRESENTATION;
	}

	public override string ToString() {
		return "Kiste - Position (" + getX () + "," + getY () + ")";
	}

	public override void changeDirection (string newDirection) {
		//Do nothing.
	}
}
