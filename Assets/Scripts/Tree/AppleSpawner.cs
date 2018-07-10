using UnityEngine;

public class AppleSpawner : MonoBehaviour {
    [System.Serializable]
    public struct SpawnedApple {
        public Transform prefab;
        public float chance;
    }

    public SpawnedApple[] apples;
    // relative spawn positions
    public Vector3[] spawnPositions;
    public float minSpawnInterval, maxSpawnInterval;

    float nextSpawn;
    float chanceSpan;

    public void Start() {
        SetNextSpawn();
        // calculate full chance span
        foreach (SpawnedApple apple in apples) {
            chanceSpan += apple.chance;
        }
    }

    public void Update() {
        if (Time.time >= nextSpawn) {
            SpawnApple(ChooseApple());
            SetNextSpawn();
        }
    }

    private void SetNextSpawn() {
        nextSpawn = Time.time + (float)RandomManager.random.NextDouble() * (maxSpawnInterval - minSpawnInterval) + minSpawnInterval;
    }

    SpawnedApple ChooseApple() {
        double r = RandomManager.random.NextDouble() * chanceSpan,
            c = 0;
        foreach (SpawnedApple apple in apples) {
            c += apple.chance;
            if (c >= r) return apple;
        }
        throw new System.Exception("bad coding, Caleb");
    }

    void SpawnApple(SpawnedApple apple) {
        // spawnPositions is relative to gameObject (tree), so add gameObject's position
        Vector3 position = gameObject.transform.position + spawnPositions[RandomManager.random.Next(0, spawnPositions.Length)];
        Transform inst = Instantiate(apple.prefab, position, Quaternion.identity);
        inst.parent = GameObject.FindGameObjectWithTag("Dynamic").transform;  // spawn as child of _Dynamic
    }
}
