using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameOverEventHandler();
    public event GameOverEventHandler GameOverEvent;

    public void EndGame()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;

        GameOverEvent?.Invoke();
    }

    public Action<float> ScoreUpEvent;
    public void AddScore(float score) => ScoreUpEvent?.Invoke(score);

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    public void ApplyTimeSlower()
    {
        StartCoroutine(nameof(SlowDownTimeRoutine));
    }

    private IEnumerator SlowDownTimeRoutine()
    {
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(2.5f); // 5sec irl

        Time.timeScale = 1;
    }
}
