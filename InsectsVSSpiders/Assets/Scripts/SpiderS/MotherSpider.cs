using UnityEngine;

public class MotherSpider : SpiderMovement
{
    [Header("Baby Spiders")]
    [SerializeField] private GameObject _babySpiderPrefab;
    [SerializeField] private int _babyCount = 3;
    [SerializeField] private float _spawnRadius = 0.5f;

    protected override void OnDeath()
    {
        base.OnDeath();

        if (_babySpiderPrefab == null)
        {
            return;
        }

        for (int i = 0; i < _babyCount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * _spawnRadius;

            GameObject baby = Instantiate(_babySpiderPrefab, transform.position + 
                (Vector3)offset, Quaternion.identity);

            SpiderMovement babyMovement = baby.GetComponent<SpiderMovement>();

            if (babyMovement != null)
            {
                babyMovement.InitializePathAtIndex(PathIndex);
            }

            // Laat de spawner deze baby meetellen als levende spin.
            SpiderSpawner.OnSpiderSpawn.Invoke();
        }
    }
}
