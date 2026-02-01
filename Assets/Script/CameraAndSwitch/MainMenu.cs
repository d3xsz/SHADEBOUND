using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame()
    {
        Time.timeScale = 1.0f;
        string sceneName = "Level1";  

        
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);  
        }
        else
        {
            Debug.LogError("Sahne yüklenemiyor: " + sceneName + ". Build Settings'te ekli mi?");
        }
    }

    
    public void StartTutorial()
    {
        Time.timeScale = 1f;
        string sceneName = "Tutorial";  

        
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);  
        }
        else
        {
            Debug.LogError("Sahne yüklenemiyor: " + sceneName + ". Build Settings'te ekli mi?");
        }
    }

    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  
#else
            Application.Quit();  // Build'de oyun tamamen kapanýr
#endif
    }
}

