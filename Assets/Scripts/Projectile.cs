using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

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
				Destroy(gameObject, 0.8f);
				gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
				print("Projectile hit!");
			}
		}

	}
}
