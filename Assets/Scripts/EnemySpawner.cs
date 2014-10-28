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
	public bool onlyOneDirection = false;
	public bool waveActive = true;
	public bool gameStarted = false;

	int waveEnemyCount = 0;
	int spawnDirection = 0;
	int spawnDelay = 1000;
	int maxSpawned = 3;
	System.Random r = new System.Random();
	bool gameOver = false;


	Dictionary<int, GameObject> enemies = new Dictionary<int, GameObject>();
	Stopwatch lastSpawn = new Stopwatch();
	
	void Start () {
		lastSpawn.Start();
		UpdateSpawnDirection();
	}

	// Event: Toggle this wave activity
	void ToggleWave() {
		waveActive = !waveActive;
		waveEnemyCount = 0;
		UpdateSpawnDirection();
	}

	// Either choose one direction for this wave (Planes) or have random direction for each enemy (Helicopter).
	void UpdateSpawnDirection() {
		spawnDirection = onlyOneDirection ? r.Next(0, 2) : -1;
	}

	public void GameOver() {
		gameOver = true;
	}
	
	void Update() {

		if (waveActive && !gameOver && gameStarted) {

			// Check if all enemies for this wave has been instantiated
			if (waveEnemyCount < maxWaveEnemyCount) {
				// Check is more enemies can be instantiated at the same time.
				// Also check that enough time has passed since last enemy spawning.
				if (enemies.Count < maxSpawned && lastSpawn.ElapsedMilliseconds > spawnDelay) {
					SpawnEnemy();
				}
			} else {
				if (enemies.Count == 0) {
					// No enemies left to spawn and no enemies alive, so toggle to another wave.
					SendMessage("ToggleWave");
				}
			}

			// Check if enemy instances in this wave should be Destroyed
			List<int> ids = new List<int>(enemies.Keys);
			foreach (int id in ids) {
				if (enemies[id].GetComponent<FlyingEnemy>().destroyed) {
					Destroy(enemies[id]);
					enemies.Remove(id);
				}
			}
		}
	}

	void ChooseEnemyDirection(FlyingEnemy flyingEnemy) {
		int dir = spawnDirection > -1 ? spawnDirection : r.Next(0, 2);
		if (dir == 0)
			flyingEnemy.startFromLeft();
		else
			flyingEnemy.startFromRight();
	}

	void SpawnEnemy() {
		// Create an enemy
		GameObject enemy = (GameObject)Instantiate(spawnedObject);
		FlyingEnemy flyingEnemy = enemy.GetComponent<FlyingEnemy>();
		ChooseEnemyDirection(flyingEnemy);

		// Keep track of alive enemies
		enemies.Add(enemy.GetInstanceID(), enemy);
		waveEnemyCount++;
		
		spawnDelay = r.Next(minSpawnDelay, maxSpawnDelay);
		maxSpawned = r.Next(minSpawnCount, maxSpawnCount);
		lastSpawn.Reset();
		lastSpawn.Start();
		print("Enemy spawned! Active enemies:" + enemies.Count + " waveEnemyCount:" + waveEnemyCount);
	}
}
