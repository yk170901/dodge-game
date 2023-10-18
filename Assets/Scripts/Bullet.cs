using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly float _speed = 8f;
    [SerializeField] private Rigidbody _rb;

    public void SetTarget(Vector3 target)
    {
        _rb.velocity = _speed * (target - transform.position).normalized;
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision) => gameObject.SetActive(false);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.gameObject.GetComponent<Player>().Die();

        gameObject.SetActive(false);
    }
}
