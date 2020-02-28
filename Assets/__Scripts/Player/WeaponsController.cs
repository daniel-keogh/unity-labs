using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponsController : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float bulletSpeed = 6.0f;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float firingInterval = 0.1f;
    [SerializeField] [Range(0f, 1.0f)] private float shootVolume = 0.5f;
    [SerializeField] private AudioClip shootClip;

    private GameObject bulletParent;
    private Coroutine firingCoroutine;  // pointer to the coroutine
                                        // need this to stop firing
    private AudioSource audioSource;

    // == private methods
    private void Start()
    {
        bulletParent = GameObject.Find("BulletParent");
        if (!bulletParent)
        {
            bulletParent = new GameObject("BulletParent");
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // setup a co routine to fire the bullets - start firing
            firingCoroutine = StartCoroutine(FireCoroutine());
            //FireBullet();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // stop firing
            StopCoroutine(firingCoroutine);
        }
    }

    // coroutine must be of type IEnumerator
    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            // generate bullets
            Bullet bullet = Instantiate(bulletPrefab, bulletParent.transform);
            bullet.transform.position = transform.position;

            // play sound - AudioClip, Volume between 0 & 1
            // AudioSource.PlayClipAtPoint(shootClip, Camera.main.transform.position, shootVolume);
            // use a local AudioSource
            audioSource.PlayOneShot(shootClip, shootVolume);

            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
            rbb.velocity = Vector2.right * bulletSpeed;
            // the yield return, causes the method to pause
            // sleep()
            yield return new WaitForSeconds(firingInterval);
        }
    }

    private void FireBullet()
    {
        // instantiate bullet
        Bullet bullet = Instantiate(bulletPrefab, bulletParent.transform);
        // give it the same position as the player
        bullet.transform.position = transform.position;
        // give it velocity and move right
        Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
        //rbb.velocity = new Vector2(1 * speed, 0);
        rbb.velocity = Vector2.right * bulletSpeed;
    }
}
