using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public GameObject Target;
    public float speed = 1.5f;
    public int enemyDamage = 10; // Damage amount the enemy deals

    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    
    private void Update()
    {
        transform.LookAt(Target.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void TakeDamage(int damage)
    {
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
            // Deal damage to the player
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(enemyDamage);
            }
        }
    }
}