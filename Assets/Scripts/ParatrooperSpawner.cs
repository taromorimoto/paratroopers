using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class ParatrooperSpawner : Spawner {

	bool canDrop = false;


	void Update () {
		if (canDrop)
			UpdateSpawning();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "DropArea") canDrop = true;
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "DropArea") canDrop = false;
	}

}
