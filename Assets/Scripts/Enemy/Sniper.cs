using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    [SerializeField] private Transform _bulletContainer;

    [SerializeField] private MeshRenderer _aimRenderer;

    public System.Action ShootEvent;

    private Transform _target;

    private MeshRenderer _renderer;

    private const float SHOOT_RATE_MIN = 0.3f;
    private float _shootRateMax = 5f;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();

        _target = FindObjectOfType<PlayerMovement>().transform;

        _renderer.enabled = false;
        _aimRenderer.enabled = false;

        GameManager.Instance.GameOverEvent += OnGameOver;
        StartCoroutine(nameof(ShootRoutine));
        StartCoroutine(nameof(RaiseDifficultyRoutine));
    }

    private IEnumerator RaiseDifficultyRoutine()
    {
        int count = 0;
        while(_shootRateMax > 3f)
        {
            count++;
            Debug.Log(count);
            _shootRateMax -= Time.deltaTime;

            yield return new WaitForSeconds(10f);
        }
    }

    private void OnGameOver()
    {
        StopCoroutine(nameof(ShootRoutine));

        _renderer.enabled = false;
        _aimRenderer.enabled = false;
    }

    private void ShootBullet()
    {
        Debug.DrawRay(transform.position, _target.position - transform.position, Color.red, 5f);

        //GameObject bullet = Instantiate(_bullet, transform.position, GetBulletRotation(GetFowardDirection().z - transform.rotation.z));
        GameObject bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
        //GameObject bullet = Instantiate(_bullet, transform.position, GetBulletRotation(Vector3.Angle(GetFowardDirection(), _target.position - transform.position)));
        bullet.transform.SetParent(_bulletContainer);
        bullet.GetComponent<Bullet>().SetDirection(_target.position - transform.position);
    }

    private IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(Random.Range(SHOOT_RATE_MIN, _shootRateMax));

        while (true)
        {
            _renderer.enabled = true;
            _aimRenderer.enabled = true;

            ShootEvent?.Invoke();
            ShootBullet();

            yield return new WaitForSeconds(0.2f);
            _aimRenderer.enabled = false;
            _renderer.enabled = false;

            yield return new WaitForSeconds(Random.Range(SHOOT_RATE_MIN, _shootRateMax));
        }
    }
}
