using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float velocity;
    public float sprintSpeed;
    public float walkSpeed;
    public float rayCastBoxSize;
    bool grounded;
    public float jumpForce;
    public float height;
    public float rayCastheight;
    Rigidbody rb;
    public RaycastHit m_Hit;
    public float gravity;

    public int maxHealth = 100;
    public int currentHealth;

    //to draw the rays
    //got it working with https://docs.unity3d.com/ScriptReference/Physics.BoxCast.html
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * (height * 0.5f + rayCastheight));
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y - 0.75f + rayCastheight, transform.position.z) + Vector3.down * 0.5f, new Vector3(rayCastBoxSize, 0, rayCastBoxSize));
    }
    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody
        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        // to show velocity ú‹ development
        velocity = rb.velocity.magnitude;
        grounded = isground();

        //if on ground
        if (grounded)
        {
            //Move with inout
            MovePlayer(GetInput());

            //jump
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

            //to cap the speed
            maxSpeed();
        }
        else
        {
            Gravity();
        }

    }

    /// <summary>
    /// Get the input and return it in a list (x,z)
    /// </summary>
    /// <returns></returns>
    private List<float> GetInput()
    {
        var z = Input.GetAxisRaw("Horizontal");
        var x = Input.GetAxisRaw("Vertical");
        return new List<float> { x, z };
    }

    /// <summary>
    /// moves the player with a list with (x,z)
    /// </summary>
    /// <param name="direction"></param>
    private void MovePlayer(List<float> direction)
    {
        //transforms input to vector 3
        var move = transform.forward * direction[0] + transform.right * direction[1];
        //adds the movement
        rb.AddForce(move.normalized * sprintSpeed * 10f, ForceMode.Force);
    }


    /// <summary>
    /// caps the speed to sprint speed
    /// </summary>
    private void maxSpeed()
    {
        //gets the velocity
        Vector3 speed = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //if speed too high
        if (speed.magnitude > sprintSpeed)
        {
            //caps the speed
            Vector3 slowSpeed = speed.normalized * sprintSpeed;
            rb.velocity = new Vector3(slowSpeed.x, rb.velocity.y, slowSpeed.z);
        }
    }

    /// <summary>
    /// checks if on fround with raycast and returns the value
    /// </summary>
    /// <returns></returns>
    private bool isground()
    {
        //if ground straight down
        if(Physics.Raycast(transform.position, Vector3.down, height * 0.5f + rayCastheight))
        {
            return true;
        }
        //if not straight downs then if it is in a square under the player
        else if (Physics.BoxCast(new Vector3(transform.position.x, transform.position.y - 0.75f + rayCastheight, transform.position.z), new Vector3(rayCastBoxSize, 0, rayCastBoxSize), Vector3.down, out m_Hit, rb.rotation, 0.5f))
        {
            return true;
        }
        //no ground
        else
        {
            return false;
        }
        
    }

    //Jump
    private void Jump()
    {
        //stops all the y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //add the jump
        rb.AddForce(0f, jumpForce * 10f, 0f);
    }

    private void Gravity()
    {
        rb.AddForce(0f, -1 * gravity, 0f);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // logic for when the player's health reaches zero or below
        if (currentHealth <= 0)
        {
            Die(); // Implement your logic for player death here
        }
    }

    private void Die()
    {
        // logic for when the player dies
        gameObject.SetActive(false);
    }
}
