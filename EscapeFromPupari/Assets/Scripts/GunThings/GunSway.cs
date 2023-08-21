using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSway : MonoBehaviour
{
    public float intensity;
    public float smooth;

    private Quaternion original_rotation;


    // Start is called before the first frame update
    private void Start()
    {
        original_rotation = transform.rotation;  
    }

    
    // Update is called once per frame
    private void Update()
    {
        UpdateGunSway();
    }

    
    private void UpdateGunSway() 
    {
        // mouse controlls
        float t_x_mouse = Input.GetAxis("Mouse X");
        float t_y_mouse = Input.GetAxis("Mouse Y");

        //target rotation
        Quaternion t_x_adj = Quaternion.AngleAxis(-intensity * t_x_mouse, Vector3.up);
        Quaternion t_y_adj = Quaternion.AngleAxis(intensity * t_y_mouse, Vector3.right);
        Quaternion target_rotation = original_rotation * t_x_adj * t_y_adj;

        //rotate towards target rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smooth);
        
    }

}
