using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePointSpawner : MonoBehaviour, IGameOverSubscriber
{
    [SerializeField] private GameObject _scorePoint;

    [SerializeField] private Transform _scorePointContainer;

    private void Awake()
    {
        StartCoroutine(nameof(SpawnRoutine));
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float ranX = Random.Range(-8, 8);
            float ranZ = Random.Range(-8, 8);

            GameObject scorePoint = Instantiate(_scorePoint, transform.TransformPoint(new Vector3(ranX, 0, ranZ)), transform.rotation);
            scorePoint.transform.SetParent(_scorePointContainer);

            yield return new WaitForSeconds(3);
        }
    }

    public void OnGameOver()
    {
        StopCoroutine(nameof(SpawnRoutine));
    }
}
