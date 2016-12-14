using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;


/**
 * Steuert die Hauptfunktionen des Spielers, wenn er sich im Weltall befindet.
 * 
 * Verantwortung:
 * - Kennt die physikalischen Werte des Raumschiffes.
 * - Erlaubt es dem Raumschiff, sich zu bewegen (funktioniert nur im All).
 * - Weiss, ob sich der Spieler im All befindet.
 */
public class SpacePlayer : MonoBehaviour{
	private static readonly int SPEED_COOLDOWN = 20;
	private static readonly float MAX_PITCH = 5;
	public static readonly float MAX_SPEED = 0.05f;
	public static readonly float ACCELERATION = 0.0001f;
	public static readonly float NEVATIVE_ACCELERATION = 0.0002f;
	public static readonly float MOBILITY_FACTOR = 15; 			// Zu speed multipliziert um Rotation zu verbessern.
	public static readonly float ROTATING_SPEED = 1;
	public float speed2 = 1;

	public Driver driver;

	private Rigidbody rbody;
	public Vector3 position;
	public SpeedCooldownTimer speedCooldownTimer;
	private float speed;
	private bool active;

	void Start() {
		this.position = gameObject.transform.position;
		this.speedCooldownTimer = new SpeedCooldownTimer(SPEED_COOLDOWN);
		this.speed = 0;

		setUpRigidbody ();
		deactivateRigidbody ();
	}

	void Update() {
		isInSpaceUpdate ();
		if (active) {
			speedUpdate ();

			if (Input.GetKey (KeyCode.W)) {
				moveForward ();
			}

			if (Input.GetKey (KeyCode.A)) {
				rotateLeft ();
			}

			if (Input.GetKey (KeyCode.D)) {
				rotateRight ();
			}

			if (Input.GetKey (KeyCode.Space)) {
				moveUp ();
			}

			if (Input.GetKey (KeyCode.LeftShift)) {
				moveDown ();
			}

			makeVelocityZero ();
		}
	}

	private void makeVelocityZero() {
		Rigidbody rbody = gameObject.GetComponent<Rigidbody> ();
		rbody.velocity = Vector3.zero;
		rbody.angularVelocity = Vector3.zero;
	}

	/**
	 * überprüft, ob sich der Spieler im All befindet. Wenn ja ist 'SpacePlayer#active' true.
	 */
	private void isInSpaceUpdate() {
		bool activeTemp = active;
		active = driver.isInSpace ();
		if (activeTemp != active) {
			if (active)
				activateRigidbody ();
			else
				deactivateRigidbody ();
		}
	}

	private void setUpRigidbody() {
		Assert.IsTrue (gameObject != null);
		Rigidbody rbody = gameObject.AddComponent<Rigidbody> ();
		rbody.useGravity = false;
	}

	private void deactivateRigidbody() {
		driver = Camera.main.GetComponent<Driver> ();
		gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		active = false;
		driver.getPlayer ().movePositionInWorld ();
	}

	private void activateRigidbody() {
		gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		active = true;
	}

	private void moveForward() {
		speedCooldownTimer.start ();
		Rigidbody rb = gameObject.GetComponent<Rigidbody> ();
		rb.AddForce (Vector3.forward * speed2);
		fixCameraPosition();
	}

	private void moveBack() {
		speedCooldownTimer.start ();
		gameObject.transform.Translate (Vector3.back * speed);
		fixCameraPosition();
	}

	private void moveLeft() {
		speedCooldownTimer.start ();
		//gameObject.transform.Rotate (Vector3.forward * speed * MOBILITY_FACTOR);
        gameObject.transform.Rotate(0, ROTATING_SPEED, 0);
        fixCameraPosition();
	}

	private void moveRight() {
		speedCooldownTimer.start ();
		//gameObject.transform.Rotate (- Vector3.forward * speed * MOBILITY_FACTOR);
        gameObject.transform.Rotate(0, - ROTATING_SPEED, 0);
        fixCameraPosition();
	}

	private void moveUp() {
		if (pitchPlayer () < MAX_PITCH)
        {
            gameObject.transform.Rotate (-ROTATING_SPEED/2, 0, 0);
            fixCameraPosition ();
		}
	}

	private void moveDown() {
		if (pitchPlayer () > - MAX_PITCH)
        {
            gameObject.transform.Rotate (ROTATING_SPEED/2, 0, 0);
			fixCameraPosition ();
		}
	}

	private void rotateRight() {
		gameObject.transform.RotateAround (gameObject.transform.position, Vector3.up, ROTATING_SPEED);
		fixCameraPosition();
	}

	private void rotateLeft() {
		gameObject.transform.RotateAround (gameObject.transform.position, Vector3.up, - ROTATING_SPEED);
		fixCameraPosition();
	}

	public void speedUpdate() {
		speedCooldownTimer.Update ();
		updateSpeed ();
		if (speed > 0 && !Input.GetKeyDown (KeyCode.W)) {
			gameObject.transform.Translate (Vector3.forward * speed);
			fixCameraPosition ();
		}
	}

	private void updateSpeed() {
		float speedTemp = speed;
		if (speedCooldownTimer.getCooldownTime() > 0)
			speedTemp += ACCELERATION;
		else
			speedTemp -= NEVATIVE_ACCELERATION;

		if (speedTemp > MAX_SPEED)
			speedTemp = MAX_SPEED;

		if (speedTemp < 0)
			speedTemp = 0;

		speed = speedTemp;
	}

	private void fixCameraPosition() {
		Camera.main.transform.position = gameObject.transform.position - 2 * Camera.main.transform.forward;
		Camera.main.transform.forward = gameObject.transform.forward;
		Camera.main.transform.Rotate(30, 0, 0);
	}

	public void resetSpeed() {
		speed = 0;
	}

	public float getSpeed() {
		return speed;
	}

	/**
	 * Berechnet die Steigung des Spielers.
	 * @return Steigung des Spielers
	 */
	public float pitchPlayer() {
		Vector3 v = gameObject.transform.forward;
		return (float) (v.y / System.Math.Sqrt(System.Math.Pow(v.x, 2) + System.Math.Pow(v.z, 2)) );
	}

	/**
	 * Aktiviert oder deaktiviert den SpacePlayer mittels 'SpacePlayer#active'.
	 * @param boolean true, wenn der Spieler aktiviert werden soll.
	 */
	public void setActive (bool boolean) {
		this.active = boolean;
	}
}
