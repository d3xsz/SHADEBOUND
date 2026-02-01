using UnityEngine;
using UnityEngine.SceneManagement;

public class SecurityCamera : MonoBehaviour
{
    public LayerMask detectionLayer; 
    public float detectionRange = 5f; 
    private bool isCameraActive = true; 
    private SpriteRenderer spriteRenderer; 

    public GameObject gameOverUI; 
    private static bool isGameOverTriggered = false; 

    void Start()
    {
        
        isCameraActive = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer bulunamadý! Kamerada bir SpriteRenderer eklemeyi unutmayýn.");
        }

        if (gameOverUI == null)
        {
            Debug.LogError("Game Over UI atanmamýþ!");
        }

        
        ResetGameOverState();
    }

    void Update()
    {
        if (!isCameraActive || isGameOverTriggered) return;

        
        Collider2D detected = Physics2D.OverlapCircle(transform.position, detectionRange, detectionLayer);
        if (detected != null)
        {
            Debug.Log($"{gameObject.name} kamera alanýna girdi!");
            spriteRenderer.color = Color.blue; 
            EndGame(); 
        }
    }

    public void DeactivateCamera()
    {
        isCameraActive = false; 
        spriteRenderer.color = Color.red; 
        Debug.Log($"{gameObject.name} kamera devre dýþý býrakýldý.");
    }

    private void EndGame()
    {
        if (isGameOverTriggered) return;

        isGameOverTriggered = true; 
        Debug.Log("Oyun bitti!");
        Time.timeScale = 0f; 

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); 
        }
    }

    public static void ResetGameOverState()
    {
        isGameOverTriggered = false; 
        Debug.Log("isGameOverTriggered sýfýrlandý.");
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = isCameraActive ? Color.green : Color.gray;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    
    public void TryAgain()
    {
        Debug.Log("Try Again butonuna basýldý!");
        Time.timeScale = 1f; 
        SecurityCamera.ResetGameOverState(); 

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void MainMenu()
    {
        Debug.Log("Main Menu butonuna basýldý!");
        Time.timeScale = 1f; // Zamaný yeniden baþlat
        SecurityCamera.ResetGameOverState(); // Game Over durumu sýfýrlanýr
        SceneManager.LoadScene("MainMenu"); // Ana menü sahnesini yükle (adýný kontrol edin)
    }

    // Yeni sahne yüklendikten sonra gerekli ayarlarý yapmak için callback fonksiyonu
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Sahne yüklendikten sonra yapýlacak iþlemler
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Oyun baþladýðýnda zaman ölçeðini 1 yapýyoruz
        Time.timeScale = 1f;

        // Game Over UI'yi kapatýyoruz
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        // Oyun baþlatýldýktan sonra, karakter hareketini, fizik iþlemlerini ve kameralarý kontrol etmek için
        // yeniden baþlatma kodlarýný burada yapabilirsiniz.
        Debug.Log("Yeni sahne yüklendi, tüm ayarlar uygulandý.");
    }
}
