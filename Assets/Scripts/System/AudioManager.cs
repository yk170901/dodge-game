using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _bulletShootingClip;

    [SerializeField]
    private AudioClip _scorePointCollectedClip;

    [SerializeField]
    private AudioClip _hitClip;

    [SerializeField]
    private AudioClip _audioClip3;

    [SerializeField]
    private AudioClip _audioClip4;

    void Awake()
    {
        GameManager.Instance.GameOverEvent += OnGameOver;
        GameManager.Instance.ScoreUpEvent += OnScored;

        _audioSource = GetComponent<AudioSource>();

        FindObjectOfType<PlayerHealth>().HitEvent += OnBulletHitPlayer;


        Sniper[] snipers = FindObjectsOfType<Sniper>();

        for (int i = 0; i < snipers.Length; i++)
        {
            snipers[i].ShootEvent += PlayBulletShootingSound;
        }
    }

    private void OnGameOver()
    {

    }

    private void OnBulletHitPlayer()
    {
        _audioSource.PlayOneShot(_hitClip);
    }

    private void OnScored(float _)
    {
        _audioSource.PlayOneShot(_scorePointCollectedClip);
    }


    private void PlayBulletShootingSound()
    {
        _audioSource.PlayOneShot(_bulletShootingClip);
    }

}
