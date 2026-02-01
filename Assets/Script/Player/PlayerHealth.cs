using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthSlider; 
    public GameObject gameOverUI; 

    void Start()
    {
        currentHealth = maxHealth;

        
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false); 
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; 
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Die() called."); 

        
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            Debug.Log("GameOverUI set to active."); 
        }
        else
        {
            Debug.LogError("GameOverUI is not assigned in the Inspector!");
        }

        Time.timeScale = 0f; 
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; 
        }

        Debug.Log("Player Health Reset: " + currentHealth);
    }

    
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; 
        }

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; 
        }

        Debug.Log("Player Healed: " + currentHealth);
    }

    
    public void RetryLevel()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }
}
