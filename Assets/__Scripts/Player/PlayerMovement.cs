using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // == public fields ==

    // == private fields ==
    private Rigidbody2D rb;
    private Transform t;
    [SerializeField] private float speed = 15.0f;

    // == public methods ==

    // == private methods ==

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // if the player presses an arrow, then move
        float vMovement = Input.GetAxis("Vertical");
        float hMovement = Input.GetAxis("Horizontal");

        // apply a force, get the player moving
        rb.velocity = new Vector2(hMovement * speed, vMovement * speed);

        // Keep the player on the screen
        // Mathf.clamp
        float yValue = Mathf.Clamp(rb.position.y, -4.6f, 4.6f);
        float xValue = Mathf.Clamp(rb.position.x, -11.0f, 11.0f);

        rb.position = new Vector2(xValue, yValue);
    }
}
