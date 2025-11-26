using UnityEngine;
using TMPro;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; private set; }

    public int playerBullets;
    public int enemyBullets;

    public TMP_Text playerBulletsText;
    public TMP_Text enemyBulletsText;

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

    public void AddBullet(bool isEnemy)
    {
        if (isEnemy) enemyBullets++;
        else playerBullets++;

        UpdateUI();
        Debug.Log($"ADD bullet. Player: {playerBullets}, Enemy: {enemyBullets}");
    }

    public void RemoveBullet(bool isEnemy)
    {
        if (isEnemy) enemyBullets = Mathf.Max(0, enemyBullets - 1);
        else playerBullets = Mathf.Max(0, playerBullets - 1);

        UpdateUI();
        Debug.Log($"REMOVE bullet. Player: {playerBullets}, Enemy: {enemyBullets}");
    }

    void UpdateUI()
    {
        if (playerBulletsText != null)
            playerBulletsText.text = "Player Bullets: " + playerBullets;

        if (enemyBulletsText != null)
            enemyBulletsText.text = "Enemy Bullets: " + enemyBullets;
    }
}