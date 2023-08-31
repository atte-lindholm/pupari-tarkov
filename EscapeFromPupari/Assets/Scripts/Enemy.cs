using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public NavMeshAgent agent;
    public GameObject Target;
    public float speed = 1.5f;
    public float rayCastSphereSize = 10.0f;
    public float frontRaySize = 25.0f;
    public int enemyDamage = 10; // Damage amount the enemy deals
    float timePassed;
    int layer;
    RaycastHit SpehereHit;
    RaycastHit straightHit;
    bool chasing = false;
    Vector3 lastPosition;
    bool lastPosChecked = true;
    


    ///The "AI" is broken and i cant seem to fix it. The problem is the sphere ray that i cannot get into place correctly.


    private void Start()
    {
        // set the current health to the max health
        currentHealth = maxHealth;

        //Get the layer where the player is
        layer = LayerMask.GetMask("Player");
    }

    //draw the sphere but not in the right spot
    private void OnDrawGizmos()
    {
       Gizmos.DrawSphere(transform.position, rayCastSphereSize);
    }
    private void Update()
    {
        //See if can chase the player and starts chacing if it can
        chasing = whenToChase();

        //if not chasing
        if (!chasing ) 
        {
            //goes to last pos where player has been seen if not checked it
            CheckLastPosition();

            //to randomize idle so that maybe does something every 4 seconds
            timePassed += Time.deltaTime;
            if (timePassed > 4)
            {
                timePassed = 0;
                switch (Random.Range(0, 5))
                {
                    case 3:
                        //moves to random pos
                        SearchMove();
                        break;
                    case 2:
                        //looks to random pos
                        SearchTurn();
                        break;
                }
            }
        }

    }

    //runs at the player
    void StraightChase()
    {
        //resets the navmesh path
        agent.ResetPath();
        //looks at player
        transform.LookAt(Target.transform.position);
        //runs
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }


    //checks if can chase
    bool whenToChase()
    {

        //if a straight ray hits. the ray has more distance than the speher
        if (Physics.Raycast(transform.position, transform.forward, out straightHit, frontRaySize, (layer | LayerMask.GetMask("Default"))))
        {

                if (straightHit.transform.gameObject == Target.transform.gameObject)
                {
                //memorize lastpos
                    lastPosition = straightHit.transform.position;
                lastPosChecked = false;

                //chase
                    StraightChase();
                    return true;
                }
            

        }
        //if around player. NOT WORKING CORRECTLY
        if (Physics.SphereCast(transform.position, rayCastSphereSize, transform.forward,out SpehereHit,rayCastSphereSize, (layer | LayerMask.GetMask("Default"))))
        {
            if (SpehereHit.transform.gameObject == Target.transform.gameObject)
            {
                //memorize last pos
                lastPosition = SpehereHit.transform.position;
                lastPosChecked = false;

                //to debug
                Debug.Log("Chase");

                //look at and go to pos
                transform.LookAt(Target.transform.position);
                agent.SetDestination(lastPosition);
                return true;
            }

        }
        //if cannot chase
        return false;
    }


    //goes to check last position
    void CheckLastPosition()
    {

        //if not checked
        if (!lastPosChecked)
        {
            //go to the destanation
            agent.SetDestination(lastPosition);

            //checks if zombie in last pos and makes it checked
            if (transform.position == lastPosition)
            {
                lastPosChecked = true;
            }
        }
    }


    //goes to random pos around the zombie
    void SearchMove()
    {
        var newPosition = new Vector3(transform.position.x * Random.Range(-1.0f, 1.0f), transform.position.y, transform.position.z * Random.Range(-1.0f, 1.0f));
        agent.SetDestination(newPosition);
    }


    //lookas in direction
    void SearchTurn()
    {
        transform.rotation = Random.rotation;
    }

    

    //Made by Atte so not commentting
    public void TakeDamage(int damage)
    {
        //allows enemy to take damage and die
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    
    // Called when the enemy collides with another collider
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if enemy collides whit enemy deal damage to the player
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                //player takes damage
                player.TakeDamage(enemyDamage);
            }
        }
    }


}