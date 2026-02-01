using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SafeVolumeController : MonoBehaviour
{
    public Transform player; 
    public Volume globalVolume; 

    private Vignette vignette;

    private void Start()
    {
        
        if (globalVolume != null && globalVolume.profile != null)
        {
            if (globalVolume.profile.TryGet(out vignette))
            {
                Debug.Log("Vignette baþarýyla alýndý.");
            }
            else
            {
                Debug.LogError("Vignette efekti bulunamadý.");
            }
        }
        else
        {
            Debug.LogError("Global Volume veya profile null.");
        }
    }

    private void Update()
    {
        if (globalVolume == null || vignette == null)
        {
            Debug.LogWarning("Global Volume veya Vignette null, iþlem yapýlmýyor.");
            return;
        }

        
        Vector3 playerScreenPos = Camera.main.WorldToViewportPoint(player.position);
        vignette.center.value = new Vector2(playerScreenPos.x, playerScreenPos.y);
    }
}
