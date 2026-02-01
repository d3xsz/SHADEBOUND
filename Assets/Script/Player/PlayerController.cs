using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private bool isGrounded;
    private bool isDucking;  
    private float groundYThreshold = 0.1f; 

    public float fireCooldown = 1f; 
    private float nextFireTime = 0f; 

    
    public bool isRunning;

    private bool isDead = false; 

    public GameObject bulletPrefab; 
    public Transform shootingPoint; 

    
    public float minX, maxX; 

    public PlayerHealth playerHealth; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (playerHealth == null)
        {
            playerHealth = GetComponent<PlayerHealth>(); 
        }
    }

    void Update()
    {
        if (isDead) return; 

        
        isGrounded = Mathf.Abs(rb.linearVelocity.y) < 0.01f;

        animator.SetBool("isGrounded", isGrounded);

        
        if (!isGrounded && rb.linearVelocity.y < 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isDucking)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("isJumping");
        }

        
        if (isGrounded)
        {
            animator.SetBool("isFalling", false);
            isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
            animator.SetBool("isRunning", isRunning);
        }

        
        if (!isDucking) 
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

            
            if (moveInput > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (moveInput < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }

        
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        
        if (Input.GetKey(KeyCode.LeftControl) && isGrounded)
        {
            SetDucking(true); 
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); 
        }
        else
        {
            SetDucking(false); 
        }

        
        if (Input.GetKey(KeyCode.F) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireCooldown; 
            Shoot(); 
        }
    }

    
    public bool IsDucking()  
    {
        return isDucking;
    }

    public void SetDucking(bool value)  
    {
        isDucking = value;
        animator.SetBool("isDucking", isDucking);
    }

    
    void Shoot()
    {
        if (bulletPrefab == null || shootingPoint == null) return;

        
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

        
        bullet.SetActive(true);

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            
            Vector2 direction = new Vector2(transform.localScale.x, 0); 
            bulletScript.SetDirection(direction);
        }
    }

    
    public void TakeDamage(int damage)
    {
        if (isDead) return; 

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage); 
        }
    }
}