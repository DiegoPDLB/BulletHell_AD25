using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float patternDuration = 10f;

    public float fireRatePattern1 = 0.35f;
    public float fireRatePattern2 = 0.2f;
    public float fireRatePattern3 = 0.08f;

    public int fanBulletCount = 5;
    public float fanSpreadAngle = 40f;

    float patternTimer;
    int currentPattern = 0;
    float nextShotTime;

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        patternTimer += Time.deltaTime;

        if (patternTimer >= patternDuration)
        {
            patternTimer = 0f;
            currentPattern = (currentPattern + 1) % 3;
        }

        switch (currentPattern)
        {
            case 0: PatternCircle();   break;
            case 1: PatternAimedFan(); break;
            case 2: PatternRain();     break;
        }
    }

    void PatternCircle()
    {
        if (Time.time < nextShotTime) return;
        nextShotTime = Time.time + fireRatePattern1;

        int bulletCount = 24;
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            SpawnBullet(transform.position, dir);
        }
    }

    void PatternAimedFan()
    {
        if (Time.time < nextShotTime) return;
        nextShotTime = Time.time + fireRatePattern2;

        if (player == null) return;

        Vector2 baseDir = (player.position - transform.position).normalized;

        if (fanBulletCount <= 1)
        {
            SpawnBullet(transform.position, baseDir);
            return;
        }

        float angleStep = fanSpreadAngle / (fanBulletCount - 1);
        float startAngle = -fanSpreadAngle / 2f;

        for (int i = 0; i < fanBulletCount; i++)
        {
            float angle = startAngle + angleStep * i;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * baseDir;
            SpawnBullet(transform.position, dir);
        }
    }

    void PatternRain()
    {
        if (Time.time < nextShotTime) return;
        nextShotTime = Time.time + fireRatePattern3;

        float x = Random.Range(-7f, 7f);
        Vector3 pos = new Vector3(x, transform.position.y, 0f);
        SpawnBullet(pos, Vector2.down);
    }

    void SpawnBullet(Vector3 pos, Vector2 dir)
    {
        if (bulletPrefab == null) return;

        GameObject b = Instantiate(bulletPrefab, pos, Quaternion.identity);
        Bullet bullet = b.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetDirection(dir);
            bullet.SetAsEnemyBullet(true);
        }
    }
}