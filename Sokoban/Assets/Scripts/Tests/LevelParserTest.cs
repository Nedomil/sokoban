using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class LevelParserTest {

	public LevelParserTest() {
	}

	public Board InitializationTest()
	{
		LevelParser lp = new LevelParser("Levels/level1", new Vector3 (0, 0, 0));
		Board board = lp.parse ();
		Assert.AreEqual("Levels/level1", lp.getFilePath());
		Assert.AreEqual(6, lp.getFileLength());
		Square[][] boardSquares = board.getBoard ();
		Assert.AreEqual (6, boardSquares [0].Length);   //is y-length
		Assert.AreEqual (7, boardSquares.Length);		//is x-length
		return board;
	}

	public void BoardHasRightSquares() {
		Board board = InitializationTest ();
		Assert.AreEqual ("Wall", board.getSquareFromBoard (6, 5).GetType().Name);
		Assert.AreEqual ("Floor", board.getSquareFromBoard (1, 1).GetType().Name);
		Assert.AreEqual ("Wall", board.getSquareFromBoard (0, 0).GetType().Name);
		Assert.AreEqual ("Player", board.getSquareFromBoard (1, 2).GetType().Name);
		Assert.AreEqual ("Box", board.getSquareFromBoard (3, 3).GetType().Name);
		Assert.AreEqual ("Goal", board.getSquareFromBoard (5, 4).GetType().Name);
	}

	public void BoardHasRightStringRepresentations() {
		Board board = InitializationTest ();
		Assert.AreEqual ("#", board.getSquareFromBoard (6, 5).stringRepresentation());
		Assert.AreEqual (" ", board.getSquareFromBoard (1, 1).stringRepresentation());
		Assert.AreEqual ("#", board.getSquareFromBoard (0, 0).stringRepresentation());
		Assert.AreEqual ("P", board.getSquareFromBoard (1, 2).stringRepresentation());
		Assert.AreEqual ("B", board.getSquareFromBoard (3, 3).stringRepresentation());
		Assert.AreEqual ("G", board.getSquareFromBoard (5, 4).stringRepresentation());
	}
}
