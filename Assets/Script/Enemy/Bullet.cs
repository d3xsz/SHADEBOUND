using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 direction;

    private void Start()
    {
       
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Mermi Player'a çarptı!");
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(1); 
            }
            Destroy(gameObject); 
        }

        
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Mermi Enemy'ye çarptı!");
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(10); 
            }
            Destroy(gameObject); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Debug.Log($"Çarpışma algılandı: {collision.gameObject.name}");
    }
}