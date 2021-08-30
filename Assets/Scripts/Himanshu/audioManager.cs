using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Himanshu
{
    [RequireComponent(typeof(AudioSource))]
    public class audioManager : MonoBehaviour
    {
        private static audioManager m_instance;
        public static audioManager Instance => m_instance;


        [SerializeField] private Dictionary<string, AudioClip> m_clips;
        [SerializeField] private List<AudioClip> clips;
        private AudioSource m_audioSource;

        private void Awake()
        {
            if (m_instance == null)
                m_instance = this;
            else
                Destroy(this.gameObject);
            m_audioSource = GetComponent<AudioSource>();

            m_clips = new Dictionary<string, AudioClip>();

            foreach (var clip in clips)
            {
                m_clips.Add(clip.name.ToLower().Replace(" ", ""), clip);
            }
        }

        public void PlayClip(string _clip)
        {
            if (m_clips.TryGetValue(_clip, out AudioClip clip))
                m_audioSource.PlayOneShot(clip);

        }
    }
}
