using System.Collections.Generic;
using UnityEngine;

public class RangeBugs : MonoBehaviour
{
    private class EnemyEntry
    {
        public SpiderMovement enemy;
        public HashSet<int> colliderIds = new HashSet<int>();
    }

    private readonly List<EnemyEntry> enemies =
        new List<EnemyEntry>();

    private SpiderMovement GetEnemy(Collider2D other)
    {
        SpiderMovement enemy =
            other.GetComponentInParent<SpiderMovement>();

        if (enemy == null && other.attachedRigidbody != null)
        {
            enemy = other.attachedRigidbody
                .GetComponentInParent<SpiderMovement>();
        }

        return enemy;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SpiderMovement enemy = GetEnemy(other);

        if (enemy == null)
            return;

        EnemyEntry entry = enemies.Find(
            e => e.enemy == enemy
        );

        if (entry == null)
        {
            entry = new EnemyEntry();
            entry.enemy = enemy;
            enemies.Add(entry);
        }

        entry.colliderIds.Add(other.GetInstanceID());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        SpiderMovement enemy = GetEnemy(other);

        if (enemy == null)
            return;

        EnemyEntry entry = enemies.Find(
            e => e.enemy == enemy
        );

        if (entry == null)
            return;

        entry.colliderIds.Remove(other.GetInstanceID());

        if (entry.colliderIds.Count == 0)
        {
            enemies.Remove(entry);
        }
    }

    private void CleanTargets()
    {
        enemies.RemoveAll(entry => entry.enemy == null);
    }

    public GameObject FirstTarget()
    {
        CleanTargets();

        if (enemies.Count == 0)
            return null;

        return enemies[0].enemy.gameObject;
    }

    public GameObject LastTarget()
    {
        CleanTargets();

        if (enemies.Count == 0)
            return null;

        return enemies[enemies.Count - 1].enemy.gameObject;
    }

    public GameObject GetClosest(Vector3 position)
    {
        CleanTargets();

        SpiderMovement closest = null;
        float closestDistance = float.MaxValue;

        foreach (EnemyEntry entry in enemies)
        {
            SpiderMovement enemy = entry.enemy;

            if (enemy == null)
                continue;

            float distance =
                (enemy.transform.position - position).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }

        return closest != null ? closest.gameObject : null;
    }
}