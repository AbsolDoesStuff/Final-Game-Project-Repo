using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource musicSource;  // Added music source

    [SerializeField] private AudioClip enemyHitClip;
    [SerializeField] private AudioClip playerHitClip;
    [SerializeField] private AudioClip projectileFiredClip;
    [SerializeField] private AudioClip enemyDeathClip;
    [SerializeField] private AudioClip playerDeathClip;

    public void PlayEnemyHitSound()
    {
        audioSource.PlayOneShot(enemyHitClip);
    }

    public void PlayPlayerHitSound()
    {
        audioSource.PlayOneShot(playerHitClip);
    }

    public void PlayProjectileFiredSound()
    {
        audioSource.PlayOneShot(projectileFiredClip);
    }

    public void PlayEnemyDeathSound()
    {
        audioSource.PlayOneShot(enemyDeathClip);
    }

    public void PlayPlayerDeathSound()
    {
        audioSource.PlayOneShot(playerDeathClip);
    }

    // Method to adjust the music volume
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
