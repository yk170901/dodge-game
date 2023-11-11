using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.GameOverEvent += OnGameOver;
    }

    public void OnGameOver()
    {
        Debug.Log("Sound Manager is Notified with Game Over");
    }
}
