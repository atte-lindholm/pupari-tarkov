using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    public Animator animator;
    Vector3 lastPos;


    // Update is called once per frame
    void Update()
    {
        var speed = (transform.position - lastPos).magnitude / Time.deltaTime;
        animator.SetFloat("Speed", speed);
        lastPos = transform.position;
    }




    public void Chase()
    {
        animator.SetBool("Chase", true);
    }

    public void StopChase() 
    {
        animator.SetBool("Chase", false);
    }

    public void Attack()
    {
        animator.SetBool("Attack", true);
    }

    public void StopAttack() 
    {
        animator.SetBool("Attack", false);
    }
    public void Dead()
    {
        animator.SetBool("Dead", true);
    }
}
