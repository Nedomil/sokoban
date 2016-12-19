using UnityEngine;
using System.Collections;

public class Board : IRestart {
	private Square[][] board;
	private float squareSize;
	private ArrayList boxes;
	private ArrayList goals;
    private ArrayList bombs;
    private ArrayList breakableWalls;
    private ArrayList breakableFloors;
	private int startPositionPlayerX, startPositionPlayerY;
	private Trophy trophy;

	public Board() {
	}

	/**
	 * überprüft, ob das Spiel bereits vorbei ist und gibt in diesem Fall true zurück.
	 * Benötigt dafür Board#goals und schaut, ob jedes Ziel-Feld von einer Kiste besetzt
	 * wird.
	 * @ return true, falls  das Spiel vorbei ist.
	 */
	public bool gameIsOver() {
		bool isOver = true;
		foreach (Goal goal in goals) {
			if (!goal.hasBox ()) {
				isOver = false;
			}
		}
        return isOver;
	}

	/**
	 * Säubert das Spielfeld von allen veränderbaren Objekten. Wird vebraucht, um das
	 * Spiel zurückzusetzen. Gegenstück zu Board#restoreStartSituation.
	 */
	public void removeFromBoard() {
		Driver driver = Camera.main.GetComponent<Driver> ();
		Player player = driver.getPlayer ();

		player.removeFromBoard ();
		foreach (Box box in boxes)
			box.removeFromBoard ();
        foreach (Bomb bomb in bombs)
            bomb.removeFromBoard();
        foreach (BreakableWall bw in breakableWalls)
            bw.removeFromBoard();
        foreach (BreakableFloor bf in breakableFloors)
            bf.removeFromBoard();
    }

	/**
	 * Fügt alle veränderbare Objekte wieder in der Startposition ein. Wird gebraucht,
	 * um das Spiel urückzusetzen. Gegenstück zu Board#removeFromBoard.
	 */
	public void restoreStartSituation() {
		Driver driver = Camera.main.GetComponent<Driver> ();
		Player player = driver.getPlayer ();

		player.restoreStartSituation ();
		foreach (Box box in boxes) {
			box.restoreStartSituation ();
		}
        foreach (Bomb bomb in bombs)
            bomb.restoreStartSituation();
        foreach (BreakableWall bw in breakableWalls)
            bw.restoreStartSituation();
        foreach (BreakableFloor bf in breakableFloors)
            bf.restoreStartSituation();
    }

	/**
	 * Sucht in Board#board nach dem Spieler und speichert diesen ab. Muss nach dem
	 * parser aufgerufen werden.
	 */
	private void initializePlayer() {
		for (int i = 0; i < board.Length; i++) {
			for (int j = 0; j < board[i].Length; j++) {
				if (board[i][j].hasPlayer()) {
					startPositionPlayerX = i;
					startPositionPlayerY = j;
					board [i] [j].removePlayer ();
					break;
				}
			}
		}
	}

	/**
	 * Sucht in Board#board nach den Zielfeldern und speichert diese ab. Muss nach dem
	 * parser aufgerufen werden.
	 */
	private void initializeGoals() {
		goals = new ArrayList ();
		for (int i = 0; i < board.Length; i++) {
			for (int j = 0; j < board[i].Length; j++) {
				if ("Goal".Equals(board[i][j].GetType().Name)) {
					goals.Add((Goal) board[i][j]);
				}
			}
		}
    }

    /**
	 * Sucht in Board#board nach den zerstörbare Felder und speichert diese ab. Muss nach dem
	 * parser aufgerufen werden.
	 */
    private void initializeBreakableFloors()
    {
        breakableFloors = new ArrayList();
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if ("BreakableFloor".Equals(board[i][j].GetType().Name))
                {
                    breakableFloors.Add((BreakableFloor)board[i][j]);
                }
            }
        }
    }

    /**
	 * Sucht in Board#board nach den zerbrechbaren Wanfeldern und speichert diese ab. Muss nach dem
	 * parser aufgerufen werden.
	 */
    private void initializeBreakableWalls() {
        breakableWalls = new ArrayList();
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j].hasBreakableWall())
                {
                    breakableWalls.Add((BreakableWall)board[i][j].getMovableSquare());
                }
            }
        }
    }

    /**
	 * Sucht in Board#board nach den Boxenfeldern und speichert diese ab. Muss nach dem
	 * parser aufgerufen werden.
	 */
    private void initializeBoxes() {
		boxes = new ArrayList ();
		for (int i = 0; i < board.Length; i++) {
			for (int j = 0; j < board[i].Length; j++) {
				if (board[i][j].hasBox()) {
					boxes.Add((Box) board[i][j].getMovableSquare ());
				}
			}
		}
    }

    /**
	 * Sucht in Board#board nach den Bombenfeldern und speichert diese ab. Muss nach dem
	 * parser aufgerufen werden.
	 */
    private void initializeBombs()
    {
        bombs = new ArrayList();
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j].hasBomb())
                {
                    bombs.Add((Bomb)board[i][j].getMovableSquare());
                }
            }
        }
    }

    /**
	 * Sucht in Board#board nach den wichtigen Daten und speichert diese ab. Muss nach
	 * dem parser aufgerufen werden.
	 */
    public void initializeData() {
		initializePlayer ();
		initializeGoals ();
		initializeBoxes ();
        initializeBombs();
        initializeBreakableWalls();
        initializeBreakableFloors();
    }

	public ArrayList getBoxes() {
		if (boxes == null)
			initializeBoxes ();
		return boxes;
	}

	public ArrayList getGoals() {
		if (goals == null)
			initializeGoals ();
		return goals;
    }

    public ArrayList getBombs()
    {
        if (bombs == null)
            initializeBombs();
        return bombs;
    }

    public ArrayList getBreakableWalls()
    {
        if (breakableWalls== null)
            initializeBreakableWalls();
        return breakableWalls;
    }

    public Square[][] getBoard() {
		return board;
	}

	public void setSquareSize(float squareSize) {
		this.squareSize = squareSize;
	}

	public float getSquareSize() {
		return squareSize;
	}

	public void setBoard(Square[][] board) {
		this.board = board;
	}

	public Square getSquareFromBoard(int x, int y) {
		return board [x] [y];
	}

	public float objectsSize(string path) {
		Vector3 temp = ((GameObject)Resources.Load (path)).GetComponent<MeshRenderer> ().bounds.size;
		return (temp.x + temp.y) / 2;
	}

	public int getStartPositionPlayerX() {
		return startPositionPlayerX;
	}

	public int getStartPositionPlayerY() {
		return startPositionPlayerY;
	}
}
