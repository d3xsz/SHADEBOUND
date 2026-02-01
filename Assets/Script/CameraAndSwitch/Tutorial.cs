using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public LayerMask detectionLayer; 
    public float detectionRange = 5f; 
    private bool isCameraActive = true; 
    private SpriteRenderer spriteRenderer; 

    void Start()
    {
        
        isCameraActive = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer bulunamadý! Kamerada bir SpriteRenderer eklemeyi unutmayýn.");
        }
    }

    void Update()
    {
        if (!isCameraActive) return;

        
        if (Input.GetMouseButtonDown(1)) 
        {
            
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (Vector2.Distance(mousePosition, transform.position) < 1f) 
            {
                DeactivateCamera(); 
            }
        }

        // Player karakterini algýlama
        Collider2D detected = Physics2D.OverlapCircle(transform.position, detectionRange, detectionLayer);
        if (detected != null)
        {
            Debug.Log($"{gameObject.name} kamera alanýna girdi!");
            spriteRenderer.color = Color.blue; 
        }
    }

    public void DeactivateCamera()
    {
        isCameraActive = false; 
        spriteRenderer.color = Color.red; 
        Debug.Log($"{gameObject.name} kamera devre dýþý býrakýldý.");
    }

    private void OnDrawGizmos()
    {
       
        Gizmos.color = isCameraActive ? Color.green : Color.gray;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}