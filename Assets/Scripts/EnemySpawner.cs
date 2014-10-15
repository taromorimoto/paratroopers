using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject spawnedObject;
	public int minSpawnCount = 2;
	public int maxSpawnCount = 6;
	public int minSpawnDelay = 1000;
	public int maxSpawnDelay = 5000;
	public int maxWaveEnemyCount = 10;
	public bool active = true;

	int waveEnemyCount = 0;
	int spawnDelay = 1000;
	int maxSpawned = 3;
	System.Random r = new System.Random();

	Dictionary<int, GameObject> enemies = new Dictionary<int, GameObject>();
	Stopwatch lastSpawn = new Stopwatch();
	
	void Start () {
		lastSpawn.Start();
	}

	void startWave() {
		waveEnemyCount = 0;
	}
	
	void Update () {
		if (waveEnemyCount >= maxWaveEnemyCount) {
			print("Wave disabled. waveEnemyCount:" + waveEnemyCount);
			active = false;
			// Start the other wave
			// ...
		}

		if (active && enemies.Count < maxSpawned && lastSpawn.ElapsedMilliseconds > spawnDelay) {

			GameObject enemy = (GameObject)Instantiate(spawnedObject);
			FlyingEnemy flyingEnemy = enemy.GetComponent<FlyingEnemy>();

			if (r.Next(0, 2) == 0)
				flyingEnemy.startFromLeft();
			else
				flyingEnemy.startFromRight();

			enemies.Add(enemy.GetInstanceID(), enemy);
			waveEnemyCount++;

			spawnDelay = r.Next(minSpawnDelay, maxSpawnDelay);
			maxSpawned = r.Next(minSpawnCount, maxSpawnCount);
			lastSpawn.Reset();
			lastSpawn.Start();
			print("Enemy spawned! Active:" + enemies.Count + " waveEnemyCount:" + waveEnemyCount);
		}

		List<int> ids = new List<int>(enemies.Keys);

		foreach (int id in ids) {
			if (enemies[id].GetComponent<FlyingEnemy>().destroyed) {
				Destroy(enemies[id]);
				enemies.Remove(id);
			}
		}
	}
}
