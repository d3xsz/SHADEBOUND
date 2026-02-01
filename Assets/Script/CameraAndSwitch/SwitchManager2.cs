using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class SwitchManager2 : MonoBehaviour
{
    public GameObject player;  
    public GameObject ghost;   
    public Camera mainCamera;  
    public float ghostDuration = 10f; 
    public TMP_Text countdownText; 

    private bool isGhostActive = false;  
    private float ghostTimeLeft;         

    private bool canSwitch = true; 

    void Start()
    {
        
        player.SetActive(true);
        ghost.SetActive(false);
        ghostTimeLeft = ghostDuration; 

        
        UpdateCountdownText();
        countdownText.gameObject.SetActive(false); 
    }

    void Update()
    {
        
        if (canSwitch && Input.GetKeyDown(KeyCode.X))
        {
            ToggleActiveCharacter();
        }

        
        if (isGhostActive)
        {
            ghostTimeLeft -= Time.deltaTime;
            if (ghostTimeLeft <= 0f)
            {
                
                GhostToPlayer();
            }
            else
            {
                
                UpdateCountdownText();
            }
        }
    }

    private void ToggleActiveCharacter()
    {
        if (isGhostActive)
        {
            
            GhostToPlayer();
        }
        else
        {
            
            if (ghostTimeLeft > 0f) 
            {
                player.SetActive(false);
                ghost.SetActive(true);
                isGhostActive = true;
                ghostTimeLeft = ghostDuration; 
                canSwitch = true; 

                
                CameraController2 cameraController = mainCamera.GetComponent<CameraController2>();
                if (cameraController != null)
                {
                    cameraController.SetCameraToGhost();
                }

                
                countdownText.gameObject.SetActive(true);
            }
        }
    }

    private void GhostToPlayer()
    {
        
        isGhostActive = false;
        ghost.SetActive(false);
        player.SetActive(true);
        ghostTimeLeft = 0f;  

        
        if (mainCamera != null)
        {
            CameraController2 cameraController = mainCamera.GetComponent<CameraController2>();
            if (cameraController != null)
            {
                
                cameraController.ResetCamera();
            }
        }

        canSwitch = false;   

        
        countdownText.gameObject.SetActive(false);
    }

    
    private void UpdateCountdownText()
    {
        if (countdownText != null)
        {
            countdownText.text = Mathf.CeilToInt(ghostTimeLeft).ToString(); 
        }
    }
}
