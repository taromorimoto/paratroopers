using UnityEngine;
using System.Collections;

public class Bomber : Spawner {

	bool canBombToLeft = false;
	bool canBombToRight = false;
	FlyingEnemy enemy;

	void Start () {
		enemy = gameObject.GetComponent<FlyingEnemy>();
		base.Start();
	}

	void Update () {

		if (canBombToLeft && enemy.velocity < 0) {
			UpdateSpawning();
		}

		if (canBombToRight && enemy.velocity > 0) {
			UpdateSpawning();
		}
	}

	public override void Spawned(GameObject spawned) {
		spawned.transform.position += new Vector3 (0, -1.5f); 
		spawned.GetComponent<Rigidbody2D> ().AddForce (enemy.transform.right * enemy.velocity * 2000.0f);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "BombToLeftArea") canBombToLeft = true;
		if (other.name == "BombToRightArea") canBombToRight = true;
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.name == "BombToLeftArea") canBombToLeft = false;
		if (other.name == "BombToRightArea") canBombToRight = false;
	}

}
