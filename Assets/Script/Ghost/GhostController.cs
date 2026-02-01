using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float moveSpeed = 5f;         
    public float walkSpeed = 3f;         

    private Rigidbody2D rb;              
    private Collider2D ghostCollider;    
    public Animator animator;            

    
    public float minX, maxX;            
    public float minY, maxY;            

    public bool isGhostModeActive = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ghostCollider = GetComponent<Collider2D>(); 
        animator = GetComponent<Animator>();  
        rb.gravityScale = 1f;           
    }

    void Update()
    {
        HandleMovement();
        ClampPosition();                
        HandleCameraInteraction();      

        
        HandleGhostMode();

        
        UpdateAnimation();
    }

    private void HandleMovement()
    {
        float moveDirection = 0;

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -1; 
            FlipSprite(true);  
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1; 
            FlipSprite(false);  
        }

        
        Vector2 velocity = new Vector2(moveDirection * moveSpeed, 0f); 
        rb.linearVelocity = velocity;
    }

    private void ClampPosition()
    {
        
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void HandleCameraInteraction()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f; 

            
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);

            if (hit.collider != null)
            {
                
                SecurityCamera camera = hit.collider.GetComponent<SecurityCamera>();
                if (camera != null)
                {
                    camera.DeactivateCamera(); 
                    Debug.Log("Kamera devre d��� b�rak�ld�.");
                }
            }
            else
            {
                Debug.Log("Kamera tespit edilmedi.");
            }
        }
    }

    private void HandleGhostMode()
    {
        
        if (isGhostModeActive)
        {
            ghostCollider.enabled = false; 
        }
        else
        {
            ghostCollider.enabled = true; 
        }
    }

    
    private void UpdateAnimation()
    {
        
        float moveDirection = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", moveDirection); 
    }

    
    private void FlipSprite(bool isFlippingLeft)
    {
        if (isFlippingLeft && transform.localScale.x > 0)
        {
            
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (!isFlippingLeft && transform.localScale.x < 0)
        {
           
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
