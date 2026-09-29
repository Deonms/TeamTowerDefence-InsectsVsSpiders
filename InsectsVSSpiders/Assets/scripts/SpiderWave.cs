using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] private float _speed;

    private WaveSpawner _waveSpawner;

    private float _countdown = 5f;

    //private int _currentWave = WaveSpawner.CurrentWave;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("the spider movement script loaded in, this is however a temporary script!"); // i have a feeling jamiro is gonna see this at aftekenen dag fully integrated and working lmao

        _waveSpawner = GetComponentInParent<WaveSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * _speed * Time.deltaTime);

        _countdown -= Time.deltaTime;

        if(_countdown <= 0)
        {
            Destroy(gameObject);

           // _waveSpawner.waves[WaveSpawner._currentWave].SpidersLeft--;
        }
    }
}
