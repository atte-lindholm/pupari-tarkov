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


        Vector3 euler = transform.localEulerAngles;


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
