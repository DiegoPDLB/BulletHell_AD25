using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float lifeTime = 3f;

    Vector2 direction = Vector2.up;
    bool isEnemy = false;

    void Start()
    {
        if (BulletManager.Instance != null)
            BulletManager.Instance.AddBullet(isEnemy);

        Destroy(gameObject, lifeTime);
    }

    void OnDestroy()
    {
        if (BulletManager.Instance != null)
            BulletManager.Instance.RemoveBullet(isEnemy);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    public void SetAsEnemyBullet(bool value)
    {
        isEnemy = value;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnemy && other.CompareTag("Enemy"))
        {
            EnemyHealth hp = other.GetComponent<EnemyHealth>();
            if (hp != null)
            {
                hp.TakeDamage(1);
            }

            Destroy(gameObject);
        }

       
    }
}