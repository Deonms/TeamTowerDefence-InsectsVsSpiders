using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BugCannon : MonoBehaviour
{
    [SerializeField] private GameObject _Projectile;
    private float _Pspeed = 10f;
    [SerializeField] RangeBugs _Target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(FireProjectile), 0.5f, 0.5f);
    }

    private void FireProjectile()
    {
        /*GameObject instance = Instantiate(_Projectile, transform.position, Quaternion.identity);
        if(intceptiondirection(_Target.transform.position, transform.position, _Target.GetComponent<Rigidbody>().linearVelocity,_Pspeed, out var direction))
            instance.GetComponent<Rigidbody>().linearVelocity = direction * _Pspeed;
        else
            instance.GetComponent<Rigidbody>().linearVelocity = (_Target.transform.position - transform.position).normalized * _Pspeed;
        Vector3 predicted = AimPrediction.GetInterceptPoint(
    transform.position, _Target.transform.position, _Target.GetComponent<Rigidbody>().linearVelocity, _Pspeed);

        Vector3 direction = (predicted - transform.position).normalized;
        transform.forward = direction;*/
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget();
    }

    void FaceTarget()
    {

        Vector3 direction = (_Target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y,0));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);

    }
    public bool intceptiondirection(Vector3 a,Vector3 b,Vector3 vA, float sB, out Vector3 result)
    {
        var aTob = b - a; var dC = aTob.magnitude; var alpha = Vector3.Angle(aTob, vA) * Mathf.Deg2Rad; var sA = vA.magnitude; var r = sA / sB;
        if(CompountEyes.Prediction(a: 1-r*r, b: 2*r*dC*Mathf.Cos(alpha), c: -(dC*dC), out float root1, out float root2) == 0)
        {
            result = Vector3.zero;
            return false;
        }
        var dA = Mathf.Max(root1, root2);
        var t = dA / sB;
        var c = a + vA * t;
        result = (c - b).normalized;
        return true;
    }
}
public class CompountEyes
{
    public static int Prediction(float a, float b, float c, out float root1, out float root2)
    {
        float discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            root1 = Mathf.Infinity;
            root2 = -root1;
            return 0;
        }
        root1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
        root2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
        return discriminant> 0 ? 2 : 1;
    }
}

public static class AimPrediction
{
    public static Vector3 GetInterceptPoint(
        Vector3 shooterPosition,
        Vector3 targetPosition,
        Vector3 targetVelocity,
        float projectileSpeed)
    {
        Vector3 toTarget = targetPosition - shooterPosition;

        float a = Vector3.Dot(targetVelocity, targetVelocity)
                 - projectileSpeed * projectileSpeed;

        float b = 2f * Vector3.Dot(toTarget, targetVelocity);
        float c = Vector3.Dot(toTarget, toTarget);

        float discriminant = b * b - 4f * a * c;

        // No valid interception solution.
        if (discriminant < 0f)
            return targetPosition;

        float sqrt = Mathf.Sqrt(discriminant);

        float t1 = (-b - sqrt) / (2f * a);
        float t2 = (-b + sqrt) / (2f * a);

        float t = Mathf.Min(
            t1 > 0f ? t1 : float.PositiveInfinity,
            t2 > 0f ? t2 : float.PositiveInfinity
        );

        if (float.IsInfinity(t))
            return targetPosition;

        return targetPosition + targetVelocity * t;
    }
}
