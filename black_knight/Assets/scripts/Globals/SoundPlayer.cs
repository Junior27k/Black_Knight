using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    [Header("Lista de Sons")]
    public Sound[] sounds;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(string soundName)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == soundName)
            {
                audioSource.PlayOneShot(sound.clip);
                return;
            }
        }
        Debug.LogWarning("Som não encontrado: " + soundName);
    }
}
