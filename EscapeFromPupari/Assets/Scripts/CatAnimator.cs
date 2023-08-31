using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimator : MonoBehaviour
{

    public Animator animator;

    Vector3 lastPos;


    // Update is called once per frame
    void Update()
    {
        var speed = (transform.position-lastPos).magnitude/Time.deltaTime;
        animator.SetFloat("Speed",speed);
        lastPos = transform.position;
    }
}
