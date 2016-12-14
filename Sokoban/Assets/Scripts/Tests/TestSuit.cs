using UnityEngine;
using System.Collections;

public class TestSuit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LevelParserTest lpt = new LevelParserTest ();
		lpt.InitializationTest ();	
		lpt.BoardHasRightSquares ();
		lpt.BoardHasRightStringRepresentations ();
	}
}
