using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float _countdown;



    [SerializeField] private GameObject _spawnPoint;

    public Wave[] waves;

    private int _currentWave = 0;



    private bool _readyToCountDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("countdown script is loaded in");

        _readyToCountDown = true;

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].SpidersLeft = waves[i].spiders.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (_readyToCountDown)
        {
            _countdown -= Time.deltaTime;
        }

        _countdown -= Time.deltaTime;

        if (_countdown <= 0)
        {
            _readyToCountDown = false;

            _countdown = waves[_currentWave].TimeToNextWave;

            StartCoroutine(SpawnWave());

        }


        if (waves[_currentWave].SpidersLeft == 0)
        {
            _readyToCountDown = true;
            _currentWave++;
        }
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < waves[_currentWave].spiders.Length; i++)
        {
            Spider spider = Instantiate(waves[_currentWave].spiders[i], _spawnPoint.transform);

            spider.transform.SetParent(_spawnPoint.transform);

            yield return new WaitForSeconds(waves[_currentWave].TimeToNextArray);
        }

    }

    public int CurrentWave()
    {
        return _currentWave;
    }


}



[System.Serializable]

public class Wave
{
    public Spider[] spiders;
    public float TimeToNextArray;
    public float TimeToNextWave;

    [HideInInspector] public int SpidersLeft;
}


// i have a feeling jamiro is gonna see this at aftekenen dag fully integrated and working lmao