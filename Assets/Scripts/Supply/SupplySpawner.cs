using Assets.Scripts.General;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Supply
{
    class SupplySpawner : MonoBehaviour, IGameOverSubscriber
    {
        private bool _isGameOver;

        public void OnGameOver()
        {
            _isGameOver = true;
        }
    }
}
