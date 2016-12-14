using UnityEngine;
using System.Collections;

public class Bomb : MovableSquare
{

    private static readonly string STRING_REPRESENTATION = "O";
    private static readonly string NAME = "Bomb";
    private static readonly string MODLE_PATH = "Bomb/Bomb";

    public Bomb(int x, int y, Board board, Vector3 position) : base(x, y, board, position)
    {
        buildObject(NAME, MODLE_PATH);
    }

    public override string stringRepresentation()
    {
        return STRING_REPRESENTATION;
    }

    public override string ToString()
    {
        return "Bombe - Position (" + getX() + "," + getY() + ")";
    }

    public override void changeDirection(string newDirection)
    {
        //Do nothing.
    }

    public void explodeBomb()
    {
        gameObject.SetActive(false);
        leaveBoard();
    }

    public override void removeFromBoard()
    {
        if (onBoard)
            base.removeFromBoard();
    }
}
