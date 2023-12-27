using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private bool _enableDeath = true;

    [SerializeField] private GameObject[] _healthIndicator;
    [SerializeField] private GameObject[] _emptyHealthIndicator;

    public Action HitEvent;

    private int _lives;

    private void Awake()
    {
        _lives = _healthIndicator.Length;

        for(int i = 0; i < _emptyHealthIndicator.Length; i++)
        {
            _emptyHealthIndicator[i].SetActive(false);
        }
    }

    public void TakeDamage()
    {
        if (_lives <= 0)
            return;

        HitEvent?.Invoke();

        _lives--;

        _healthIndicator[_lives].SetActive(false);
        _emptyHealthIndicator[_lives].SetActive(true);

        Debug.Log(_lives);
        if (_lives <= 0)
        {
            Debug.Log("die");

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
