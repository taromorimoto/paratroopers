using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class ParatrooperSpawner : Spawner {

	public int spawnXMin = 9;
	public int spawnXMax = 38;

	bool canDrop = false;


	void Update () {
		UpdateSpawning ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "DropArea") canDrop = true;
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "DropArea") canDrop = false;
	}

}
