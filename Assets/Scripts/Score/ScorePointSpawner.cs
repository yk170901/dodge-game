using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePointSpawner : MonoBehaviour, IGameOverSubscriber
{
    [SerializeField] private GameObject _scorePoint;

    private Transform _floor;

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
            Debug.Log(_floor.position.x + " " + _floor.position.y);

            // Random point in Floor goes in the middle
            MeshRenderer renderer = _floor.GetComponent<MeshRenderer>();

            float ranX = Random.Range(_floor.position.x - renderer.bounds.size.x/2, _floor.position.x + renderer.bounds.size.x/2);
            float ranZ = Random.Range(_floor.position.z - renderer.bounds.size.z/2, _floor.position.z + renderer.bounds.size.z/2);

            Instantiate(_scorePoint, new Vector3(ranX, 0, ranZ), Quaternion.identity);

            yield return new WaitForSeconds(5f);
        }
    }

    public void OnGameOver()
    {
        _isGameOver = true;
    }
}
