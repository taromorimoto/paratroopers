using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject spawnedObject;
	public int spawnDelay = 1000;
	public int maxSpawned = 3;

	Dictionary<int, GameObject> enemies = new Dictionary<int, GameObject>();
	Stopwatch lastSpawn = new Stopwatch();
	
	void Start () {
		lastSpawn.Start();
	}
	
	void Update () {
		if (enemies.Count < maxSpawned && lastSpawn.ElapsedMilliseconds > spawnDelay) {
			GameObject enemy = (GameObject)Instantiate(spawnedObject);
			enemies.Add (enemy.GetInstanceID (), enemy);
			lastSpawn.Reset();
			lastSpawn.Start();
			print("Enemy spawned! Total:" + enemies.Count);
		}
	}
}
