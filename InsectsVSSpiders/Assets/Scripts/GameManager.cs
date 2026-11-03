
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Andere scripts kunnen de GameManager via Main bereiken.
    public static GameManager Main { get; private set; }

    [Header("Lose Condition")]
    [SerializeField] private int _maxSpidersAtEnd = 5;
    [SerializeField] private string _gameOverSceneName = "Game Over";

    // Houdt bij hoeveel spinnen de finish hebben bereikt.
    private int _spidersAtEnd = 0;

    // Voorkomt dat de Game Over-scene meerdere keren wordt geladen.
    private bool _gameOver = false;

    private int _wavesSurvived = 0;

    // Houdt bij hoeveel waves de speler heeft overleefd.
    public int WavesSurvived => _wavesSurvived;

    private void Awake()
    {
        Main = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SpiderReachedEnd()
    {
        // Tel niet verder als de speler al verloren heeft.
        if (_gameOver)
        {
            return;
        }

        // Verhoog het aantal spinnen dat de finish bereikt.
        _spidersAtEnd++;

        // Controleer of de speler de lose-conditie heeft bereikt.
        if (_spidersAtEnd >= _maxSpidersAtEnd)
        {
            _gameOver = true;

            // Laad de Game Over-scene.
            SceneManager.LoadScene(_gameOverSceneName);
        }
    }

    public void SetWavesSurvived(int wave)
    {
        _wavesSurvived = wave;
    }
}
