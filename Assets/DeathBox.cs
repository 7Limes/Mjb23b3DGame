using UnityEngine;

public class DeathBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("loaded here");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetType() == typeof(BoxCollider))
        {
            Debug.Log("Collided with a BoxCollider: " + collision.gameObject.name);
            // Perform actions specific to colliding with a BoxCollider
        }
    }
}
