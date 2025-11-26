using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    float nextFireTime;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = b.GetComponent<Bullet>();
        bullet.SetDirection(Vector2.down);
        bullet.SetAsEnemyBullet(true);
    }
}