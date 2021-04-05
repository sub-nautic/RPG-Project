using UnityEngine;

public class RandomizeSounds : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips = null;

    AudioSource audioSource = null;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomAudioClip()
    {
        if (audioClips != null)
        {
            int index = UnityEngine.Random.Range(0, audioClips.Length);
            var randomClip = audioClips[index];
            audioSource.PlayOneShot(randomClip);
        }
    }
}
