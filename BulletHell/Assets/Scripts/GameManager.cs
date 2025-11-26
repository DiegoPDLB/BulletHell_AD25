using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public float phase1Duration = 30f;

    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

    bool bossSpawned = false;

    void Update()
    {
        if (!bossSpawned && Time.timeSinceLevelLoad >= phase1Duration)
        {
            Debug.Log("Tiempo cumplido, intentando spawnear boss...");
            StartBossPhase();
        }
    }
    void StartBossPhase()
    {
        bossSpawned = true;

        if (enemySpawner != null)
        {
            enemySpawner.enabled = false;
            Debug.Log("EnemySpawner deshabilitado");
        }
        else
        {
            Debug.LogWarning("enemySpawner es NULL en GameManager");
        }

        if (bossPrefab != null && bossSpawnPoint != null)
        {
            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            Debug.Log("Boss instanciado en " + bossSpawnPoint.position);
        }
        else
        {
            if (bossPrefab == null) Debug.LogWarning("bossPrefab es NULL en GameManager");
            if (bossSpawnPoint == null) Debug.LogWarning("bossSpawnPoint es NULL en GameManager");
        }
    }
}