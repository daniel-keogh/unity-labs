using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 6.0f;
    [SerializeField] private Bullet bulletPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        // instantiate bullet, give it the same position as the player
        // and a velocity
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = this.transform.position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * bulletSpeed;
    }
}
