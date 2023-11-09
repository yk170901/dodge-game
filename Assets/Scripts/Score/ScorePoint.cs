using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    private ParticleSystem _particleSys;

    private int score = 100;

    private void Awake()
    {
        _particleSys = GetComponentInChildren<ParticleSystem>();
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Player player)) return;

        player.Score(score);
        Deactivate();
    }

    private void Deactivate() => _particleSys.Stop();
}
