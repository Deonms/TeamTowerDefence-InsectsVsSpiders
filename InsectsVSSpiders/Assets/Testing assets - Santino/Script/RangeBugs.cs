using System.Collections.Generic;
using System.Linq;
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Target Entered");
       if( _Targets.Add(other.gameObject))
        {   
            OrderedEnemies.Add(other.gameObject); 
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_Targets.Remove(other.gameObject))
        { 
            OrderedEnemies.Remove(other.gameObject);
        }
        
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

        if(OrderedEnemies.Count == 0)
        {
            return null;
        }
        return OrderedEnemies.First();
    }
    public GameObject LastTarget()
    {
        _Targets.UnionWith(OrderedEnemies);
        _CurrentTarget = OrderedEnemies[^1];
        return _CurrentTarget;
    }

}
