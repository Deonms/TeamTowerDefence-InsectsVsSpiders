using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpiderMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;

    [Header("Attributes")]
    [SerializeField] private float _moveSpeed = 2f;

    private Transform _target;
    private int _pathIndex = 0;

    private void Start()
    {
        // Kies het eerste punt van het pad als doel
        _target = LevelManager.Main.Path[_pathIndex];
    }

    private void Update()
    {
        // Controleer of de spin dicht genoeg bij het huidige doel is
        if (Vector2.Distance(_target.position, transform.position) <= 0.1f)
        {
            // Ga naar het volgende punt van het pad
            _pathIndex++;

            // Controleer of het einde van het pad is bereikt
            if (_pathIndex == LevelManager.Main.Path.Length)
            {
                // Verwijder de spin wanneer hij het einde heeft bereikt
                Destroy(gameObject);
                return;
            } else
            {
                // Stel het volgende punt in als nieuw doel
                _target = LevelManager.Main.Path[_pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        // Bereken de richting van de spin naar het doel
        Vector2 direction = (_target.position - transform.position);

        // Bereken de afstand tussen de spin en het doel
        float distanceToTarget = direction.magnitude;

        // Controleer of de spin bijna bij het doel is
        if (distanceToTarget < 0.1f)
        {
            transform.position = _target.position;
            _rb.linearVelocity = Vector2.zero;
        }
        else
        {
            // Beweeg de spin richting het doel met de ingestelde snelheid
            _rb.linearVelocity = direction.normalized * _moveSpeed;
        }
    }
}
