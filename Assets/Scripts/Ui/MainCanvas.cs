using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverElementContainer;
    [SerializeField] private GameObject _addedScoreContainer;

    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _restartText;
    [SerializeField] private TextMeshProUGUI _recordText;
    [SerializeField] private GameObject _addedScoreText;

    private readonly string _bestRecordStr = "bestRecord";

    private float _totalScore => _passedTime + _scorePoint;

    private float _scorePoint = 0;

    private float _passedTime = 0;

    private bool _isGameOver = false;

    private void Start()
    {
        _gameOverElementContainer.SetActive(false);
        _addedScoreText.SetActive(false);

        GameManager.Instance.GameOverEvent += OnGameOver;
        GameManager.Instance.ScoreUpEvent += (score) =>
        {
            _scorePoint += score;
            _addedScoreText.SetActive(true);
        };
    }

    private void FixedUpdate()
    {
        if (!_isGameOver)
        {
            _passedTime += Time.fixedDeltaTime;

            _timeText.text = $"{_totalScore: 0.0}";
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

        if (_totalScore > bestRecord)
        {
            bestRecord = _totalScore;
            PlayerPrefs.SetFloat(_bestRecordStr, bestRecord);
        }

        return bestRecord;
    }
}
