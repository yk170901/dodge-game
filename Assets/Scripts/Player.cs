using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private Animator _animator;

    [SerializeField] private bool _enableDeath;

    private bool _isGameOver = false;

    private float _speed = 8f;

    private void Start() => GameManager.Instance.GameOverEvent += OnGameOver;
    private void OnGameOver()
    {
        _isGameOver = true;
        _rb.velocity = Vector3.zero;
    }

    private void Update()
    {
        if (_isGameOver) return;

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        _animator.SetBool("IsRunning", (moveDirection.x != 0 || moveDirection.z != 0));
        _rb.velocity = moveDirection * _speed;

        if (moveDirection != Vector3.zero)
            transform.forward = moveDirection;
    }

    public void Die()
    {
        if(_enableDeath)
            GameManager.Instance.EndGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Score"))
            Debug.Log("Score");
    }
}
