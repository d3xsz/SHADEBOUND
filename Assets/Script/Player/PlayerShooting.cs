using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public float bulletSpeed = 10f; 
    public float fireCooldown = 1f; 

    private float nextFireTime = 0f; 
    private PlayerController playerController; 

    void Start()
    {
        
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        
        if (playerController.IsDucking())  
        {
            return; 
        }

       
        if (playerController.isRunning)  
        {
            return; 
        }

        
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
        }

        
        UpdateFirePointPosition();
    }

    private void UpdateFirePointPosition()
    {
        
        firePoint.position = new Vector3(transform.position.x, transform.position.y, firePoint.position.z);

        
        if (transform.localScale.x > 0)
        {
            firePoint.localPosition = new Vector3(0.5f, 0f, 0f);  
        }
        else
        {
            firePoint.localPosition = new Vector3(-0.5f, 0f, 0f);  
        }
    }

    private void Shoot()
    {
        
        nextFireTime = Time.time + fireCooldown;

        
        Animator animator = playerController.GetComponent<Animator>();
        animator.SetTrigger("isShooting"); 

        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        
        float direction = transform.localScale.x > 0 ? 1f : -1f; 

        
        rb.linearVelocity = new Vector2(direction * bulletSpeed, 0f);

        
        Destroy(bullet, 2f); 
    }
}