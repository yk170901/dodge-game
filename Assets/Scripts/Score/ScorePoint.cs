using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    private bool hasBeenCollected = false;

    private int score = 100;

    private void Awake()
    {
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Player player)
            || hasBeenCollected) return;

        hasBeenCollected = true;
        GameManager.Instance.AddScore(score);
        GetComponentInChildren<ParticleSystem>().Stop();
    }
}
