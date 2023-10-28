using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly float _speed = 10f;
    [SerializeField] private Rigidbody _rb;

    public void SetDirection(Vector3 direction)
    {
        _rb.velocity = _speed * direction.normalized;
        transform.forward = direction;

        // temporary fix
        // this is only for 'Bullet' prefab (the prefab model is built weird, so it needs to be modified)
        if (!gameObject.name.Contains("Strong"))
            transform.Rotate(new Vector3(90, 0, 0));
        
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) return;

        if (other.CompareTag("Player"))
            other.gameObject.GetComponent<Player>().Die();

        gameObject.SetActive(false);
    }
}
