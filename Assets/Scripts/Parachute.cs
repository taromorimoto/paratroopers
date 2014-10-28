using UnityEngine;
using System.Collections;

public class Parachute : MonoBehaviour {

	void Start() {
	
	}
	
	void Update() {
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Projectile") {
			Paratrooper paratrooper = gameObject.GetComponentInParent<Paratrooper>();
			paratrooper.ParachuteHit();
		}
	}
	
	
}
