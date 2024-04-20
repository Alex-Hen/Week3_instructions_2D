using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpHeight = 5;
    // Vector2 dir;
    float horzDir;

    // Reference the 2D rigidbody
    private Rigidbody2D rb;

    private bool isGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        // Create a 3D vector
        // dir = new Vector2(0, 0);

        // Initialize the rigid body
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // convert the 2D vector into 3D for the transform function
        // Vector3 v3dir = new Vector3(dir.x, dir.y, 0);

        // Move us based on our position - Don't forget to scale for framerate
        // transform.position += v3dir * speed * Time.deltaTime;

        // And then just modify the velocity value
        // rb.velocity = dir * speed;

        // Only change the x velocity and leave the y component along - set it to whatever it was before
        // NOTE - we can't directly edit existing values - have to make new ones
        rb.velocity = new Vector2(horzDir * speed, rb.velocity.y);
    }


    // When we hit a button - when we do some input, then we get a value
    void OnMove(InputValue value)
    {
        // Get the value
        Vector2 inputDir = value.Get<Vector2>();

        // Split into the components - only the x for now
        float inputX = inputDir.x;

        // Set the direction based on the components that we got
        // NOTE: Position is ALWAYS a 3D vector - even if we're working in 2D
        // Note that we are overwriting the y direction to 0 every time - counters physics
        // dir = new Vector2(inputX, 0);

        // Slap it directly into the direction
        horzDir = inputX;
    }




    // Add some jumping --------------------

    // Contact
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    // Leave Contact
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false ;
        }
    }

    // When we recieve the jump command
    void OnJump()
    {
        // Only edit the y component
        if (isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }


}
