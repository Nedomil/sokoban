using UnityEngine;
using System.Collections;

/**
 * Muss bei Objekten implementiert sein, die ihren Zustand oder ihre Position
 * ändern. Diese Objekte sollen jeweils in den Anfangszustand zurückgesetzt
 * werden können.
 */
public interface IRestart {

	/**
	 * Entfernt das Objekt temporär vom Spielfeld, damit keine Konflikte mit
	 * anderen Objekten entstehen.
	 */
	void removeFromBoard();

	/**
	 * Setzt Zustand und Position eines Objektes zurück.
	 */
	void restoreStartSituation();
}
