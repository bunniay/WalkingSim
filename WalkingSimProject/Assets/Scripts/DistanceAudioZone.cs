using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class DistanceAudioZone : MonoBehaviour
{
    [Header("Player Settings")]
    public string playerTag = "Player";

    [Header("Distance Settings")]
    public float maxDistance = 10f;
    public float insideVolume = 1f;
    public float fadeSpeed = 3f;

    private Transform player;
    private AudioSource audioSource;
    private Collider zoneCollider;
    private float targetVolume = 0f;

    void Start()
    {
        // Auto-find player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);

        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("No object with tag '" + playerTag + "' found.");

        audioSource = GetComponent<AudioSource>();
        zoneCollider = GetComponent<Collider>();

        zoneCollider.isTrigger = true;

        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // Full 3D audio
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(
            player.position,
            zoneCollider.ClosestPoint(player.position)
        );

        if (distance <= 0.1f)
        {
            targetVolume = insideVolume;

            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else if (distance <= maxDistance)
        {
            float t = 1f - (distance / maxDistance);
            targetVolume = t * insideVolume;

            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            targetVolume = 0f;
        }

        audioSource.volume = Mathf.Lerp(
            audioSource.volume,
            targetVolume,
            Time.deltaTime * fadeSpeed
        );

        if (audioSource.volume <= 0.01f && targetVolume == 0f && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}