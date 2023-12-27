using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Supply
{
    public abstract class SupplyItemBase : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(nameof(DisappearRoutine));       
        }

        private IEnumerator DisappearRoutine()
        {
            yield break;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement player))
            {
                OnPlayerCollectItem(other);

                AudioManager.Instance.PlayerItemPickUpSound();
                Destroy(gameObject);
            }
        }

        protected abstract void OnPlayerCollectItem(Collider other);
    }
}
