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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Target.transform.position, transform.position) > distance)
        {
            agent.SetDestination(Target.transform.position);
        }

    }




}
