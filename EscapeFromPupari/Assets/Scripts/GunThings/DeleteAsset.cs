using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAsset : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy gameobject after certain amount of time
        Destroy(gameObject,time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
