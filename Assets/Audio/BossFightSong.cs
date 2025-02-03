using UnityEngine;
using Audio;

public class BossFightSong : MonoBehaviour
{
    private AudioClip song;

    private void Start() {
        song = Resources.Load<AudioClip>("Music/TankBossFightSong");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance?.PlayLoopingMusic(song);
        }
    }

}
