using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

/**
 * Bildet die Grundlage für alle Squares die sich in einem Sokoban bewegen können.
 * 
 * Verantwortung:
 * - Kennt die Startposition des Feldes auf dem Spielfeld
 * - Bearbeitet die Bewegungen
 */
public abstract class MovableSquare : Square, IRestart {
	
	private static readonly string STRING_REPRESENTATION = "M";

    protected bool onBoard;

    /* Startwerte */
    private int startX, startY;

	public MovableSquare(int x, int y, Board board, Vector3 position) : base(x, y, board, position) {
		startX = x;
		startY = y;
        onBoard = true;
	}
		
	public override string stringRepresentation() {
		return STRING_REPRESENTATION;
	}

	/**
	 * überprüft das aktive Spielfeld und fragt den Driver danach.
	 * @return aktives Spielfeld.
	 */
	protected Board activeBoard() {
		return driver.getActiveBoard ();
	}

	/**
	 * Bewegt das Objekt in die angegebene Richtung. Mögliche Richtungen sind
	 * left, right, up, down. Gibt Rückmeldung, ob die Bewegung erfolgt ist.
	 * @param direction Richtung in die das Objekt bewgt wird.
	 * @return true, falls die Bewegung erfolgreich war.
	 */
	public virtual bool move(string direction) {
		int xTemp = x;
		int yTemp = y;
		changeDirection (direction);
		switch (direction) {
		case "right":
			moveTo (x + 1, y, direction);
			break;
		case "left":
			moveTo (x - 1, y, direction);
			break;
		case "up":
			moveTo (x, y - 1, direction);
			break;
		case "down":
			moveTo (x, y + 1, direction);
			break;
		default:
			throw new InvalidMoveException ();
		}
		return !(xTemp == x && yTemp == y);
	}

	/**
	 * Hilfsprozedur zum verändern der Koordinaten des Objektes. Sorgt ausserdem dafür,
	 * dass das aktuelle und zukünftige Feld über die Bewegung des Objektes bescheid
	 * wissen. überprüft ausserdem, ob das Objekt überhaupt bewegt werden kann.
	 * @param newX neue X-Koordinate
	 * @param newy neue Y-Koordinate
	 * @param direction Richtung in der das Objekt bewegt werden soll.
	 */
	private void moveTo(int newX, int newY, string direction) {
        Square thisSquare = activeBoard().getSquareFromBoard(x, y);
        Square nextSquare = activeBoard().getSquareFromBoard(newX, newY);
        if (nextSquare.askToLandHere(direction, this) && onBoard)
        {
            x = newX;
            y = newY;
            movePositionInWorld();
            thisSquare.setMovableSquare(null);
            nextSquare.setMovableSquare(this);
        }
    }

    protected void leaveBoard()
    {
        onBoard = false;
        getSquare().setMovableSquare(null);
        x = 999;
        y = 999;
    }

    /**
	 * Entfernt dieses Objekt vom Spielfeld. Setzt Square#movableSquare vom Feld, auf 
	 * dem sich dieses Objekt befindet auf null und macht es ausserdem unsichtbar.
	 */
    public virtual void removeFromBoard() {
		Square actualSquare = activeBoard().getSquareFromBoard (x, y);
		actualSquare.setMovableSquare (null);
		gameObject.SetActive (false);
		Assert.IsFalse (actualSquare.hasBox ());
        Assert.IsFalse (actualSquare.hasPlayer());
        Assert.IsFalse (actualSquare.hasBreakableWall());
        Assert.IsFalse (actualSquare.hasBomb());
    }

	/**
	 * Setzt Objekt an seine Startposition zurück. Braucht dafür die Startkoordinaten. Setzt
	 * ausserdem Square@movableSquare auf this, für das Startfeld. Aktiviert das dazugehörige
	 * GameObject wieder.
	 */
	public virtual void restoreStartSituation() {
		Square startSquare = activeBoard().getSquareFromBoard (startX, startY);
		startSquare.setMovableSquare (this);
		x = startX;
		y = startY;
		movePositionInWorld ();
		gameObject.SetActive (true);
        onBoard = true;
        Assert.IsTrue(startSquare.getMovableSquare() != null);
	}

	/**
	 * Versucht sich zu bewegen. Wenn dies nicht geht, gibt die Anfrage false zurück.
	 * @param direction Richtung in dem die Bewegung erfolgen soll.
	 * @return true, wenn sich das Objekt bewegen kann.
	 */
	public override bool askToLandHere(string direction, Square askingSquare) {
        if (!"Player".Equals(askingSquare.GetType().Name))
            return false;
        else
		    return move (direction);
	}

	public Square getSquare() {
		return activeBoard().getSquareFromBoard (x, y);
	}

	public abstract void changeDirection(string newDirection);
}
