using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Firstplayercamera : MonoBehaviour
{
    //variablse
    public float rotation = 4.0f;
    public float walkRotation = 2.0f;
    public Transform player;
    public float mouseSensitivity = 2f;
    public float rotationAmount = 1.0f;
    public float rotationSpeed = 1.0f;
    float cameraVertivalRotation = 0f;
    private Transform _playerTransform;
    private Rigidbody _player;
    bool lockedCursor = true;
    private bool woblleright = true;
    private float i = 1.0f;
    private float test;

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = transform.parent;
        _player = _playerTransform.GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        //get mouse input
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        
        var xspeed = _player.velocity.x;
        var zspeed = _player.velocity.z;
        Vector3 euler = transform.localEulerAngles;


        //if (zspeed > 3 || zspeed < -3)
        //{
        //    if (!Input.GetKey(KeyCode.C))
        //    {
        //        if (woblleright == true)
        //        {
        //            i = i + rotationSpeed / 10;
        //            if (i > rotationAmount)
        //            {
        //                woblleright = false;

        //            }

        //        }
        //        else
        //        {
        //            i = i - rotationSpeed / 10;
        //            if (i < -rotationAmount)
        //            {
        //                woblleright = true;
        //            }

        //        }
        //        euler.z = ((zspeed * walkRotation * i)+test)/2;

        //    }
        //    else { euler.z = euler.z / 2; }
        //}
        euler.z = ((rotation * xspeed) + test)/2; 

        test = euler.z;

            
        //camera rotation Y axis
        cameraVertivalRotation -= inputY;
        cameraVertivalRotation = Mathf.Clamp(cameraVertivalRotation, -90f, 90f);
        euler.x = cameraVertivalRotation;
        transform.localEulerAngles = euler;


        //camera rotation X axis

        player.Rotate(Vector3.up * inputX);

    }

    // thanks youtube tutorial https://www.youtube.com/watch?v=5Rq8A4H6Nzw
}
