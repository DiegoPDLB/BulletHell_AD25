using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Vector2 spawnXRange = new Vector2(-7f, 7f);
    public float spawnY = 6f;
    public float spawnInterval = 1.5f;
    public float phaseDuration = 30f;

    float timer;
    float elapsed;

    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= phaseDuration)
        {
            enabled = false;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        float x = Random.Range(spawnXRange.x, spawnXRange.y);
        Vector3 pos = new Vector3(x, spawnY, 0f);
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}