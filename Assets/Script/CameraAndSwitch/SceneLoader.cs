using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject level2UI; 

    void Start()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;

        
        HandleUI(SceneManager.GetActiveScene());
    }

    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleUI(scene);
    }

    
    void HandleUI(Scene scene)
    {
        if (level2UI != null)
        {
            
            level2UI.SetActive(scene.name == "Level2");
        }
    }

    void OnDestroy()
    {
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
