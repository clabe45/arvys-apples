using UnityEngine;

public class CaterpillarSpawnController : MonoBehaviour {
    [System.Serializable]
    public struct Spawn {
        public Vector3 position;
        public Vector3 eulerAngles;
    }

    public GameObject caterpillar;
    public int startTime;
    public float minSpawnInterval, maxSpawnInterval;
    public Spawn[] spawnLocations;

    float nextSpawn;

	void Update () {
        if (Time.time < startTime) return;

        if (Time.time >= nextSpawn) {
            SpawnCat();
            SetNextSpawn();
        }
	}

    private void SetNextSpawn() {
        nextSpawn = Time.time + (float)RandomManager.random.NextDouble() * (maxSpawnInterval - minSpawnInterval) + minSpawnInterval;
    }

    /// <summary>
    /// Spawns a caterpillar at a random location
    /// </summary>
    private void SpawnCat() {
        Spawn spawn = spawnLocations[RandomManager.random.Next(0, spawnLocations.Length - 1)];
        GameObject cat = Instantiate(caterpillar, GameObject.FindGameObjectWithTag("Dynamic").transform);   // make child of _Dynamic
        cat.transform.SetPositionAndRotation(spawn.position, Quaternion.Euler(spawn.eulerAngles));
    }
}
