using System.Collections;
using UnityEngine;

public class ScorePointSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _scorePoint;

    [SerializeField] private Transform _scorePointContainer;

    private void Awake()
    {
        StartCoroutine(nameof(SpawnRoutine));
        GameManager.Instance.GameOverEvent += OnGameOver;
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
            //yield return null;
        }
    }

    public void OnGameOver()
    {
        StopCoroutine(nameof(SpawnRoutine));

        _scorePointContainer.gameObject.SetActive(false);
    }
}
