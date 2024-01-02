using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Supply
{
    public abstract class SupplyItemBase : MonoBehaviour
    {
        [SerializeField] protected Renderer renderer;

        [SerializeField] protected Material dissolveMaterial;

        private float _disappearTimer = 0;

        private bool _isDisappearing = false;

        private void Update()
        {
            _disappearTimer += Time.deltaTime;

            if(_disappearTimer >= 4 && !_isDisappearing)
            {
                _isDisappearing = true;
                PlayDissolveEffect();
            }
            else if(_disappearTimer > 10)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")
                && !_isDisappearing)
            {
                OnPlayerCollectItem(other);

                AudioManager.Instance.PlayerItemPickUpSound();
                Destroy(gameObject);
            }
        }

        protected abstract void OnPlayerCollectItem(Collider other);

        // may need customization if
        // (1) there are multiple materials for the item like TimeSlower
        // (2) or one material that's applied to many parts of the model like SpeedBoost
        protected virtual void PlayDissolveEffect()
        {
            renderer.material = dissolveMaterial;
        }
    }
}
