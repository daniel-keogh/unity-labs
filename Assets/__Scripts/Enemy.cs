using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use this to manage the collisions
public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        // parameter = what ran into me
        // if the player hits me, then destroy the player and the current enemy rectangle
        var player = whatHitMe.GetComponent<PlayerMovement>();
        var bullet = whatHitMe.GetComponent<Bullet>();

        Debug.Log("I hit something");

        if (player)
        {
            Debug.Log("It was the player");

            // play crash sound here
            // destroy the player and the rectangle
            // give the player points/coins
            Destroy(player.gameObject);
            Destroy(gameObject);
        }

        if (bullet)
        {
            // play die sound
            // give player points

            // destroy the bullet
            Destroy(bullet.gameObject);
            // destroy this GameObject
            Destroy(gameObject);
        }
    }
}
