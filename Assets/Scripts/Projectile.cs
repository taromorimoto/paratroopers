using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public GameObject deathAnimation;

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

		if (col.gameObject.tag == "Bomb") {
			GameObject ps = (GameObject)Instantiate(deathAnimation);
			ps.transform.position = transform.position;
		}
	}
}
