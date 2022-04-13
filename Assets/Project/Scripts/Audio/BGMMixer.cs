using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMMixer : MonoBehaviour
{
    [SerializeField] private AudioSource _currentBGM;
    [SerializeField] private AudioSource _nextBGM;
    private Animator _animtor;
    public AudioClip CurrentBGM => _currentBGM.clip;
    public AudioClip NextBGM => _nextBGM.clip;
    private void Start()
    {
        _animtor = GetComponent<Animator>();
    }
    public void ToNextBGM(AudioClip clip)
    {
        if (_currentBGM.clip == clip || _nextBGM.clip == clip)
            return;
        _nextBGM.clip = clip;
        _animtor.SetTrigger("Mix");
        _nextBGM.Play();

    }
    private void OnMixExit()
    {
        _currentBGM.time = _nextBGM.time;
        _currentBGM.clip = _nextBGM.clip;
        _currentBGM.Play();
        _nextBGM.Stop();
    }
}
