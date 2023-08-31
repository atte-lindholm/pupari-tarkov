using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CatMovement : MonoBehaviour
{
    public GameObject Target;
    public float distance = 30;
    public NavMeshAgent agent;




    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Target.transform.position, transform.position) > distance)
        {
            agent.SetDestination(new Vector3(Target.transform.position.x+Random.Range(-1,1), Target.transform.position.y, Target.transform.position.z + Random.Range(-1, 1)));
        }

    }




}
