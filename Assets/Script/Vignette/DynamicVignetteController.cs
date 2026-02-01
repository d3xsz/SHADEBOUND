using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DynamicVignetteController : MonoBehaviour
{
    public Transform player; 
    public Transform ghost; 
    public Volume globalVolume; 

    private Vignette vignette;

    private void Start()
    {
        
        if (globalVolume.profile.TryGet(out vignette))
        {
            Debug.Log("Vignette efekti baþarýyla alýndý.");
        }
        else
        {
            Debug.LogError("Global Volume içinde Vignette efekti bulunamadý.");
        }
    }

    private void Update()
    {
        if (vignette == null)
        {
            return;
        }

        
        if (player != null && player.gameObject.activeSelf)
        {
           
            vignette.intensity.Override(0.55f);
            vignette.smoothness.Override(0.2f); 
            vignette.center.Override(new Vector2(0.5f, 0.5f)); 
        }
        
        else if (ghost != null && ghost.gameObject.activeSelf)
        {
            vignette.intensity.Override(0f); 
        }
    }
}