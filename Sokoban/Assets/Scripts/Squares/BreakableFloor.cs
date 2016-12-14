using UnityEngine;
using System.Collections;
using System;

/**
 * Feld, welches nach n mal betreten, zerstört wird.
 * 
 * Verantwortung:
 * - Weiss, wann es zerstört werden muss.
 * - Zerstört sich selbst und lässt sich ab diesem Zeitpunkt nicht mehr betreten.
 */
public class BreakableFloor : Floor, IRestart
{
    private int startSteps;
    private int steps;
    private bool broken;

    private Vector3 startPosition;

    public BreakableFloor(int x, int y, Board board, Vector3 position, int steps) : base(x, y, board, position)
    {
        this.startSteps = steps;
        this.steps = steps;
        this.broken = false;
        this.startPosition = gameObject.transform.position;
        Rigidbody rbody = gameObject.AddComponent<Rigidbody>();
        rbody.useGravity = false;
        rbody.isKinematic = true;
    }

    public override string stringRepresentation()
    {
        return startSteps.ToString();
    }

    public override bool askToLandHere(string direction, Square askingSquare)
    {
        bool returnTemp = false;
        if (!broken)
        {
            if (!hasPushableElement())
                returnTemp = true;
            else
                returnTemp = getMovableSquare().askToLandHere(direction, askingSquare);
        }

        if (returnTemp)
            countDown();

        return returnTemp;
    }

    private void countDown ()
    {
        --steps;
        if (steps <= 0)
            broken = true;
    }

    /**
     * Setzt neues 'movableSquare' auf dem Feld. Falls das Feld auf 'null' gesetzt wird und
     * das feld zerstört werden soll, wird es zerstört.
     */
    public override void setMovableSquare(MovableSquare movableSquare)
    {
        if (movableSquare == null && broken)
            breakFloor();

        base.setMovableSquare(movableSquare);
    }

    private void breakFloor()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void removeFromBoard()
    {
        gameObject.SetActive(false);
    }

    public void restoreStartSituation()
    {
        steps = startSteps;
        broken = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.transform.position = startPosition;
        gameObject.SetActive(true);
    }
}
