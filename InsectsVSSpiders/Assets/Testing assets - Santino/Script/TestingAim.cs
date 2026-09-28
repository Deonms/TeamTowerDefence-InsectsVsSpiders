using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TestingAim : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // The projectile prefab
    [SerializeField] private Transform target;          // The enemy transform
    [SerializeField] private float projectileSpeed = 20f; // Speed of your bullet/projectile
    [SerializeField] private Transform firePoint;       // Where the projectile spawns

    void Start()
    {
        InvokeRepeating(nameof(shoot), 0.5f, 0.5f); // Start shooting after 1 second
    }
    void Update()
    {
        if (target == null) return;

        Vector3 predictedPosition = CalculatePredictedPosition();

        // Aim at the predicted position
        transform.LookAt(predictedPosition);
        
    }

    public void shoot()
    {
        Vector3 predictedPosition = CalculatePredictedPosition();
        var instance = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(predictedPosition));
        instance.GetComponent<Rigidbody>().linearVelocity = (predictedPosition - firePoint.position).normalized * projectileSpeed;
    }

    Vector3 CalculatePredictedPosition()
    {
        // Try to get Rigidbody velocity, or fallback to custom movement tracking
        Vector3 targetVelocity = Vector3.zero;
        if (target.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            targetVelocity = rb.linearVelocity; // Use .velocity in Unity versions older than 6000.0
        }

        // Distance from gun to target
        float distance = Vector3.Distance(firePoint.position, target.position);

        // Approximate time for the projectile to reach the target
        float travelTime = distance / projectileSpeed;

        // Predict where the target will be after 'travelTime'
        return target.position + (targetVelocity * travelTime);
    }
}

