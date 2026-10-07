using System.Collections.Generic;
using UnityEngine;

public class RangeBugs : MonoBehaviour
{
    [SerializeField] HashSet<GameObject> _Targets = new HashSet<GameObject>();
    private GameObject _CurrentTarget;
    List<GameObject> OrderedEnemies = new List<GameObject>();

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _Targets.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        _Targets.Remove(other.gameObject);
    }
    public GameObject GetClosest(Vector3 targetPosition)
    {
        GameObject closest = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject obj in _Targets)
        {
            float distance = (obj.transform.position - targetPosition).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = obj;
            }
        }

        return closest;
    }


    public GameObject FirstTarget()
    {

        _Targets.UnionWith(OrderedEnemies);
        _CurrentTarget = OrderedEnemies[0];
        return _CurrentTarget;
    }
    public GameObject LastTarget()
    {
        _Targets.UnionWith(OrderedEnemies);
        _CurrentTarget = OrderedEnemies[^1];
        return _CurrentTarget;
    }

}
