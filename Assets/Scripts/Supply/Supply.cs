using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Supply
{
    class Supply : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;

        [SerializeField] private GameObject _parachutte;

        [SerializeField] private GameObject[] _supplyItems;

        private string _floorTag = "Floor";


        private void Awake()
        {
            StartCoroutine(nameof(FallRoutine));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_floorTag))
            {
                StopCoroutine(nameof(FallRoutine));

                _rb.rotation = Quaternion.identity;

                StartCoroutine(nameof(FoldParachutteRoutine));
            }
        }

        private IEnumerator FallRoutine()
        {
            float positionElapsedTime = 0;
            float rotationElapsedTime = 0;

            Quaternion temp;
            Quaternion from = new Quaternion() { eulerAngles = new Vector3(0, 0, 8) };
            Quaternion to = new Quaternion() { eulerAngles = new Vector3(0, 0, -8) };

            float initialYPos = _rb.position.y;

            while (true)
            {
                positionElapsedTime += Time.deltaTime;
                rotationElapsedTime += Time.deltaTime;

                _rb.position = new Vector3(_rb.position.x, Mathf.Lerp(initialYPos, -0.01f, positionElapsedTime / 6), _rb.position.z);

                _rb.rotation = Quaternion.Lerp(from, to, rotationElapsedTime / 1.6f);

                if (rotationElapsedTime >= 1.7f)
                {
                    rotationElapsedTime = 0;

                    temp = from;
                    from = to;
                    to = temp;
                }

                yield return null;
            }
        }

        private IEnumerator FoldParachutteRoutine()
        {
            // shrink parashutte height
            float minScale = 0.1f;
            float yElapsedTime = 0;
            float initialYScale = _parachutte.transform.localScale.y;

            while (_parachutte.transform.localScale.y > minScale)
            {
                yElapsedTime += Time.deltaTime;

                _parachutte.transform.localScale = new Vector3(
                    _parachutte.transform.localScale.x,
                    Mathf.Lerp(initialYScale, minScale, yElapsedTime / 1.9f),
                    _parachutte.transform.localScale.z
                    );

                yield return null;
            }

            // shrink parashutte entirely
            float xZElapsedTime = 0;
            float initialXZScale = _parachutte.transform.localScale.x;
            float newXZScale = initialXZScale;

            while (newXZScale > minScale)
            {
                xZElapsedTime += Time.deltaTime;

                newXZScale = Mathf.Lerp(initialXZScale, minScale, xZElapsedTime / 0.5f);

                _parachutte.transform.localScale = new Vector3(
                    newXZScale,
                    _parachutte.transform.localScale.y,
                    newXZScale
                    );

                yield return null;
            }

            GameObject supplyItem = _supplyItems[Random.Range(0, _supplyItems.Length)];

            Instantiate(supplyItem,
                transform.position + supplyItem.transform.position,
                supplyItem.transform.rotation);
            
            Destroy(gameObject);
        }
    }
}