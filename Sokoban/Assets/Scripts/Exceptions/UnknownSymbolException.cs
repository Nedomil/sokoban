using UnityEngine;
using System.Collections;

public class UnknownSymbolException : System.Exception {

	public UnknownSymbolException() {
		Debug.Log ("Level file contains unknown symbols. Fix it ore change it to a valid Level");
	}
}
