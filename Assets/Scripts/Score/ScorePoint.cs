using Assets.Scripts.Player;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    private bool hasBeenCollected = false;

    private int score = 10;

    private void Awake()
    {
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PlayerMovement player)
            || hasBeenCollected) return;

        hasBeenCollected = true;
        GameManager.Instance.AddScore(score);
        GetComponentInChildren<ParticleSystem>().Stop();
    }
}
