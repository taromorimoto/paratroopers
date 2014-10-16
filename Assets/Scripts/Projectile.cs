using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float aliveTimeAfterHit = 0.5f;

	void Update () {
		if (!renderer.isVisible) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col != null && col.gameObject) {
			FlyingEnemy enemy = col.gameObject.GetComponent<FlyingEnemy>();
			if (enemy != null) {
				enemy.hit();
				Destroy(gameObject, aliveTimeAfterHit);
				gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
				print("Projectile hit!");
			}
		}

	}
}
