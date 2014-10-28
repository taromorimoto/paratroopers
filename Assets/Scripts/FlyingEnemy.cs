using UnityEngine;
using System.Collections;

public class FlyingEnemy : MonoBehaviour {

	public float velocity = 0.1f;
	public int startFromRightAltitude = 45;
	public int startFromLeftAltitude = 42;
	public bool destroyed = false;
	public GameObject deathAnimation;

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

	public void hit() {
		print ("FlyingObject hit! Destroying and instatiating death animation.");

		destroyed = true;

		// Death explosion animation
		GameObject ps = (GameObject)Instantiate(deathAnimation);
		ps.transform.position = transform.position;
		ps.GetComponent<Rigidbody2D>().AddForce(transform.right * 2000.0f * velocity);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Projectile") {
			hit();
            Destroy(other.gameObject);
		}
	}
	
	
}
