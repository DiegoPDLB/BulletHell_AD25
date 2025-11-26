using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 6f;
    public float slowSpeed = 2.5f;

    public Vector2 minBounds = new Vector2(-8f, -4.5f);
    public Vector2 maxBounds = new Vector2(8f, 4.5f);

    public GameObject playerBulletPrefab; 
    public Transform firePoint;
    public float fireRate = 0.15f;


    float nextFireTime;

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        Vector2 dir = Vector2.zero;

        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed)  dir.x -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) dir.x += 1f;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed)  dir.y -= 1f;
        if (kb.wKey.isPressed || kb.upArrowKey.isPressed)    dir.y += 1f;

        if (dir.sqrMagnitude > 1f)
            dir = dir.normalized;

        bool slow = kb.leftShiftKey.isPressed || kb.rightShiftKey.isPressed;
        float speed = slow ? slowSpeed : normalSpeed;

        transform.position += (Vector3)(dir * speed * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void HandleShooting()
    {
        var kb = Keyboard.current;

        if (kb == null) return;

        if (kb.zKey.isPressed && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject b = Instantiate(playerBulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = b.GetComponent<Bullet>();
        bullet.SetDirection(Vector2.up);
        bullet.SetAsEnemyBullet(false);
    }
}