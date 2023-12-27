using Assets.Scripts.Player;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    private bool _hasBeenCollected = false;

    private const int SCORE = 10;

    private void Awake()
    {
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PlayerMovement player)
            || _hasBeenCollected) return;

        _hasBeenCollected = true;
        GameManager.Instance.AddScore(SCORE);
        GetComponentInChildren<ParticleSystem>().Stop();
    }
}
