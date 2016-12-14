using UnityEngine;
using System.Collections;

public class InvalidMoveException : System.Exception {

	public InvalidMoveException() {
		Debug.Log ("Unauthorized move. Please use a valid direction.");
	}
}
