using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    private int score = 100;

    private void Awake()
    {
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Player player)) return;

        player.Score(score);
        GetComponentInChildren<ParticleSystem>().Stop();
    }
}
