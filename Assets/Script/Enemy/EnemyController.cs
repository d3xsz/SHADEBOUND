using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private bool isShooting = false;
    private bool isPlayerInRange = false;

    public float shootingDelay = 2f; 
    private float shootingTimer = 0f;

    public float detectionRange = 5f; 
    public Transform player; 
    public SpriteRenderer spriteRenderer; 
    public GameObject bulletPrefab; 
    public Transform shootingPoint; 

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 

        
        if (bulletPrefab != null)
        {
            bulletPrefab.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null) return; 

        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        
        if (distanceToPlayer <= detectionRange)
        {
            
            isPlayerInRange = true;
            isShooting = true;

            shootingTimer -= Time.deltaTime; 

            if (shootingTimer <= 0)
            {
                
                if (animator != null)
                {
                    animator.SetTrigger("ShootTrigger");
                }
                shootingTimer = shootingDelay; 
                Shoot(); 
            }

            
            FlipTowardsPlayer();
        }
        else
        {
            
            isPlayerInRange = false;
            isShooting = false;
            if (animator != null)
            {
                animator.SetBool("IsIdle", true); 
            }
        }
    }

    
    void FlipTowardsPlayer()
    {
        if (player == null) return; 

        
        if (player.position.x < transform.position.x)
        {
            if (spriteRenderer != null && !spriteRenderer.flipX) 
            {
                spriteRenderer.flipX = true;
            }
        }
        else 
        {
            if (spriteRenderer != null && spriteRenderer.flipX) 
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    
    void Shoot()
    {
        if (player == null || bulletPrefab == null || shootingPoint == null) return; 

        
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

        
        bullet.SetActive(true);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            
            Vector2 direction = (player.position - shootingPoint.position).normalized;
            bulletScript.SetDirection(direction);
        }
    }
}