using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private Animator _animator;

    [SerializeField] private bool _enableDeath;

    private float _speed = 8f;

    private bool _isGameOver = false;
    
    private void Start() => GameManager.Instance.GameOverEvent += OnGameOver;
    private void OnGameOver()
    {
        _isGameOver = true;
        _rb.velocity = Vector3.zero;
    }

    private void Update()
    {
        if (_isGameOver) return;

        float horMove = Input.GetAxis("Horizontal");
        float verMove = Input.GetAxis("Vertical");

        _animator.SetBool("IsRunning", (horMove != 0 || verMove != 0));

        Vector3 value = new Vector3(horMove, 0, verMove) * _speed;
        _rb.velocity = value;
    }

    public void Die()
    {
        if(_enableDeath)
            GameManager.Instance.EndGame();
    }
}
