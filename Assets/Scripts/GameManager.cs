using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public delegate void GameOverEventHandler(object sender);
    public event GameOverEventHandler GameOverEvent;

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if(_instance == null) _instance = this;
    }

    public void EndGame() => GameOverEvent?.Invoke(this);
}
