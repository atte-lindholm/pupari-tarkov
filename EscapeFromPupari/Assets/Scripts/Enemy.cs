using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
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