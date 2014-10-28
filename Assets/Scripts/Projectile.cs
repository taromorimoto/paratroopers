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
			Projectile proj = col.gameObject.GetComponent<Projectile>();
			if (proj != null) {
				Destroy(proj.gameObject);
				Destroy(gameObject);
				print("Projectile hit another projectile");
			}
		}
	}
}
