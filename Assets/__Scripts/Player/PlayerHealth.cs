using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // set up an initial health value
    // set an amount of damage per enemy - start with 5

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private int playerStartHealth = 20;
    private int playerCurrentHealth;
    private GameController gc;
    private Vector3 startPosition;

    private SpriteRenderer sr;
    private PolygonCollider2D pc2d;
    private WeaponsController wc;
    private PlayerMovement pm;
    private ParticleSystem ps;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        pc2d = GetComponent<PolygonCollider2D>();
        wc = GetComponent<WeaponsController>();
        pm = GetComponent<PlayerMovement>();
        ps = GetComponentInChildren<ParticleSystem>();

        playerCurrentHealth = playerStartHealth;
        gc = FindObjectOfType<GameController>();

        startPosition = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z
        );
    }

    // use the triggerEnter method to see if it gets hit by enemy
    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        var enemy = whatHitMe.GetComponent<Enemy>();

        if (enemy)
        {
            playerStartHealth -= enemy.DamageValue;
            Debug.Log($"Player Health = {playerStartHealth}");
        }

        if (playerCurrentHealth <= 0)
        {
            // player should die
            Die();
        }
    }

    private void Die()
    {
        // need to stop the player interacting - disable weapons
        // make the ship disappear
        // need to play an explosion
        // hide the onject, then make it reappear at the start
        // going to take a little time

        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        // disable the components
        DisableComonents();

        GameObject explosion = Instantiate(explosionPrefab);

        explosion.transform.position = transform.position;

        // tell the GameController lost one life
        gc.LoseOneLife();

        yield return new WaitForSeconds(1.5f);

        if (gc.RemainingLives > 0)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // set the player back to the start position
        // reset the player health
        // re-enable all the components to make the player visible
        playerCurrentHealth = playerStartHealth;
        transform.position = startPosition;

        EnableComponents();
    }

    private void DisableComonents()
    {
        SetComponentsEnabled(false);
    }

    private void EnableComponents()
    {
        SetComponentsEnabled(true);
    }

    private void SetComponentsEnabled(bool status)
    {
        sr.enabled = status;
        pc2d.enabled = status;
        pm.enabled = status;
        wc.enabled = status;

        if (status)
        {
            ps.Play();
        }
        else
        {
            ps.Stop();
        }
    }
}
