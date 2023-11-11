using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePointSpawner : MonoBehaviour, IGameOverSubscriber
{
    [SerializeField] private GameObject _scorePoint;

    [SerializeField] private Transform _scorePointContainer;

    private Transform _floor;

    private float offset = 5;

    private bool _isGameOver;

    private void Awake()
    {
        _floor = GameObject.FindGameObjectWithTag("Floor").transform;

        StartCoroutine(nameof(SpawnRoutine));
    }

    private IEnumerator SpawnRoutine()
    {
        while (!_isGameOver)
        {
            float ranX = Random.Range(-8, 8);
            float ranZ = Random.Range(-8, 8);

            GameObject scorePoint = Instantiate(_scorePoint, transform.TransformPoint(new Vector3(ranX, 0, ranZ)), transform.rotation);

            scorePoint.transform.SetParent(_scorePointContainer);

            yield return new WaitForSeconds(0.001f);
        }
    }

    public void OnGameOver()
    {
        _isGameOver = true;
    }
}
