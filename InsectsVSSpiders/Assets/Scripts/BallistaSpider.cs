using UnityEngine;

public class BallistaSpiderMovement : SpiderMovement
{
    [Header("Ballista Attack")]
    [SerializeField] private float _throwRange = 3f;
    [SerializeField] private float _throwCooldown = 5f;
    [SerializeField] private int _waypointsToThrow = 1;

    private float _nextThrowTime;

    protected override void Update()
    {
        base.Update();

        if (IsDead || Time.time < _nextThrowTime)
        {
            return;
        }

        SpiderMovement[] spiders = FindObjectsByType<SpiderMovement>(FindObjectsSortMode.None);

        SpiderMovement nearestSpider = null;
        float nearestDistance = _throwRange;

        foreach (SpiderMovement spider in spiders)
        {
            // Ignore this Ballista and spiders that are dead.
            if (spider == this || spider.IsDead)
            {
                continue;
            }

            // Only select spiders behind the Ballista.
            if (spider.PathIndex >= PathIndex) 
            {
                continue;
            }

            float distance = Vector2.Distance(transform.position, spider.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestSpider = spider;
            }
        }

        if (nearestSpider == null)
        {
            return;
        }

        int destinationIndex = nearestSpider.PathIndex + _waypointsToThrow;

        // Don't throw a spider onto the final waypoint.
        if (destinationIndex >= LevelManager.Main.Path.Length - 1)
        {
            Debug.Log("Ballista skipped a throw: target is too close to the end.");
            return;
        }

        Debug.Log("Ballista throwing " + nearestSpider.name + 
            " from waypoint " + nearestSpider.PathIndex +
            " to waypoint " + destinationIndex);

        nearestSpider.MoveToPathIndex(destinationIndex);

        _nextThrowTime = Time.deltaTime + _throwCooldown;
    }
}
