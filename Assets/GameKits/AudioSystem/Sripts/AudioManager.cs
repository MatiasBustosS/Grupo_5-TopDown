using GameKits.InventorySystem.Scripts;
using System.Collections.Generic;
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
        [SerializeField] AudioClip grassWalk;
        [SerializeField] AudioClip death;

        public static AudioManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

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
                AudioType.Background => background,
                AudioType.GrassWalk => grassWalk,
                AudioType.Death => death,
                _ => null,
            };
        }

        public enum AudioType
        {
            Background,
            GrassWalk,
            Death
        }
    }
}