using UnityEngine;
using System.Collections;

/**
 * Für GUI-Elemente die angezeigt und versteckt werden müssen.
 */
public interface IGUIHide {
	/**
	 * Versteckt das Objekt.
	 */
	void hideObject ();

	/**
	 * Zeigt das Objekt an.
	 */
	void showObject (string reason);
}
