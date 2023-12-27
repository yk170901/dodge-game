using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    [SerializeField] private Transform _bulletContainer;

    [SerializeField] private MeshRenderer _aimRenderer;

    private Transform _target;

    private MeshRenderer _renderer;

    private float _shootRateMin = 1f;
    private float _shootRateMax = 5f;
    private float _shootRate;
    private float _shootTimer;

    private bool isGameOver = false;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();

        _target = FindObjectOfType<PlayerMovement>().transform;

        _renderer.enabled = false;
        _aimRenderer.enabled = false;

        ResetShootingCondition();

        GameManager.Instance.GameOverEvent += OnGameOver;
    }

    private void OnGameOver()
    {
        isGameOver = true;
        _renderer.enabled = false;
        _aimRenderer.enabled = false;
    }

    private void Update()
    {
        if (isGameOver) return;

        _shootTimer += Time.deltaTime;

        if (_shootTimer >= _shootRate)
        {
            Debug.DrawRay(transform.position, _target.position - transform.position, Color.red, 5f);

            //GameObject bullet = Instantiate(_bullet, transform.position, GetBulletRotation(GetFowardDirection().z - transform.rotation.z));
            GameObject bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            //GameObject bullet = Instantiate(_bullet, transform.position, GetBulletRotation(Vector3.Angle(GetFowardDirection(), _target.position - transform.position)));
            bullet.transform.SetParent(_bulletContainer);
            bullet.GetComponent<Bullet>().SetDirection(_target.position - transform.position);

            StartCoroutine(nameof(AppearRoutine));
            ResetShootingCondition();
        }
    }

    private IEnumerator AppearRoutine()
    {
        _renderer.enabled = true;
        _aimRenderer.enabled = true;
        yield return new WaitForSeconds(0.3f);
        _aimRenderer.enabled = false;
        _renderer.enabled = false;
    }

    private void ResetShootingCondition()
    {
        _shootRate = Random.Range(_shootRateMin, _shootRateMax);
        _shootTimer = 0;
    }
}
