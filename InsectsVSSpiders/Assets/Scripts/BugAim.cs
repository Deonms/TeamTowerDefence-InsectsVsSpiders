
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BugAim : MonoBehaviour
{
    [Header("Target")]
    [SerializeField]private Transform _Target;

    [Header("Projectile")]
    [SerializeField]private GameObject _Projectile;
    [SerializeField]private float _Pspeed = 20f;
    bool ReadyToShoot = true;
    [SerializeField]private float _Cooldown = 2f;
    [SerializeField]private RangeBugs rangeBugs;

    [Header("Prediction")]
    public float maxPredictionTime = 2f;
    

    private Vector3 lastTargetPosition;
    private Vector3 targetVelocity;

    void Start()
    {
        if (_Target != null)
            lastTargetPosition = _Target.position;
    }

    void Update()
    {
        if (rangeBugs.FirstTarget() == null)
            return;
        _Target = rangeBugs.FirstTarget().transform;

        if (_Target == null)
            return;

        // Estimate target velocity.
        targetVelocity = (_Target.position - lastTargetPosition) / Time.deltaTime;
        lastTargetPosition = _Target.position;

        // 
        Vector3 predictedPosition = PredictTargetPosition();
        

        Vector3 direction = predictedPosition - transform.position;

        if(ReadyToShoot == true)
        {
            ReadyToShoot = false;
            FireBall(direction.normalized);
            Invoke(nameof(ResetReadyToShoot), _Cooldown);
        }
        

        // drait als de richting van de voorspelde positie groter is dan een kleine waarde, dan roteer naar die richting
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);
            // wat is slerp?
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                10f * Time.deltaTime
            );
        }

        Debug.DrawLine(
            transform.position,
            predictedPosition,
            Color.red
        );
    }

    Vector3 PredictTargetPosition()
    {
        Vector3 toTarget = _Target.position - transform.position;

        float distance = toTarget.magnitude;

        // How long the projectile would take to reach
        // the target's current position.
        float predictionTime = distance / _Pspeed;

        predictionTime = Mathf.Clamp(
            predictionTime,
            0f,
            maxPredictionTime
        );

        return _Target.position + targetVelocity * predictionTime;
    }
    private void FireBall(Vector3 DD)
    {
        GameObject instance = Instantiate(_Projectile, transform.position, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().linearVelocity = DD * _Pspeed;

    }
    private void ResetReadyToShoot()
    {
        ReadyToShoot = true;
        
    }
}
