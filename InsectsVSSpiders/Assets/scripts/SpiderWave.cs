using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] private float _speed;

    private WaveSpawner _waveSpawner;

    private float _countdown = 5f;

    private int _currentWave;

    void Start()
    {
        print("the spider movement script loaded in, this is however a temporary script!");

        _waveSpawner = GetComponentInParent<WaveSpawner>();

        _currentWave = _waveSpawner.CurrentWave();
    }

    void Update()
    {
        transform.Translate(transform.right * _speed * Time.deltaTime);

        _countdown -= Time.deltaTime;

        if (_countdown <= 0)
        {
            Destroy(gameObject);

            _waveSpawner.waves[_currentWave].SpidersLeft--;
        }
    }
}