using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float _countdown;

    public Wave[] waves;

    private int _currentWave = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("countdown script is loaded in");
    }

    // Update is called once per frame
    void Update()
    {
        _countdown -= Time.deltaTime;

        if (_countdown <= 0)
        {

            SpawnWave();
        }
    }

    private void SpawnWave()
    {

    }
}

[System.Serializable]

public class Wave
{
    public Spider[] spiders;
}
