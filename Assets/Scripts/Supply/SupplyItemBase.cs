using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Supply
{
    public abstract class SupplyItemBase : MonoBehaviour
    {
        private float _disappearTimer = 0;

        private bool _isDisappearing = false;

        private void Update()
        {
            _disappearTimer += Time.deltaTime;

            if(_disappearTimer >= 4 && !_isDisappearing)
            {
                // dissolveEffect that's done in 1 sec
            }
            
            if(_disappearTimer > 5)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerCollectItem(other);

                AudioManager.Instance.PlayerItemPickUpSound();
                Destroy(gameObject);
            }
        }

        protected abstract void OnPlayerCollectItem(Collider other);
    }
}
