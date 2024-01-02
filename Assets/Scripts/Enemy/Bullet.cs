﻿using Assets.Scripts.Player;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly float _speed = 13f;
    
    public void SetDirection(Vector3 direction)
    {
        GetComponent<Rigidbody>().velocity = _speed * direction.normalized;
        transform.forward = direction;

        // temporary fix
        // this is only for 'Bullet' prefab (the prefab model is built weird, so it needs to be modified)
        if (!gameObject.name.Contains("Strong"))
            transform.Rotate(new Vector3(90, 0, 0));

        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHp = FindObjectOfType<PlayerHealth>();
            playerHp.TakeDamage();
        }
        else if (!other.CompareTag("Fence"))
            return;

        gameObject.SetActive(false);
    }
}
