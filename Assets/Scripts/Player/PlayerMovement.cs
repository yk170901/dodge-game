using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _rb;

        private Animator _animator;

        private bool _isGameOver = false;

        [SerializeField] private float _speed;
        //private float _speed = 7f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            GameManager.Instance.GameOverEvent += OnGameOver;
        }

        private void Update()
        {
            if (_isGameOver) return;

            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            _animator.SetBool("IsRunning", (moveDirection.x != 0 || moveDirection.z != 0));
            _rb.velocity = moveDirection * _speed; // _speed를 PlayerData에서 받아와서 사용하는 걸로. 이래야 speedUp Item 먹었을 때 적용됨

            if (moveDirection != Vector3.zero)
                transform.forward = moveDirection;
        }

        public void OnGameOver()
        {
            _isGameOver = true;
            _animator.SetTrigger("Die");
            _rb.velocity = Vector3.zero;
        }
    }
}
