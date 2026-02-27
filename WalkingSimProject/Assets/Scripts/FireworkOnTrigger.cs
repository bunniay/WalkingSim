using UnityEngine;

public class FireworkOnTrigger : MonoBehaviour
{
    public string targetTag = "Player";

    [Header("Firework VFX")]
    public ParticleSystem fireworkParticles;

    [Header("Firework SFX")]
    public AudioSource fireworkAudio;

    [Header("Options")]
    public bool playOnce = true;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag)) return;

        if (playOnce && hasPlayed) return;

        if (fireworkParticles != null) fireworkParticles.Play(true);
        if (fireworkAudio != null) fireworkAudio.Play();

        hasPlayed = true;
    }
}