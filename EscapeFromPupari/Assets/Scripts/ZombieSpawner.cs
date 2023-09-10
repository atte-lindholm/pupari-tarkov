using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject Zombie;
    public float timeBetweenWaves = 20f;
    public float zombieAmount = 10f;
    public float waveMultiplier = 0.25f;
    public List<GameObject> spawns;
    public float time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Check if  time between waves
        if (time >= timeBetweenWaves)
        {
            //spawn zombies
            ZombieSpawn();

            //add zombie amount by multiplying with the multiplyer
            zombieAmount = zombieAmount * waveMultiplier;

            //reset time
            time = 0;
        }
        else
        {
            //add time
            time = time + Time.deltaTime;
        }


    }

    void ZombieSpawn()
    {
        //loop as many time as zombies
        for (int i = 0; i < zombieAmount; i++)
        {
            //get spawn location with random
            var randomSpawn = spawns[Random.Range(0, spawns.Count)];

            //spawn the zombie at thath location
            Instantiate(Zombie, randomSpawn.transform.position, Quaternion.identity);
        }
    }
}
