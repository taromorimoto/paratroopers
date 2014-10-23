using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Spawner : MonoBehaviour {
	
	public GameObject spawnedObject;
	public int spawnDelayMin = 1000;
	public int spawnDelayMax = 4000;
	public int spawnCount = -1; // -1 for unlimited

	Stopwatch lastSpawn = new Stopwatch();
	int spawnDelay;
	
	public void Start () {
		ResetSpawning ();
	}
	
	void ResetSpawning() {
		lastSpawn.Reset ();
		lastSpawn.Start ();
		spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);
	}

	public virtual void Spawned(GameObject spawned) {
	}

	void Update() {
		UpdateSpawning();
	}

	protected void UpdateSpawning () {

		if (spawnCount != 0 && spawnedObject && lastSpawn.ElapsedMilliseconds > spawnDelay) {
			GameObject spawned = (GameObject)Instantiate (spawnedObject);
			spawned.transform.position = gameObject.transform.position;

			Spawned(spawned);

			if (spawnCount > 0) spawnCount--;

			ResetSpawning();
		}
	}


}
