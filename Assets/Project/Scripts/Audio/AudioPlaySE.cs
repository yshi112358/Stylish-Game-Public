using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;

namespace Project.Audio
{
    public class AudioPlaySE : MonoBehaviour
    {
        AudioSource _audioSource;
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            var scene = SceneManager.GetActiveScene();
            this.UpdateAsObservable()
                .Where(_ => SceneManager.GetActiveScene() != scene)
                .Where(_ => !_audioSource.isPlaying)
                .Subscribe(_ => Destroy(gameObject));
            DontDestroyOnLoad(gameObject);
        }
        public void PlayOneShot(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }

}