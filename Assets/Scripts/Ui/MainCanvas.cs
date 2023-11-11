using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverElementContainer;

    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _restartText;
    [SerializeField] private TextMeshProUGUI _recordText;

    private readonly string _bestRecordStr = "bestRecord";

    private float _passedTime = 0;

    private bool _isGameOver = false;

    private void Start()
    {
        _gameOverElementContainer.SetActive(false);

        GameManager.Instance.GameOverEvent += OnGameOver;
    }

    private void FixedUpdate()
    {
        if (!_isGameOver)
        {
            _passedTime += Time.fixedDeltaTime;
            _timeText.text = $"{_passedTime: 0.0}";
        }
        else if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnGameOver()
    {
        _isGameOver = true;

        _gameOverElementContainer.SetActive(true);

        _recordText.text = $"Best Record : {FetchBestRecord(): 0.0}";
    }

    private float FetchBestRecord()
    {
        float bestRecord = PlayerPrefs.GetFloat(_bestRecordStr);

        if (_passedTime > bestRecord)
        {
            bestRecord = _passedTime;
            PlayerPrefs.SetFloat(_bestRecordStr, bestRecord);
        }

        return bestRecord;
    }
}
