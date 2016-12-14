using UnityEngine;
using System.Collections;

/**
 * Ermöglicht es, Cooldowns zu setzen.
 * 
 * Verantwortung:
 * - besitzt einen eigenen counter.
 * - kann den Timer zurücksetzen, neu starten und weiss, ob er gestartet wurde.
 */
public class SpeedCooldownTimer {
	public int cooldownTimeStart;
	private int cooldownTime;
	private bool active;

	public SpeedCooldownTimer(int cooldownTimeStart) {
		this.cooldownTimeStart = cooldownTimeStart;
		this.cooldownTime = 0;
		this.active = false;
	}
	
	/**
	 * Verringert cooldownTime pro frame um eins.
	 */
	public void Update () {
		if (cooldownTime > 0 && hasStarted ()) {
			--cooldownTime;
		}
	}

	/**
	 * Setzt den timer zurück.
	 */
	public void reset() {
		cooldownTime = cooldownTimeStart;
	}

	/**
	 * Startet den Timer neu, dabei wird er zurückgesetzt.
	 */
	public void start() {
		active = true;
		reset ();
	}

	/**
	 * Unterbricht dem Timer und setzt ihn auf deaktiviert.
	 */
	public void stop() {
		active = false;
	}

	/**
	 * überprüft, ob der Timer aktiv ist.
	 * @reutrn true, wenn der Timer aktiv ist.
	 */
	public bool hasStarted() {
		return active;
	}

	public int getCooldownTime() {
		return cooldownTime;
	}
}
