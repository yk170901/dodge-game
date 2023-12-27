using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Supply
{
    class SupplySpawner : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.Instance.GameOverEvent += OnGameOver;
        }


        public void OnGameOver()
        {
        
        }
    }
}
