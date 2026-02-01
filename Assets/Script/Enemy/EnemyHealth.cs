using UnityEngine;
using UnityEngine.UI; 

public class EnemyHealth : MonoBehaviour
{
    public int health = 2; 
    public HealthbarBehaviour healthbarBehaviour; 
    public int maxHealth = 2; 

    public GameObject deathEffect; 
    public float destroyDelay = 0.5f; 

    
    public void TakeDamage(int damage)
    {
        health -= damage;

        
        healthbarBehaviour.SetHealth(health, maxHealth);

       
        if (health <= 0)
        {
            Die();
        }
    }

    
    private void Die()
    {
        
        if (deathEffect != null)
        {
            
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);

            
            Animator animator = effect.GetComponent<Animator>();
            if (animator != null)
            {
                
                float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
                Destroy(effect, animationLength + 0.1f); 
            }
            else
            {
                
                Destroy(effect, 1f); 
            }
        }

        
        Destroy(gameObject, destroyDelay);
    }
}
