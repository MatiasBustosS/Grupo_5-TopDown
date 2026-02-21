using UnityEngine;

namespace GameKits.AudioSystem.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Audio Sources")]
        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioSource SFXSource;

        [Header("Audio Clips")]
        [SerializeField] AudioClip background;
        [SerializeField] AudioClip walk;
        [SerializeField] AudioClip death;

        private void Start()
        {
            musicSource.clip = background;
            musicSource.Play();
        }

        public void PlaySFX(AudioType audioType)
        {
            var clip = GetAudioClip(audioType);
            
            if (clip != null)
            {
                SFXSource.PlayOneShot(clip);
            }
        }

        private AudioClip GetAudioClip(AudioType audioType)
        {
            return audioType switch
            {
                AudioType.Walk => walk,
                AudioType.Death => death,
                _ => null,
            };
        }

        public enum AudioType
        {
            Walk,
            Death
        }
    }
}