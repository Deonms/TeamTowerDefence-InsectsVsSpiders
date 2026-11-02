using System.Collections.Generic;
using UnityEngine;

public class BlackWidowSpider : SpiderMovement
{
    [Header("Dodge Attributes")]
    [SerializeField, Range(0f, 1f)]
    private float _baseDodgeChance = 0.2f;

    [SerializeField, Range(0f, 1f)]
    private float _chanceIncreasePerHit = 0.15f;

    [SerializeField, Range(0f, 1f)]
    private float _maxDodgeChance = 0.8f;

    [SerializeField] private float _detectionRadius = 2f;
    [SerializeField] private float _dodgeDistance = 1.5f;
    [SerializeField] private float _dodgeSpeed = 8f;
    [SerializeField] private LayerMask _projectileLayer;

    private float _currentDodgeChance;
    private Vector2 _dodgeTarget; 
    private bool _isDodging;

    private readonly HashSet<int> _checkedProjectiles = new();

    protected override void Start()
    {
        base.Start();
        _currentDodgeChance = _baseDodgeChance;
    }

    protected override void Update()
    {
        base.Update();

        if (IsDead)
        {
            return;
        }

        if (_isDodging)
        {
            if (Vector2.Distance(transform.position, _dodgeTarget) < 0.05f)
            {
                _isDodging = false;
            }

            return;
        }

        Collider2D[] projectiles = Physics2D.OverlapCircleAll(transform.position, _detectionRadius, _projectileLayer);

        foreach (Collider2D projectile in projectiles)
        {
            int id = projectile.gameObject.GetInstanceID();

            if (!_checkedProjectiles.Add(id))
            {
                continue;
            }

            if (Random.value < _currentDodgeChance)
            {
                Vector2 away = (Vector2)transform.position - (Vector2)projectile.transform.position;

                if (away.sqrMagnitude < 0.001f)
                {
                    away = Vector2.right;
                }

                away.Normalize();

                Vector2 sideways = new Vector2(-away.y, away.x);

                _dodgeTarget = (Vector2)transform.position + sideways * _dodgeDistance;

                _isDodging = true;
                _currentDodgeChance = _baseDodgeChance;

                break;
            }
        }
    }

    protected override void FixedUpdate()
    {
        if (IsDead || _rb == null)
        {
            return;
        }

        if (_isDodging)
        {
            Vector2 nextPosition = Vector2.MoveTowards(_rb.position, _dodgeTarget,
                _dodgeSpeed * Time.fixedDeltaTime);

            _rb.MovePosition(nextPosition);
            return;
        }

        base.FixedUpdate();
    }

    public override void TakeDamage(int damage)
    {
        if (IsDead)
        {
            return;
        }

        _currentDodgeChance = Mathf.Min(_currentDodgeChance + _chanceIncreasePerHit, _maxDodgeChance);
    }
}
