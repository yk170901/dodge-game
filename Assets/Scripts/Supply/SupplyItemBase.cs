using Assets.Scripts.Player;
using System;
using System.Collections;
using UnityEngine;

// helped me a lot
// https://chat.openai.com/c/1e825507-e3b4-4122-9bf0-c1e016d9c419
namespace Assets.Scripts.Supply
{
    public abstract class SupplyItemBase : MonoBehaviour
    {
        [SerializeField] protected Renderer dissolveRenderer;

        [SerializeField] protected Material dissolveMaterial;

        private float _disappearTimer = 0;

        private bool _isDisappearing = false;
        protected float dissolveSpeed = 0.2f;

        private bool _isDestroyed = false;

        private void OnDestroy()
        {
            _isDestroyed = true;
        }

        private void Awake()
        {
            GameManager.Instance.GameOverEvent += OnGameOver;
        }

        private void OnGameOver()
        {
            if(!_isDestroyed)
            {
                StopCoroutine(nameof(PlayDissolveEffect));

                gameObject.SetActive(false);

                Destroy(gameObject);
            }
        }

        private void Update()
        {
            _disappearTimer += Time.deltaTime;

            if(_disappearTimer >= 4 && !_isDisappearing)
            {
                _isDisappearing = true;
                StartCoroutine(PlayDissolveEffect());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isDestroyed
                && other.CompareTag("Player")
                && !_isDisappearing)
            {
                OnPlayerCollectItem(other);

                AudioManager.Instance.PlayerItemPickUpSound();

                StopCoroutine(nameof(PlayDissolveEffect));

                Destroy(gameObject);
            }
        }

        protected abstract void OnPlayerCollectItem(Collider other);

        protected virtual IEnumerator PlayDissolveEffect()
        {
            // assign clone for sine time. if directly assign the original, sine time is not new, and cause different shader behaviour depending on the sine time
            Material mat = new Material(dissolveMaterial);
            dissolveRenderer.material = mat;

            float progress = 0;

            while (progress < 1)
            {
                progress += Time.deltaTime * dissolveSpeed;
                mat.SetFloat("_DissolveProgress", progress);
                yield return null;
            }

            if(!_isDestroyed)
                Destroy(gameObject);
        }
    }
}
