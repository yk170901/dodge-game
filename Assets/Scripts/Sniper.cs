using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SniperPosition
{
    Undefined,
    Front,
    Back,
    Right,
    Left
}

public class Sniper : MonoBehaviour
{
    [SerializeField] private SniperPosition _sniperPosition;

    [SerializeField] private MeshRenderer _renderer;

    [SerializeField] private GameObject _bullet;

    [SerializeField] private Transform _bulletContainer;
    [SerializeField] private Transform _target;

    [SerializeField] private float _spawnRateMin = 1f;
    [SerializeField] private float _spawnRateMax = 5f;

    private float _spawnRate;
    private float _spawnTimer;

    private bool isGameOver = false;

    private void OnGameOver(object sender)
    {
        isGameOver = true;
        _renderer.enabled = false;
    }

    private void Start()
    {
        _renderer.enabled = false;
        ResetSpawnCondition();

        GameManager.Instance.GameOverEvent += OnGameOver;
    }

    private void Update()
    {
        if (isGameOver) return;

        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _spawnRate)
        {
            Debug.DrawRay(transform.position, GetFowardDirection(), Color.green, 15f);
            Debug.DrawRay(transform.position, _target.position - transform.position, Color.red, 5f);

            Debug.Log($"{Vector3.Angle(GetFowardDirection(), _target.position - transform.position)}");

            //GameObject bullet = Instantiate(_bullet, transform.position, GetBulletRotation(GetFowardDirection().z - transform.rotation.z));
            GameObject bullet = Instantiate(_bullet, transform.position, GetBulletRotation(Vector3.Angle(GetFowardDirection(), _target.position - transform.position)));
            bullet.transform.SetParent(_bulletContainer);
            bullet.GetComponent<Bullet>().SetDirection(_target.position - transform.position);

            StartCoroutine(nameof(AppearRoutine));
            ResetSpawnCondition();
        }
    }

    private Quaternion GetBulletRotation(float angleTowardsPlayer)
    {
        switch (_sniperPosition)
        {
            case SniperPosition.Front:
                return Quaternion.Euler(new Vector3(90, 0, 0 - angleTowardsPlayer));
            case SniperPosition.Back:
                return Quaternion.Euler(new Vector3(-90, 0, 0 - angleTowardsPlayer));
            case SniperPosition.Right:
                return Quaternion.Euler(new Vector3(0, -angleTowardsPlayer, -90));
            case SniperPosition.Left:
                return Quaternion.Euler(new Vector3(0, angleTowardsPlayer, 90));
            default:
                return Quaternion.identity;
        }
    }

    private Vector3 GetFowardDirection()
    {
        switch (_sniperPosition)
        {
            case SniperPosition.Front:
                return Vector3.forward;
            case SniperPosition.Back:
                return Vector3.back;
            case SniperPosition.Right:
                return Vector3.right;
            case SniperPosition.Left:
                return Vector3.left;
            default:
                return Vector3.zero;
        }
    }

    private IEnumerator AppearRoutine()
    {
        _renderer.enabled = true;
        yield return new WaitForSeconds(0.3f);
        _renderer.enabled = false;
    }

    private void ResetSpawnCondition()
    {
        _spawnRate = Random.Range(_spawnRateMin, _spawnRateMax);
        _spawnTimer = 0;
    }
}
