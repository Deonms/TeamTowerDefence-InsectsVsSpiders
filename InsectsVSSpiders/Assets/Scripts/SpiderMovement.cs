using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpiderMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;

    [Header("Attributes")]
    [SerializeField] protected float _moveSpeed = 2f;
    [SerializeField] protected int _health = 3;

    protected Transform _target;
    protected int _pathIndex = 0;
    protected bool _isDead = false;

    public int PathIndex => _pathIndex;
    public bool IsDead => _isDead;

    protected virtual void Start()
    {
        _target = LevelManager.Main.Path[_pathIndex];
    }

    protected virtual void Update()
    {
        if (_isDead || _target == null)
        {
            return;
        }

        if (Vector2.Distance(_target.position, transform.position) <= 0.1f)
        {
            _pathIndex++;

            if (_pathIndex == LevelManager.Main.Path.Length)
            {
                Die();
                return;
            } else
            {
                // Stel het volgende punt in als nieuw doel
                _target = LevelManager.Main.Path[_pathIndex];
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if (_isDead || _target == null || _rb == null)
        {
            return;
        }

        // Bereken de richting van de spin naar het doel
        Vector2 direction = (_target.position - transform.position);

        float distanceToTarget = direction.magnitude;

        if (distanceToTarget < 0.1f)
        {
            transform.position = new Vector3(_target.position.x, _target.position.y, -2f);
            _rb.linearVelocity = Vector2.zero;
        }
        else
        {
            _rb.linearVelocity = direction.normalized * _moveSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            _rb.rotation = angle - 90f;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (_isDead)
        {
            return;
        }

        _health -= damage;

        if (_health <= 0) 
        { 
            Die();
        }
    }

    public void MoveToPathIndex(int index)
    {
        index = Mathf.Clamp(index, 0, LevelManager.Main.Path.Length - 1);

        _pathIndex = index;
        _target = LevelManager.Main.Path[_pathIndex];

        transform.position = new Vector3(_target.position.x, _target.position.y, -2f);

        if (_rb != null)
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }

    public void Die()
    {
        if (_isDead)
        {
            return;
        }

        _isDead = true;

        if (_rb != null)
        {
            _rb.linearVelocity = Vector2.zero;
        }

        OnDeath();

        SpiderSpawner.OnSpiderDestroy.Invoke();
        Destroy(gameObject);
    }

    protected virtual void OnDeath()
    {

    }
}
