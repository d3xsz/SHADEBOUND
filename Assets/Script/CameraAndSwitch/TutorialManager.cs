using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject backButton; 

    void Start()
    {
        
        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            if (backButton != null)
            {
                backButton.SetActive(false);
            }
        }
    }

    public void ReturnToMainMenu()
    {
        
        SceneManager.LoadScene("MainMenu");
    }
}
