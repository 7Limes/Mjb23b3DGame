using UnityEngine;

public class SplashBox : MonoBehaviour
{
    [Header("Particle Settings")]
    public ParticleSystem splashParticles;
    public Transform playerTransform;
    
    [Header("Splash Configuration")]
    public float splashThreshold = 0.1f; // Minimum movement speed to trigger splash
    public float splashCooldown = 0.1f; // Time between splashes
    
    private Vector3 lastPlayerPosition;
    private float lastSplashTime;
    private bool playerInWater = false;

    void Start()
    {
        // If no particle system assigned, try to find one
        if (splashParticles == null)
            splashParticles = GetComponentInChildren<ParticleSystem>();
            
        // If no player transform assigned, find player by tag
        if (playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            
        lastPlayerPosition = playerTransform.position;
    }

    void Update()
    {
        if (playerInWater)
        {
            CheckForMovementSplash();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInWater = true;
            lastPlayerPosition = other.transform.position;
            
            // Create initial splash when entering water
            CreateSplash(other.transform.position);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInWater = false;
            
            CreateSplash(other.transform.position);
        }
    }

    void CheckForMovementSplash()
    {
        Vector3 currentPosition = playerTransform.position;
        float movementDistance = Vector3.Distance(currentPosition, lastPlayerPosition);
        
        // Check if player has moved enough and cooldown has passed
        if (movementDistance > splashThreshold && Time.time - lastSplashTime > splashCooldown)
        {
            // Calculate splash position at player's feet
            Vector3 splashPosition = new Vector3(
                currentPosition.x, 
                transform.position.y + GetComponent<BoxCollider>().size.y, 
                currentPosition.z
            );
            
            CreateSplash(splashPosition);
            lastSplashTime = Time.time;
        }
        
        lastPlayerPosition = currentPosition;
    }

    void CreateSplash(Vector3 position)
    {
        if (splashParticles != null)
        {
            // Move particle system to splash position
            splashParticles.transform.position = position;
            
            // Play the particle effect
            splashParticles.Play();
        }
    }
}
