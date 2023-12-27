using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Supply
{
    class SupplySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _supply;
        [SerializeField] private Transform _supplyContainer;

        private float _spawnHeight = 13;

        private void Awake()
        {
            GameManager.Instance.GameOverEvent += OnGameOver;

            StartCoroutine(nameof(SpawnRoutine));
        }

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                float ranX = Random.Range(-8, 8);
                float ranZ = Random.Range(-8, 8);

                GameObject scorePoint = Instantiate(_supply,
                    transform.TransformPoint(new Vector3(ranX, Supply.VerticalFallingDistance, ranZ)),
                    transform.rotation);

                scorePoint.transform.SetParent(_supplyContainer);

                //yield return new WaitForSeconds(3);
                yield return new WaitForSeconds(1);
            }
        }

        public void OnGameOver()
        {
            StopCoroutine(nameof(SpawnRoutine));
        }
    }
}
