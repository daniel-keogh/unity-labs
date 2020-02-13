using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use this to manage the collisions
public class Enemy : MonoBehaviour
{
    // Used from the GameContoller
    public int ScoreValue { 
        get {
            return scoreValue; 
        }
    }

    [SerializeField] private int scoreValue = 10;
    [SerializeField] private GameObject explosionFX;

    private float explosionDuration = 0.2f;

    // Set this to publish an event to the system when killed.
    // Delegate method to use for the event
    public delegate void EnemyKilled(Enemy enemy);

    // static method to be implemented in the listener
    public static EnemyKilled EnemyKilledEvent;

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

            // publish the event to the system to give the player points
            PublishEnemyKilledEvent();

            // show the explosion
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);

            Destroy(explosion, explosionDuration);
            // destroy this GameObject
            Destroy(gameObject);
        }
    }

    private void PublishEnemyKilledEvent()
    {
        // Make sure somebody is listening
        EnemyKilledEvent?.Invoke(this);
    }
}
