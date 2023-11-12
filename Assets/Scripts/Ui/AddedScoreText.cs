using UnityEngine;

public class AddedScoreText : MonoBehaviour
{
    private Vector3 _initPosition;

    private float _disappearTimer = 0;

    private void Awake() => _initPosition = transform.position;

    private void Update()
    {
        transform.position += 50 * Vector3.up * Time.deltaTime;

        _disappearTimer += Time.deltaTime;

        if (_disappearTimer >= 0.5)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        transform.position = _initPosition;
        _disappearTimer = 0;
    }
}