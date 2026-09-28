using UnityEngine;

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
        _target = LevelManager.Main._path[_pathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(_target.position, transform.position) <= 0.1f)
        {
            _pathIndex++;

            if (_pathIndex == LevelManager.Main._path.Length)
            {
                Destroy(gameObject);
                return;
            } else
            {
                _target = LevelManager.Main._path[_pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (_target.position - transform.position).normalized;

        _rb.linearVelocity = direction * _moveSpeed;
    }
}
