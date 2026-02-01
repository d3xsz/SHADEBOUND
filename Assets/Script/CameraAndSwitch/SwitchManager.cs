using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public GameObject player; 
    public GameObject ghost;  

    private bool isGhostActive = false; 

    void Start()
    {
        
        player.SetActive(true);
        ghost.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleActiveCharacter();
        }
    }

    private void ToggleActiveCharacter()
    {
        if (isGhostActive)
        {
            
            ghost.SetActive(false);
            player.SetActive(true);
            isGhostActive = false;
        }
        else
        {
           
            player.SetActive(false);
            ghost.SetActive(true);
            isGhostActive = true;
        }
    }
}
