using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    
    public float speed = 5.0f; // Constant speed
    public Transform startPoint;
    public Transform endPoint;

    private bool movingToEnd = true;

    private void Update()
    {
        if (movingToEnd)
        {
            // Move towards the end point
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);

            // Check if reached the end point
            if (Vector3.Distance(transform.position, endPoint.position) < 0.1f)
            {
                // Turn around
                Flip();
            }
        }
        else
        {
            // Move towards the start point
            transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);

            // Check if reached the start point
            if (Vector3.Distance(transform.position, startPoint.position) < 0.1f)
            {
                // Turn around
                Flip();
            }
        }
    }

    // This function flips the enemy's direction
    private void Flip()
    {
        movingToEnd = !movingToEnd;
        transform.Rotate(Vector3.up, 180f); // Rotate 180 degrees
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Add any additional logic for handling damage here
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}