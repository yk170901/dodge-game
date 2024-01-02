using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _bulletShootingClip;

    [SerializeField]
    private AudioClip _scorePointCollectedClip;

    [SerializeField]
    private AudioClip _hitPlayerClip;

    [SerializeField]
    private AudioClip _hitShieldClip;

    [SerializeField]
    private AudioClip _itemPickupClip;


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }


        GameManager.Instance.GameOverEvent += OnGameOver;
        GameManager.Instance.ScoreUpEvent += OnScored;

        _audioSource = GetComponent<AudioSource>();

        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();

        playerHealth.HitPlayerEvent += OnBulletHitPlayer;
        playerHealth.HitShieldEvent += OnBulletHitShield;


        Sniper[] snipers = FindObjectsOfType<Sniper>();

        for (int i = 0; i < snipers.Length; i++)
        {
            snipers[i].ShootEvent += OnShootingBullet;
        }
    }

    public void PlayerItemPickUpSound()
    {
        _audioSource.PlayOneShot(_itemPickupClip);
    }

    private void OnGameOver()
    {

    }

    private void OnBulletHitPlayer()
    {
        _audioSource.PlayOneShot(_hitPlayerClip);
    }

    private void OnBulletHitShield()
    {
        _audioSource.PlayOneShot(_hitShieldClip);
    }


    private void OnScored(float _)
    {
        _audioSource.PlayOneShot(_scorePointCollectedClip);
    }


    private void OnShootingBullet()
    {
        _audioSource.PlayOneShot(_bulletShootingClip);
    }

}
