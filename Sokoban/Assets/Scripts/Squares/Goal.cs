using UnityEngine;
using System.Collections;

public class Goal : Floor {
	
	private static readonly string STRING_REPRESENTATION = "G";
	private static readonly string NAME = "Goal";
	private static readonly string MODLE_PATH = "Floor/Boden2";
	private static readonly bool WAKABLE = true;

	public Goal(int x, int y, Board board, Vector3 position) :	base(x, y, board, position) {
		buildObject (NAME, MODLE_PATH);
	}

	public override string stringRepresentation() {
		return STRING_REPRESENTATION;
	}

	public override string ToString() {
		return "Ziel - Position (" + getX () + "," + getY () + ")";
	}
}