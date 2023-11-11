using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameOverEventHandler();
    public event GameOverEventHandler GameOverEvent;
    public void EndGame() => GameOverEvent?.Invoke();

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

}
