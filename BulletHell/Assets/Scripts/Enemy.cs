using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnEnable()
    {
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.AddEnemy();
    }

    void OnDisable()
    {
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.RemoveEnemy();
    }
}