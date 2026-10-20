using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpiderSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] _spiderPrefabs;

    [Header("Attributes")]
    [SerializeField] private int _baseSpiders = 5;
    [SerializeField] private float _spidersPerSecond = 0.5f;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _diffScalingFactor = 0.75f;
    [SerializeField] private float _spidersPerSecondCap = 15f;

    [Header("Events")]
    public static UnityEvent OnSpiderDestroy = new UnityEvent();

    private int _currentWave = 1;
    private float _timeSinceLastSpawn;
    private int _spidersAlive;
    private int _spidersLeftToSpawn;
    private float _sps;
    private bool _isSpawning = false;

    private void Awake()
    {
        OnSpiderDestroy.AddListener(SpiderDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!_isSpawning)
        {
            return;
        }

        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn > 1f / _sps && _spidersLeftToSpawn > 0)
        {
            SpawnSpider();
            _spidersLeftToSpawn--;
            _spidersAlive++;
            _timeSinceLastSpawn = 0f;
        }

        if (_spidersAlive == 0 && _spidersLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void SpiderDestroyed()
    {
        _spidersAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(_timeBetweenWaves);

        _isSpawning = true;
        _spidersLeftToSpawn = SpidersPerWave();
        _sps = SpidersPerSecond();
    }

    private void EndWave()
    {
        _isSpawning = false;
        _timeSinceLastSpawn = 0f;
        _currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnSpider()
    {
        int index = Random.Range(0, _spiderPrefabs.Length);
        GameObject prefabToSpawn = _spiderPrefabs[index];

        Vector3 spawnPosition = LevelManager.Main.StartPoint.position;
        spawnPosition.z = -2f;

        GameObject spider = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }

    private int SpidersPerWave()
    {
        return Mathf.RoundToInt(_baseSpiders * Mathf.Pow(_currentWave, _diffScalingFactor));
    }

    private float SpidersPerSecond()
    {
        return Mathf.Clamp(_spidersPerSecond * Mathf.Pow(_currentWave, _diffScalingFactor), 0, _spidersPerSecondCap);
    }
}
