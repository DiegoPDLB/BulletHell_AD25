using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    public int enemies;
    public TMP_Text enemiesText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        UpdateUI();
    }

    public void AddEnemy()
    {
        enemies++;
        UpdateUI();
    }

    public void RemoveEnemy()
    {
        enemies = Mathf.Max(0, enemies - 1);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (enemiesText != null)
            enemiesText.text = "Enemies: " + enemies;
    }
}