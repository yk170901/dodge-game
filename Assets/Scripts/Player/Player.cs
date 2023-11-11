using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private bool _enableDeath;

    private Rigidbody _rb;

    private Animator _animator;

    private bool _isGameOver = false;

    [SerializeField] private float _speed;
    //private float _speed = 7f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    internal void Score(int score)
    {
        Debug.Log(score + " get!");
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
        if (!_enableDeath) return; // variable for testing

        GameManager.Instance.EndGame();

        _isGameOver = true;

        _animator.SetTrigger("Die");

        _rb.velocity = Vector3.zero;
    }
}
