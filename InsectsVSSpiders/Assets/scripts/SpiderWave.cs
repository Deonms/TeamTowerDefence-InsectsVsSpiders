using UnityEngine;

public class Spider : MonoBehaviour
{
    private float _speed = 5;

    private WaveSpawner _waveSpawner;

    private float _countdown = 5f;

    private int _currentWave;

    void Start()
    {
        print("the spider movement script loaded in, this is however a temporary script!"); // i have a feeling jamiro is gonna see this at aftekenen dag fully integrated and working lmao

        _waveSpawner = GetComponentInParent<WaveSpawner>();

        _currentWave = _waveSpawner.CurrentWave();
    }

    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime, Space.World);

        _countdown -= Time.deltaTime;

        if (_countdown <= 0)
        {
            Destroy(gameObject);

            _waveSpawner.waves[_currentWave].SpidersLeft--;
        }
    }
}