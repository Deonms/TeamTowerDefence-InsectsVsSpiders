using System.Collections.Generic;
using UnityEngine;

public class RangeBugs : MonoBehaviour
{
    private readonly List<GameObject> OrderedEnemies =
        new List<GameObject>();

    private readonly Dictionary<GameObject, int> ColliderCounts =
        new Dictionary<GameObject, int>();

    private GameObject GetEnemy(Collider2D other)
    {
        if (other.attachedRigidbody != null)
            return other.attachedRigidbody.gameObject;

        return other.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject enemy = GetEnemy(other);

        if (ColliderCounts.ContainsKey(enemy))
        {
            ColliderCounts[enemy]++;
        }
        else
        {
            ColliderCounts.Add(enemy, 1);
            OrderedEnemies.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject enemy = GetEnemy(other);

        if (!ColliderCounts.ContainsKey(enemy))
            return;

        ColliderCounts[enemy]--;

        if (ColliderCounts[enemy] <= 0)
        {
            ColliderCounts.Remove(enemy);
            OrderedEnemies.Remove(enemy);
        }
    }

    private void CleanTargets()
    {
        for (int i = OrderedEnemies.Count - 1; i >= 0; i--)
        {
            if (OrderedEnemies[i] == null)
            {
                ColliderCounts.Remove(OrderedEnemies[i]);
                OrderedEnemies.RemoveAt(i);
            }
        }
    }

    public GameObject FirstTarget()
    {
        CleanTargets();

        if (OrderedEnemies.Count == 0)
            return null;

        return OrderedEnemies[0];
    }

    public GameObject LastTarget()
    {
        CleanTargets();

        if (OrderedEnemies.Count == 0)
            return null;

        return OrderedEnemies[OrderedEnemies.Count - 1];
    }

    public GameObject GetClosest(Vector3 targetPosition)
    {
        CleanTargets();

        GameObject closest = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject enemy in OrderedEnemies)
        {
            float distance =
                (enemy.transform.position - targetPosition).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }
}