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

    [Header("Prediction")]
    public float maxPredictionTime = 2f;

    protected Vector3 lastTargetPosition;
    protected Vector3 targetVelocity;

    private float nextFireTime = 0f;

    private void Awake()
    {
        // Zoek automatisch naar RangeBugs op de toren of een child.
        if (rangeBugs == null)
        {
            rangeBugs = GetComponentInChildren<RangeBugs>(true);
        }

        if (rangeBugs == null)
        {
            Debug.LogError("Geen RangeBugs gevonden op " + gameObject.name);
        }
    }

    // Hiermee kan BuyingAInsectorSpider het projectiel instellen.
    public void SetProjectile(GameObject projectile)
    {
        if (projectile == null)
        {
            Debug.LogError("Er is een leeg projectile meegegeven aan " + gameObject.name);
            return;
        }

        _Projectile = projectile;
    }

    protected virtual void Start()
    {
        if (_Target != null)
        {
            lastTargetPosition = _Target.position;
        }
    }

    protected virtual void Update()
    {
        Attack();
    }

    protected Vector3 PredictTargetPosition()
    {
        if (_Target == null)
        {
            return transform.position;
        }

        if (_Pspeed <= 0f)
        {
            return _Target.position;
        }

        Vector2 towerPosition = transform.position;
        Vector2 targetPosition = _Target.position;

        float distance = Vector2.Distance(
            towerPosition,
            targetPosition
        );

        float predictionTime = distance / _Pspeed;

        predictionTime = Mathf.Clamp(
            predictionTime,
            0f,
            Mathf.Max(0f, maxPredictionTime)
        );

        Vector2 predictedPosition =
            targetPosition + (Vector2)targetVelocity * predictionTime;

        return new Vector3(
            predictedPosition.x,
            predictedPosition.y,
            _Target.position.z
        );
    }

    protected bool FireBall(Vector2 direction)
    {
        if (_Projectile == null)
        {
            Debug.LogError(
                "Projectile prefab is niet ingevuld op " + gameObject.name
            );

            return false;
        }

        Vector3 spawnPosition =
            transform.position + (Vector3)(direction.normalized * 0.5f);

        GameObject instance = Instantiate(
            _Projectile,
            spawnPosition,
            Quaternion.identity
        );

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Projectile heeft geen Rigidbody2D!");
            Destroy(instance);
            return false;
        }

        // Laat het projectiel bewegen.
        rb.linearVelocity = direction.normalized * _Pspeed;

        // Voorkom botsingen met de eigen toren.
        Collider2D[] projectileColliders =
            instance.GetComponentsInChildren<Collider2D>();

        Collider2D[] towerColliders =
            GetComponentsInChildren<Collider2D>();

        foreach (Collider2D projectileCollider in projectileColliders)
        {
            foreach (Collider2D towerCollider in towerColliders)
            {
                if (projectileCollider != null && towerCollider != null)
                {
                    Physics2D.IgnoreCollision(
                        projectileCollider,
                        towerCollider
                    );
                }
            }
        }

        return true;
    }

    protected void Attack()
    {
        if (rangeBugs == null)
        {
            return;
        }

        // Zoek automatisch de eerste vijand in het bereik.
        GameObject targetObject = rangeBugs.FirstTarget();

        if (targetObject == null)
        {
            _Target = null;
            targetVelocity = Vector3.zero;
            return;
        }

        Transform newTarget = targetObject.transform;

        // Reset de voorspelling als de toren een nieuwe vijand kiest.
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

        // Bereken de voorspelde positie van de vijand.
        Vector3 predictedPosition = PredictTargetPosition();

        Vector2 direction =
            (Vector2)(predictedPosition - transform.position);

        if (direction.sqrMagnitude < 0.001f)
        {
            return;
        }

        direction.Normalize();

        // Draai de toren naar de vijand.
        float angle = Mathf.Atan2(
            direction.y,
            direction.x
        ) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(
            0f,
            0f,
            angle
        );

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            720f * Time.deltaTime
        );

        // Schiet wanneer de cooldown voorbij is.
        if (Time.time >= nextFireTime)
        {
            bool shotFired = FireBall(direction);

            if (shotFired)
            {
                nextFireTime = Time.time + Mathf.Max(0f, _Cooldown);
            }
        }
    }
}