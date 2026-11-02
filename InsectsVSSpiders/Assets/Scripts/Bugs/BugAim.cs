using UnityEngine;

public class BugAim : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] protected Transform _Target;

    [Header("Projectile")]
    [SerializeField] protected GameObject _Projectile;
    [SerializeField] protected float _Pspeed = 20f;
    [SerializeField] protected float _Cooldown = 2f;
    [SerializeField] protected RangeBugs rangeBugs;

    protected bool ReadyToShoot = true;

    [Header("Prediction")]
    public float maxPredictionTime = 2f;

    protected Vector3 lastTargetPosition;
    protected Vector3 targetVelocity;

    private void Awake()
    {
        if (rangeBugs == null)
            rangeBugs = GetComponentInChildren<RangeBugs>();
    }

    protected virtual void Start()
    {
        if (_Target != null)
            lastTargetPosition = _Target.position;
    }

    protected virtual void Update()
    {
        Attack();
    }

    protected Vector3 PredictTargetPosition()
    {
        if (_Target == null)
            return transform.position;

        if (_Pspeed <= 0f)
            return _Target.position;

        Vector3 toTarget = _Target.position - transform.position;

        float predictionTime = toTarget.magnitude / _Pspeed;

        predictionTime = Mathf.Clamp(
            predictionTime,
            0f,
            Mathf.Max(0f, maxPredictionTime)
        );

        return _Target.position + targetVelocity * predictionTime;
    }

    protected void FireBall(Vector3 direction)
    {
        if (_Projectile == null)
        {
            Debug.LogError("Projectile is missing!");
            return;
        }

        GameObject instance = Instantiate(
            _Projectile,
            transform.position,
            Quaternion.identity
        );

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Projectile has no Rigidbody2D!");
            Destroy(instance);
            return;
        }

        // Find all colliders belonging to the tower.
        Collider2D[] towerColliders =
            GetComponentsInChildren<Collider2D>();

        // Find all colliders belonging to the projectile.
        Collider2D[] projectileColliders =
            instance.GetComponentsInChildren<Collider2D>();

        // Prevent the projectile from hitting its own tower.
        foreach (Collider2D towerCollider in towerColliders)
        {
            foreach (Collider2D projectileCollider in projectileColliders)
            {
                Physics2D.IgnoreCollision(
                    towerCollider,
                    projectileCollider,
                    true
                );
            }
        }

        rb.linearVelocity = direction.normalized * _Pspeed;
    }

    protected void ResetReadyToShoot()
    {
        ReadyToShoot = true;
    }

    protected void Attack()
    {
        if (rangeBugs == null)
            return;

        GameObject targetObject = rangeBugs.FirstTarget();

        if (targetObject == null)
        {
            _Target = null;
            return;
        }

        Transform newTarget = targetObject.transform;

        // Reset velocity prediction when the tower changes target.
        if (_Target != newTarget)
        {
            _Target = newTarget;
            lastTargetPosition = _Target.position;
            targetVelocity = Vector3.zero;
        }

        float dt = Mathf.Max(Time.deltaTime, 0.0001f);

        targetVelocity =
            (_Target.position - lastTargetPosition) / dt;

        lastTargetPosition = _Target.position;

        Vector3 predictedPosition = PredictTargetPosition();

        Vector3 direction = predictedPosition - transform.position;

        if (direction.sqrMagnitude < 0.001f)
            return;

        // 2D rotation.
        float angle = Mathf.Atan2(
            direction.y,
            direction.x
        ) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            720f * Time.deltaTime
        );

        if (ReadyToShoot)
        {
            ReadyToShoot = false;

            FireBall(direction.normalized);

            Invoke(
                nameof(ResetReadyToShoot),
                Mathf.Max(0f, _Cooldown)
            );
        }
    }
}