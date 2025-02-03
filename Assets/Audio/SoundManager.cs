using UnityEngine;

namespace Audio
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        private AudioSource soundSource;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            soundSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip sound, float volume = 1f)
        {
            soundSource.PlayOneShot(sound, volume);
        }

        public void PlayLoopingMusic(AudioClip music)
        {
            if (soundSource.clip == music && soundSource.isPlaying)
                return;

            soundSource.clip = music;
            soundSource.loop = true;
            soundSource.Play();
        }

        public void StopMusic()
        {
            soundSource.Stop();
        }
    }
}
