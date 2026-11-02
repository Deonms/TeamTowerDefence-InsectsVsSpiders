using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpiderSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] _spiderPrefabs;
    [Range(0f, 1f)]
    [SerializeField] private float _bigSpiderSpawnChance = 0.1f;
    [SerializeField] private GameObject[] _bigSpiderPrefabs;

    [Header("Attributes")]
    [SerializeField] private int _baseSpiders = 3;
    [SerializeField] private float _spidersPerSecond = 0.5f;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _diffScalingFactor = 0.75f;
    [SerializeField] private float _spidersPerSecondCap = 15f;

    [Header("Events")]
    // Wordt aangeroepen wanneer een spin sterft of de finish bereikt.
    public static UnityEvent OnSpiderDestroy = new UnityEvent();

    // Wordt aangeroepen wanneer een spin spawnt.
    public static UnityEvent OnSpiderSpawn = new UnityEvent();

    private int _currentWave = 1;
    private float _timeSinceLastSpawn;
    private int _spidersAlive;
    private int _spidersLeftToSpawn;
    private float _sps;
    private bool _isSpawning = false;

    private void Awake()
    {
        // Zorgt ervoor dat het aantal levende spinnen wordt bijgewerkt
        // wanneer het OnSpiderDestroy-event wordt aangeroepen.
        OnSpiderDestroy.AddListener(SpiderDestroyed);

        OnSpiderSpawn.AddListener(SpiderSpawned);
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
            _timeSinceLastSpawn = 0f;
        }

        if (_spidersAlive == 0 && _spidersLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    // Verhoog het aantal levende spinnen wanneer een baby verschijnt.
    private void SpiderSpawned()
    {
        _spidersAlive++;
    }

    // Verlaag het aantal levende spinnen wanneer een spin sterft.
    private void SpiderDestroyed()
    {
        _spidersAlive = Mathf.Max(0, _spidersAlive - 1);
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(_timeBetweenWaves);

        _isSpawning = true;

        // Bereken hoeveel spinnen deze wave bevat.
        _spidersLeftToSpawn = SpidersPerWave();

        // Bereken de spawnfrequentie voor deze wave.
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

        // Spawn een normale spin en verhoog het aantal levende spinnen.
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        _spidersAlive++;

        // Grote spinnen kunnen pas vanaf wave 3 verschijnen.
        // Random.value bepaalt of de ingestelde spawnkans wordt gehaald.
        if (_currentWave >= 3 &&_bigSpiderPrefabs.Length > 0 && Random.value < _bigSpiderSpawnChance)
        {
            index = Random.Range(0, _bigSpiderPrefabs.Length);
            prefabToSpawn = _bigSpiderPrefabs[index];

            // Spawn een grote spin en verhoog het aantal levende spinnen.
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            _spidersAlive++;
        }
    }

    private int SpidersPerWave()
    {
        // Bereken het aantal spinnen voor de huidige wave.
        // Het aantal neemt toe naarmate het wave-nummer hoger wordt.
        return Mathf.RoundToInt(_baseSpiders * Mathf.Pow(_currentWave, _diffScalingFactor));
    }

    private float SpidersPerSecond()
    {
        // Bereken de spawnfrequentie en zorg ervoor dat deze
        // niet boven de ingestelde maximale snelheid uitkomt.
        return Mathf.Clamp(_spidersPerSecond * Mathf.Pow(_currentWave, _diffScalingFactor), 0, _spidersPerSecondCap);
    }
}
