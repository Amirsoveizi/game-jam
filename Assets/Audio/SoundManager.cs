using UnityEngine;

namespace Audio
{
    
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance {get; private set;}
        private AudioSource soundSource;


        private void Awake() 
        {
            if (Instance != null)
                Destroy(gameObject);

            Instance = this;

            soundSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip sound, float volume = 1.0f)
        {
            soundSource.PlayOneShot(sound, volume);
        }


    }




}

