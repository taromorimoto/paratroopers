using UnityEngine;
using System.Collections;

public class FlyingEnemy : MonoBehaviour {

	public float velocity = 0.1f;
	public int startFromRightAltitude = 45;
	public int startFromLeftAltitude = 42;
	public bool destroyed = false;

	FlipObject flip;

	void Awake () {
		flip = gameObject.GetComponent<FlipObject>();
	}

	public void startFromRight() {
		flip.facingRight = false;
		transform.position = new Vector3 (50, startFromRightAltitude);
		velocity *= -1;
	}
	
	public void startFromLeft() {
		flip.facingRight = true;
		transform.position = new Vector3 (-50, startFromLeftAltitude);
	}
	
	void Update () {

		if (transform.position.x < -50 || transform.position.x > 50) {
			destroyed = true;
		} else {
			transform.position += new Vector3(velocity, 0);
		}

	}
}
