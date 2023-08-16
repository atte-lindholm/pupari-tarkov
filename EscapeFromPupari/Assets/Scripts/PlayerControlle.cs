using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControlle : MonoBehaviour
{

    // Start is called before the first frame update
    public float rigidbodyvleocity;
    public float Speed = 500.0f;
    public float SprintSpeed = 200.0f;
    public float SlideBoost = 200.0f;
    public float jumpHeight = 100.0f;
    public float jumpPower = 50;
    public float verticalInput = 0.0f;
    public float horizontalInput = 0.0f;
    public bool isGrounded = false;
    public float crouchHeight = 1.0f;
    public float CrouchspeedDivider = 2.0f;
    private float _drag;
    private float _colliderHeight;
    Rigidbody rb;
    CapsuleCollider _collider;

    void Start()
    {
       _collider = GetComponent<CapsuleCollider>();
        _colliderHeight = _collider.height;
       rb = GetComponent<Rigidbody>();
        _drag = rb.drag;

    }

    // Update is called once per frame
    void Update()
    {
        rigidbodyvleocity = rb.velocity.x;




        //Check if on gorund before moving
        isGrounded = CheckGround();
        if (isGrounded)
        {
            float amountToMove = Speed * Time.deltaTime;
            //sprint
            if (Input.GetKey(KeyCode.LeftShift) & Input.GetAxis("Vertical") > 0.8)
            {
                amountToMove = (Speed+SprintSpeed) * Time.deltaTime;
            }




            //Check Movement
            
            Vector3 Horizontalmovement = (Input.GetAxis("Horizontal") * transform.right * amountToMove);
            Vector3 Verticalmovement = (Input.GetAxis("Vertical") * transform.forward * amountToMove);
            Vector3 movement = (Horizontalmovement + Verticalmovement);




            //Check if space pressed
            if (Input.GetKeyDown("space"))
            {
                Jump(movement);
            }
            else if (Input.GetKey(KeyCode.C))
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    rb.AddForce(movement*SlideBoost);
                }
                Slide();
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                Crouch(movement);
            }
            else
            {

                CancelSlideandCrouch();

                //wasd movement
                Move(movement);
            }




        }



    }

    /// <summary>
    /// Movest the rigid body with vector3
    /// </summary>
    /// <param name="movement"></param>
    public void Move(Vector3 movement)
    {
        //adding the current movement
        rb.AddForce(movement);

    }

    /// <summary>
    /// Makes the rigid body jumps and adds Vector 3 forces indented to make the jump have more power
    /// </summary>
    /// <param name="movement"></param>
    public void Jump(Vector3 movement)
    {
        //adding vertical and the current movement
        CancelSlideandCrouch();
        rb.AddForce(Vector3.up * jumpHeight+ movement* jumpPower);
    }


    public void Crouch(Vector3 movement)
    {
        rb.drag = _drag;
        Move(movement/CrouchspeedDivider);
        _collider.height = crouchHeight;
    }

    public void Slide()
    {
        _collider.height = crouchHeight;
        rb.drag = 0;


    }
    public void CancelSlideandCrouch()
    {
        _collider.height = _colliderHeight;
        rb.drag = _drag;

    }

    /// <summary>
    /// Chech if the rigid body is on the ground
    /// </summary>
    /// <returns></returns>
    public bool CheckGround()
    {
        //Thanks google
        //https://forum.unity.com/threads/how-to-detect-if-the-player-is-on-ground.1165010/
        var _distanceToTheGround = GetComponent<Collider>().bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, _distanceToTheGround + 0.1f);
    }

}
