using UnityEngine;

public class EnergyDrink : MonoBehaviour
{
    public int healAmount = 10; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>(); 
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount); 
            }

            Destroy(gameObject); 
        }
    }
}