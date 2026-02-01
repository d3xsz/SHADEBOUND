using UnityEngine;
using UnityEngine.Rendering;  

public class GlobalVolumeManager : MonoBehaviour
{
    public Volume globalVolume; 

    void Awake()
    {
        if (globalVolume != null)
        {
            
            DontDestroyOnLoad(globalVolume.gameObject);
        }
        else
        {
            Debug.LogError("Global Volume nesnesi atanmadý.");
        }
    }
}
