using UnityEngine;

public class DeathBox : MonoBehaviour {
    [Header("Respawn Settings")]
    public Transform respawnPoint;
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (respawnPoint != null) {
                other.enabled = false;
                other.transform.position = respawnPoint.position;
                other.enabled = true;
            }
        }
    }
}