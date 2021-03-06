﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;


/**
 * Kennt alle wichtigen Eigenschaften des Spieles und erlaubt es dem Spieler sich darauf zu bewegen.
 * 
 * Verantwortung:
 * - Kennt Spieler, Board, die eigene Position im All
 * - Weiss, wo die Kamera stehen muss, damit das Spielfeld gänzlich angezeigt wird.
 * - Befielt dem Spieler sich zu bewegen.
 * - Weiss, ob das Sokoban bereits gelöst wurde und kann es signalisieren.
 */
public class Sokoban {
	private Board board;
	private Player player;
	private Vector3 positionCenter;
	private Vector3 position;
	private Driver driver;

	//true falls das Spiel gelöst wurde.
	private bool solved;
	private Trophy trophy;

	/*Startsituation der Hauptkamera*/
	public Vector3 mainCamStartPosition;
	public Vector3 mainCamStartForward;

	/**
	 * Erstellt mit LevelParser das Spielfeld. Speichert Spieler und Spielfeld ab und
	 * setzt Sokoban#inSpace auf false, da sich der Anfang nicht im All abspielt. Speichert ausserdem
	 * die Ausgangslage der Hauptkamera ab.
	 * @param position des Sokobans in der Welt
	 * @param path der Sokoban-Textdatei. 
	 */
	public Sokoban (Vector3 position, string path, bool createPlayer, Driver driver) {
		this.position = position;
		LevelParser lp = new LevelParser(path, position);
		this.board = lp.parse ();
		readPositionCenterInWorld ();
		this.driver = driver;
		this.solved = false;
		trophy = new Trophy (board, solved);
	}

	private void destroyPlayer() {
		player.getGameObject ().SetActive (false);
		player = null;
	}

	private void readPositionCenterInWorld() {
		int xLength = board.getBoard ().Length;
		int yLength = board.getBoard () [0].Length;
		int xIndexCenter = (int)(xLength / 2);
		int yIndexCenter = (int)(yLength / 2);
		Square squareCenter = board.getBoard () [xIndexCenter] [yIndexCenter];
		positionCenter = squareCenter.getGameObject ().transform.position;
	}

	/**
	 * Hört zu und reagiert, falls eine Bewegungstaste gedrückt wird damit, dass er den Spieler
	 * dementsprechend bewegt.
	 */
	public void sokobanListener() {
		if (!board.gameIsOver () && !driver.gamePaused()) {
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				player.move ("right");
			}

			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				player.move ("left");
			}

			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				player.move ("up");
			}

			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				player.move ("down");
			}
		} else if (!driver.gamePaused()) {
			solved = true;
			trophy.setSolved (true);
			Debug.Log ("Game is Over! You won!");
		}
	}

	public Vector3 getPositionCenterSokoban() {
		return positionCenter;
	}

	public Board getBoard() {
		return board;
	}

	public Player getPlayer() {
		return player;
	}

	public override string  ToString() {
		return "Ich bin das Spiel an Position " + position + ".";
	}

	public void setPlayer(Player player) {
		this.player = player;
	}

	public void setMainCamStartPosition(Vector3 v) {
		mainCamStartPosition = v;
	}

	public void setMainCamStartForward(Vector3 v) {
		mainCamStartForward = v;
	}

	public void setSolved(bool solved) {
		this.solved = solved;
	}

	public bool getSolved() {
		return solved;
	}
}
