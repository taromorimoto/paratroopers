using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour {

	public Vector3 velocity = new Vector3(-0.1f, 0, 0);
	int spriteFlip = -90;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (50, 42);
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.x < -50 || transform.position.x > 50) {
			velocity.x *= -1;
			spriteFlip *= -1;
			transform.localEulerAngles = new Vector3(1, spriteFlip, 1);
		}
		transform.localEulerAngles = new Vector3(0, 90+spriteFlip, 0);

		transform.position += velocity;
		
	}
}
