using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoundaryCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        var bullet = collider.GetComponent<Bullet>();
        if (bullet)
        {
            Destroy(bullet.gameObject);
        }

        var enemy = collider.GetComponent<Enemy>();
        if (enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}
