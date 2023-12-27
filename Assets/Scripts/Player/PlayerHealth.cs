using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private bool _enableDeath = true;

    private int _lives;

    [SerializeField] private GameObject[] _healthIndicator;

    private void Awake()
    {
        _lives = _healthIndicator.Length;
    }

    public void TakeDamage()
    {
        if (_lives <= 0)
            return;

        _healthIndicator[--_lives].SetActive(false);

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

        _healthIndicator[_lives++].SetActive(true);
    }

    public void Die()
    {
        // variable for testing
        //if (!_enableDeath) return;

        GameManager.Instance.EndGame();
    }
}
