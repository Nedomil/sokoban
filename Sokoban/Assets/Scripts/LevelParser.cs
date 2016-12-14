using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Text;
using System.IO;

/**
 * Analysiert eine Spielfelddatei und errichtet anhand dieser das Spielfeld. Die Rückgabe
 * erfolgt über die Methode LevelParser#parse in Form eines Board-Objektes.
 */
public class LevelParser {
	private string filePath;
	private int fileLength;
	private Vector3 position;

	private bool invariant() {
		return filePath != null && fileLength != 0 && fileLength > 0;
	}

	/**
	 * Speichert den Pfad der Datei und deren Länge ab.
	 * @params filePath Pfad der Datei, die eingelesen werden soll.
	 */
	public LevelParser(string filePath, Vector3 position) {
		this.filePath = filePath;
		this.fileLength = fileLengthCalculator ();
		this.position = position;
		Assert.IsTrue (invariant ());
	}

	/**
	 * Erstellt anhand des gespeicherten Dateipfades ein Board-Objekt.
	 * Stellt ausserdem sicher, dass das Board-Objekt alle Daten zur Verfügung
	 * steht. Siehe board#initializeData.
	 * @return Gibt das erstelte Board-Objekt zurück.
	 */
	public Board parse() {
		Assert.IsTrue (invariant ());
		Board board = new Board ();
		board.setBoard(boardMaker (board));
		board.initializeData ();
		Assert.IsTrue (invariant ());
		return board;
	}

	/**
	 * Erstellt ein doppeltverkettetes Array bestehend aus den gegebenen Feldern,
	 * welches das Spielfeld simuliert.
	 * @preconditions Die Variable filePath darf nicht null sein.
	 * @param boardObject Auf welchem gearbeitet wird.
	 * @return Doppeltverkettetes Array mit den Feldern des Spielses
	 */
	private Square[][] boardMaker (Board boardObject) {
		Assert.IsTrue (filePath != null);
		string[][] file = readFile ();
		Square[][] board = new Square[file.Length][];
		SquareFactory sf = new SquareFactory ();

		for (int i = 0; i < file.Length; i++) {
			board [i] = new Square[file [i].Length];
			for (int j = 0; j < file [i].Length; j++) {
				board [i] [j] = sf.makeSquare (file[i][j], i, j, boardObject, position);
			}
		}
		Assert.AreEqual (file.Length, board.Length);
		Assert.AreEqual (file [0].Length, board [0].Length);
		return board;
	}

	/**
	 * Vertauscht in einem doppeltverkettetem Array die x- und die y-Achse.
	 * @preconditions Das Eingabearray muss aus mindestens einer Zeile bestehen.
	 * @params board Doppeltverkettetes Array, welches angepasst werden soll.
	 * @return Doppeltverkettetes Array mit angepassten Einträgen.
	 */
	private string[][] rotateArray(string[][] board) {
		Assert.IsTrue (board != null);
		Assert.IsTrue (board [0] != null);
		string[][] newBoard = new string[board [0].Length][];

		for (int i = 0; i < board [0].Length; i++) {
			newBoard [i] = new string[board.Length];
			for (int j = 0; j < board.Length; j++) {
				newBoard [i] [j] = board [j] [i];
			}
		}
		Assert.AreEqual (board.Length, newBoard [0].Length);
		Assert.AreEqual (board [0].Length, newBoard.Length);
		return newBoard;
	}

	/**
	 * Liest eine Datei ein und schreibt jedes einzelne Zeichen in ein doppeltverkettetes
	 * Array. Jede Zeile kommt auch im Array auf eine neue Zeile.
	 * @return Eingelesene Datei als verkettetes Array.
	 */
	private string[][] readFile() {
		string line;
		string[][] board = new string[fileLength][];
		int lineCounter = 0;

		string fileAsString = Resources.Load<TextAsset>(filePath).text;

		StringReader reader = new StringReader (fileAsString);

		using (reader) 
		{
			do 
			{
				line = reader.ReadLine ();
				if (line != null) {
					board[lineCounter] = cutLine(line);
					lineCounter++;
				}
			} while (line != null);

			reader.Close ();
		}
		return rotateArray(board);
	}
		
	/**
	 * Schneidet einen String auseinander und schreibt jedes Symbol als einzelner
	 * Eintrag eines Arrays. Der String darf nicht null sein.
	 * @param line String der geschnitten werden soll. Darf nicht null sein.
	 * @return Array mit den Symbolen des Eingabestrings als Einträge.
	 */
	private string[] cutLine(string line) {
		Assert.IsTrue (line != null);
		int lineLength = line.Length;	
		string[] lineAsArray = new string[lineLength];
		for (int i = 0; i < lineLength; i++) {
			lineAsArray [i] = line [i].ToString();
		}
		return lineAsArray;
	}

	/**
	 * Berechnet die Zeilenlänge der Datei, die unter LevelParser#filePath gespeichert wurde.
	 * @return Zeilenlänge der Datei, die unter LevelParser#filepath gespeichert wurde.
	 */
	private int fileLengthCalculator() {
		string fileAsString = Resources.Load<TextAsset>(filePath).text;
		int counter = 1;
		foreach (char c in fileAsString)
			if (c == '\n')
				counter++;		
		return counter;
	}

	public string getFilePath (){
			return filePath;
	}

	public int getFileLength () {
		return fileLength;
	}
}
