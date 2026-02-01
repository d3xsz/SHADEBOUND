using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform ghost;

    public float followSpeed = 5f;
    public Vector3 playerOffset;
    public Vector3 ghostOffset;

    public float playerZoom = 5f;
    public float ghostZoom = 8f;
    public float zoomSpeed = 2f;

    private Transform currentTarget;
    private Vector3 currentOffset;
    private float targetZoom;
    private Camera cam;

    public float minX, maxX;
    public float minY, maxY; 

    void Start()
    {
        cam = Camera.main;

        if (player != null)
        {
            currentTarget = player;
            currentOffset = playerOffset;
            targetZoom = playerZoom;
        }
        else
        {
            Debug.LogError("Player Transform atanmamýþ!");
        }
    }

    void Update()
    {
        HandleSwitch();
    }

    void LateUpdate()
    {
        FollowTarget();
        AdjustZoom();
    }

    private void HandleSwitch()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentTarget == player && ghost != null)
            {
                currentTarget = ghost;
                currentOffset = ghostOffset;
                targetZoom = ghostZoom;
            }
            else if (currentTarget == ghost && player != null)
            {
                currentTarget = player;
                currentOffset = playerOffset;
                targetZoom = playerZoom;
            }
        }
    }

    private void FollowTarget()
    {
        if (currentTarget != null)
        {
            Vector3 targetPosition = currentTarget.position + currentOffset;

            
            float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(targetPosition.y, minY, maxY);
            targetPosition.x = clampedX;
            targetPosition.y = clampedY;

            
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Takip edilecek hedef atanmadý!");
        }
    }

    private void AdjustZoom()
    {
        if (cam != null)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
        }
    }

    public void ResetCamera()
    {
        if (player != null)
        {
            currentTarget = player;
            currentOffset = playerOffset;
            targetZoom = playerZoom;
        }

        
        transform.position = player.position + currentOffset;
        cam.orthographicSize = playerZoom;
    }
}
