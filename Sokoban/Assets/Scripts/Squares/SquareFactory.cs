using UnityEngine;
using System.Collections;

public class SquareFactory {
	private const string PLAYER_SYMBOL = "P";
	private const string FLOOR_SYMBOL = " ";
	private const string BOX_SYMBOL = "B";
	private const string GOAL_SYMBOL = "G";
	private const string WALL_SYMBOL = "#";
    private const string BOMB_SYMBOL = "O";
    private const string BREAKABLE_WALL_SYMBOL = "%";


	/**
	 * Erstellt die richtige Art Feld anhand des eingelesenen Symboles und gibt
	 * dieses zurück. Das eingegebene Symbol muss eines der aufgelisteten sein.
	 * Wirft eine Exception,falls das Symbol nicht existiert.
	 * "#": Wand
	 * " ": Weg
	 * "P":	Spieler
	 * "B": Box
	 * "G":	Zielfeld
	 * @precondition Das eingegebene Symbol muss existieren.
	 * @params symbol welches eingelesen werden soll. Muss existieren.
	 * @params x Koordinate des Feldes.
	 * @params y Koordinate des Feldes.
	 * @param board auf welchem gearbeitet wird.
	 * @return Das erstellte neue Feld.
	 */
	public Square makeSquare(string symbol, int x, int y, Board board, Vector3 position) {
		Square square;
		switch (symbol) {
		    case FLOOR_SYMBOL:
			    square = new Floor (x, y, board, position);
    			break;
    		case WALL_SYMBOL:
    			square = new Wall (x, y, board, position);
    			break;
            case BREAKABLE_WALL_SYMBOL:
                BreakableWall bw = new BreakableWall(x, y, board, position);
                square = new Floor(x, y, board, position);
                square.setMovableSquare(bw);
                break;
            case PLAYER_SYMBOL:
			    Player player = new Player (x, y, board, position);
			    square = new Floor (x, y, board, position);
		    	square.setMovableSquare (player);
		    	break;
	    	case BOX_SYMBOL:
		    	Box box = new Box (x, y, board, position);
		    	square = new Floor (x, y, board, position);
	    		square.setMovableSquare (box);
	    		break;
		    case GOAL_SYMBOL:
	    		square = new Goal (x, y, board, position);
		    	break;
            case BOMB_SYMBOL:
               Bomb bomb = new Bomb(x, y, board, position);
               square = new Floor(x, y, board, position);
               square.setMovableSquare(bomb);
               break;
            case "1":
                square = new BreakableFloor(x, y, board, position, 1);
                break;
            case "2":
                square = new BreakableFloor(x, y, board, position, 2);
                break;
            case "3":
                square = new BreakableFloor(x, y, board, position, 3);
                break;
            case "4":
                square = new BreakableFloor(x, y, board, position, 4);
                break;
            case "5":
                square = new BreakableFloor(x, y, board, position, 5);
                break;
            case "6":
                square = new BreakableFloor(x, y, board, position, 6);
                break;
            case "7":
                square = new BreakableFloor(x, y, board, position, 7);
                break;
            case "8":
                square = new BreakableFloor(x, y, board, position, 8);
                break;
            case "9":
                square = new BreakableFloor(x, y, board, position, 9);
                break;
            default:
			    throw new UnknownSymbolException ();
		}
		return square;
	}
}
