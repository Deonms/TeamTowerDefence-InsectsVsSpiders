
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
            // Negeer deze Ballista en dode spinnen.
            if (spider == this || spider.IsDead)
            {
                continue;
            }

            // Niet de Moeder Spin gooien.
            if (spider.GetComponent<MotherSpider>() != null)
            {
                continue;
            }

            // Niet de Newcastle Big Boy gooien.
            if (spider.GetComponent<BigBoySpider>() != null)
            {
                continue;
            }

            // Niet de Ballista Spin gooien.
            if (spider.GetComponent<BallistaSpider>() != null)
            {
                continue;
            }

            // Alleen spinnen die achter de Ballista spin kiezen.
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

        // Alleen één spin wordt gekozen per aanval.
        if (nearestSpider == null)
        {
            return;
        }

        int destinationIndex = nearestSpider.PathIndex + _waypointsToThrow;

        // Niet naar de laatste tussenpunt gooien.
        if (destinationIndex >= LevelManager.Main.Path.Length - 1)
        {
            return;
        }

        nearestSpider.MoveToPathIndex(destinationIndex);

        // Start de cooldown.
        _nextThrowTime = Time.time + _throwCooldown;
    }
}
