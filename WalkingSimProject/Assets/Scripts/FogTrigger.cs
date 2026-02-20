using UnityEngine;

public class FogTrigger : MonoBehaviour
{
    [Header("Fog Settings")]
    public GameObject fogPrefab;        // Assign if you want to spawn fog
    public GameObject existingFog;      // Assign if fog already exists in scene
    public bool spawnNewFog = true;

    [Header("Spawn Settings")]
    public Transform spawnPoint;        // Optional custom spawn point
    public bool destroyAfterTrigger = false;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (spawnNewFog && fogPrefab != null)
            {
                Vector3 spawnPosition = spawnPoint != null
                    ? spawnPoint.position
                    : transform.position;

                Instantiate(fogPrefab, spawnPosition, Quaternion.identity);
            }
            else if (!spawnNewFog && existingFog != null)
            {
                existingFog.SetActive(true);
            }

            if (destroyAfterTrigger)
            {
                Destroy(gameObject);
            }
        }
    }
}