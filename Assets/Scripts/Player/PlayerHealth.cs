using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private bool _enableDeath = true;

    [SerializeField] private GameObject[] _healthIndicator;
    [SerializeField] private GameObject[] _emptyHealthIndicator;

    [SerializeField] private ParticleSystem _shield;

    public Action HitPlayerEvent;
    public Action HitShieldEvent;

    private int _lives;

    private bool _isShielding = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ApplyShield();
        }
    }

    private void Awake()
    {
        _lives = _healthIndicator.Length;
        _shield.Stop();

        for (int i = 0; i < _emptyHealthIndicator.Length; i++)
        {
            _emptyHealthIndicator[i].SetActive(false);
        }
    }

    public void ApplyShield()
    {
        StartCoroutine(nameof(ShieldRoutine));
    }

    private IEnumerator ShieldRoutine()
    {
        _shield.Play();
        _isShielding = true;

        var particleSetting = _shield.main;
        particleSetting.simulationSpeed = 5f;
        yield return new WaitForSeconds(0.1f); 
        particleSetting.simulationSpeed = 1f;
        Debug.Log(particleSetting.simulationSpeed);

        yield return new WaitForSeconds(7); // shield time

        particleSetting.simulationSpeed = 5f;
        _isShielding = false;
        _shield.Stop();
    }

    public void TakeDamage()
    {
        if (_isShielding)
        {
            HitShieldEvent?.Invoke();
            return;
        }

        if (_lives <= 0)
            return;

        HitPlayerEvent?.Invoke();

        _lives--;

        _healthIndicator[_lives].SetActive(false);
        _emptyHealthIndicator[_lives].SetActive(true);

        if (_lives <= 0)
        {
            Die();
            return;
        }
    }

    public void RecoverHealth()
    {
        if (_lives >= _healthIndicator.Length)
            return;

        _healthIndicator[_lives].SetActive(true);
        _emptyHealthIndicator[_lives].SetActive(false);

        _lives++;
    }

    public void Die()
    {
        // variable for testing
        //if (!_enableDeath) return;

        GameManager.Instance.EndGame();
    }
}
