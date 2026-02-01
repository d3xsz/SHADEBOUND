using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class PlayerInteraction3 : MonoBehaviour
{
    public GameObject fireEffect; 
    public GameObject box; 
    public float interactionRange = 2f; 
    public float burnDelay = 1f; 
    private bool gameOver = false;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.G) && !gameOver)
        {
            
            if (Vector2.Distance(transform.position, box.transform.position) <= interactionRange)
            {
                
                StartFireEffect();

                
                StartCoroutine(BurnBoxWithDelay());
            }
        }
    }

    void StartFireEffect()
    {
        
        Vector3 firePosition = new Vector3(box.transform.position.x - 0.2f, box.transform.position.y + 0.1f, box.transform.position.z - 1f); 
        GameObject fire = Instantiate(fireEffect, firePosition, Quaternion.identity);

        
        SpriteRenderer fireRenderer = fire.GetComponent<SpriteRenderer>();
        if (fireRenderer != null)
        {
            fireRenderer.sortingLayerName = "Fire"; 
            fireRenderer.sortingOrder = 1; 
        }
    }

    IEnumerator BurnBoxWithDelay()
    {
        
        yield return new WaitForSeconds(burnDelay);

        
        BurnBox();

        
        EndGame();
    }

    void BurnBox()
    {
       

        SpriteRenderer boxRenderer = box.GetComponent<SpriteRenderer>();
        if (boxRenderer != null)
        {
            boxRenderer.color = Color.red; 
        }
    }

    void EndGame()
    {
        gameOver = true;
        Debug.Log("Game Over! The box has burned.");
    }

    
    public void OnNewLevelClicked()  
    {
        
        SceneManager.LoadScene("Level2");  
    }

    
    public void OnMainMenuClicked()  
    {
        
        SceneManager.LoadScene("MainMenu");  
    }
}
