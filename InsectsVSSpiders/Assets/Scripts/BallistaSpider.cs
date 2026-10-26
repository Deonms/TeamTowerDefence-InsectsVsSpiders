
using UnityEngine;

public class BallistaSpider : SpiderMovement
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
            // Ignore this Ballista and dead spiders.
            if (spider == this || spider.IsDead)
            {
                continue;
            }

            // Do not throw the Mother Spider.
            if (spider.GetComponent<MotherSpider>() != null)
            {
                continue;
            }

            // Do not throw Newcastle Big Boy.
            if (spider.GetComponent<BigBoySpider>() != null)
            {
                continue;
            }

            // Do not throw Ballista Spider.
            if (spider.GetComponent<BallistaSpider>() != null)
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

        // Only one spider is selected per attack.
        if (nearestSpider == null)
        {
            return;
        }

        int destinationIndex = nearestSpider.PathIndex + _waypointsToThrow;

        // Do not throw onto the final waypoint.
        if (destinationIndex >= LevelManager.Main.Path.Length - 1)
        {
            return;
        }

        Debug.Log("Ballista threw: " + nearestSpider.name);

        nearestSpider.MoveToPathIndex(destinationIndex);

        // Start the cooldown.
        _nextThrowTime = Time.time + _throwCooldown;
    }
}
