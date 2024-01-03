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

        public static readonly float VerticalFallingDistance = 15;

        private const string FLOOR_TAG = "Floor";


        private void Awake()
        {
            GameManager.Instance.GameOverEvent += OnGameOver;
            StartCoroutine(nameof(FallRoutine));
        }

        private void OnGameOver()
        {
            // if not destroyed yet
            try
            {
                StopAllCoroutines();
                Destroy(gameObject);
            }
            catch (System.Exception)
            {
                Debug.Log("Tried Accessing to Destroyed Object");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("0. Supply Hit Something");

            if (other.CompareTag(FLOOR_TAG))
            {
                Debug.Log(" 1. It was Floor");
                StopCoroutine(nameof(FallRoutine));

                Debug.Log("2. Fall Routine has stopped");

                _rb.rotation = Quaternion.identity;

                Debug.Log("3. rotation is frozen");

                StartCoroutine(nameof(FoldParachutteRoutine));
                
                Debug.Log("4. Now Folding Parachutte");
            }

            Debug.Log("You should've seen 5 messages (including 0th message)");
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

                // TODO : Cannot use Rigidbody to move position. Find out why
                // https://forum.unity.com/threads/setting-rigidbody-position-sometimes-doesnt-change-the-position.1482804/
                //_rb.MovePosition(new Vector3(_rb.position.x, Mathf.Lerp(initialYPos, -0.01f, positionElapsedTime / 3), _rb.position.z));
                //_rb.position = new Vector3(_rb.position.x, Mathf.Lerp(initialYPos, -0.01f, positionElapsedTime / 3), _rb.position.z);
                // this one below works tho
                //transform.position = new Vector3(_rb.position.x, Mathf.Lerp(initialYPos, -0.01f, positionElapsedTime / 3), _rb.position.z);
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(initialYPos, -0.01f, positionElapsedTime / 3), transform.position.z);

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
            Debug.Log("FoldParachutteRoutine 0");
            // shrink parashutte height
            float minScale = 0.1f;
            float yElapsedTime = 0;
            float initialYScale = _parachutte.transform.localScale.y;

            while (_parachutte.transform.localScale.y > minScale)
            {
                Debug.Log("FoldParachutteRoutine 1 - while");
                yElapsedTime += Time.deltaTime;

                _parachutte.transform.localScale = new Vector3(
                    _parachutte.transform.localScale.x,
                    Mathf.Lerp(initialYScale, minScale, yElapsedTime / 1.9f),
                    _parachutte.transform.localScale.z
                    );

                yield return null;
            }

            Debug.Log("FoldParachutteRoutine 2");
            // shrink parashutte entirely
            float xZElapsedTime = 0;
            float initialXZScale = _parachutte.transform.localScale.x;
            float newXZScale = initialXZScale;

            while (newXZScale > minScale)
            {
                Debug.Log("FoldParachutteRoutine 3 - while");
                xZElapsedTime += Time.deltaTime;

                newXZScale = Mathf.Lerp(initialXZScale, minScale, xZElapsedTime / 0.5f);

                _parachutte.transform.localScale = new Vector3(
                    newXZScale,
                    _parachutte.transform.localScale.y,
                    newXZScale
                    );

                yield return null;
            }

            Debug.Log("FoldParachutteRoutine 4");
            GameObject supplyItemPrefab = _supplyItems[Random.Range(0, _supplyItems.Length)];
            //GameObject supplyItemPrefab = _supplyItems[2];

            GameObject supplyItem = Instantiate(supplyItemPrefab,
                                        transform.position + supplyItemPrefab.transform.position,
                                        supplyItemPrefab.transform.rotation);

            supplyItem.transform.SetParent(transform.parent);
            // TODO : make supply items always face camera

            Destroy(gameObject);
            Debug.Log("FoldParachutteRoutine 5");
        }
    }
}