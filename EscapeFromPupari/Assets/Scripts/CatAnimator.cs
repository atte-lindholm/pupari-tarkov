using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimator : MonoBehaviour
{

    public Animator animator;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
       if(rb.velocity.magnitude > 0.2)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Sit", false);
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Sit", true);
        }
    }
}
