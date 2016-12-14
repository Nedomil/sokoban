using UnityEngine;
using System.Collections;
using System;

public class Floor : Square {
	
	private static readonly string STRING_REPRESENTATION = " ";
	private static readonly string NAME = "Floor";
	private static readonly string MODLE_PATH = "Floor/Boden1";
	private static readonly bool WAKABLE = true;

	public Floor(int x, int y, Board board, Vector3 position) : base(x, y, board, position) {
		buildObject (NAME, MODLE_PATH);
	}

	public override string stringRepresentation() {
		return STRING_REPRESENTATION;
	}

	public override string ToString() {
		return "Weg - Position (" + getX () + "," + getY () + ")";
	}

	public override bool askToLandHere(string direction, Square askingSquare) {
		if (!hasPushableElement())
			return WAKABLE;
		else
			return getMovableSquare().askToLandHere (direction, askingSquare);
	}

    protected bool hasPushableElement()
    {
        return hasBox() || hasBomb() || hasBreakableWall();
    }
}
