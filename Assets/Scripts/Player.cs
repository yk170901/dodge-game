using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private bool _enableDeath;

    private float _speed = 8f;

    private bool _isGameOver = false;
    
    private void Start() => GameManager.Instance.GameOverEvent += OnGameOver;

    private void Update()
    {
        if (_isGameOver) return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 value = new Vector3(x, 0, z) * _speed;
        _rb.velocity = value;
    }

    private void OnGameOver(object sender)
    {
        _isGameOver = true;
        _rb.velocity = Vector3.zero;
    }

    public void Die()
    {
        if(_enableDeath)
            GameManager.Instance.EndGame();
    }
}
