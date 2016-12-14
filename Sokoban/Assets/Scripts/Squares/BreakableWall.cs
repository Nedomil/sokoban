using UnityEngine;
using System.Collections;
using System;

public class BreakableWall : MovableSquare
{

    private static readonly string STRING_REPRESENTATION = "%";
    private static readonly string NAME = "BreakableWall";
    private static readonly string MODLE_PATH = "Wall/Wand1";

    public Mesh objectToCreate;

    public BreakableWall(int x, int y, Board board, Vector3 position) : base(x, y, board, position)
    {
        board.setSquareSize(board.objectsSize(MODLE_PATH));
        buildObject(NAME, MODLE_PATH);
    }

    public override string stringRepresentation()
    {
        return STRING_REPRESENTATION;
    }

    public override string ToString()
    {
        return "Zerstörbare Mauer - Position (" + getX() + "," + getY() + ")";
    }

    public override bool askToLandHere(string direction, Square askingSquare)
    {
        if ("Bomb".Equals(askingSquare.GetType().Name))
        {
            ((Bomb)askingSquare).explodeBomb();
            explodeWall();
            return true;
        }
        else
            return false;
    }

    private void explodeWall()
    {
        gameObject.SetActive(false);
        leaveBoard();
    }

    public override void changeDirection(string newDirection)
    {
        //Do nothing
    }

    public override void removeFromBoard()
    {
        if (onBoard)
            base.removeFromBoard();
    }
}