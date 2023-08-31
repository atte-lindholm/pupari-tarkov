using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public GunSystem gunScript;
    public GunSway gunSway;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;
    
    private void satrt()
    {
        //check if gun is not equipped 
        if (!equipped)
        {
            gunSway.enabled = false;
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        
        //check if gun is equipped
        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }

    }

    
    private void Update()
    {
        //pickup and drop sustem
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }

        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }
    
    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        // Make the object a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
        


        rb.isKinematic = true;
        coll.isTrigger = true;

        //enables gun script
        gunScript.enabled = true;


    }
    
    
    private void Drop()
    {
        
        equipped = false;
        slotFull = false;

        // Set parent to null
        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //enables gun script
        gunScript.enabled = false;


    }
}
