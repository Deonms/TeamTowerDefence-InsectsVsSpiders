using UnityEngine;
using UnityEngine.Rendering;

public class TarantulaSpider : SpiderMovement
{
    [Header("Web Attack")]
    [SerializeField] private float _webRange = 4f;
    [SerializeField] private float _attackCooldown = 3f;
    [SerializeField] private float _stunDuration = 2f;
    [SerializeField] private LayerMask _insectLayer;

    private float _nextAttackTime;

    protected override void Update()
    {
        base.Update();

        if (IsDead || Time.time < _nextAttackTime)
        {
            return;
        }

        Collider2D[] insects = Physics2D.OverlapCircleAll(transform.position, _webRange, _insectLayer);

        foreach (Collider2D insect in insects)
        {
            InsectStun stun = insect.GetComponentInParent<InsectStun>();

            if (stun == null)
            {
                continue;
            }

            stun.ApplyStun(_stunDuration);
            _nextAttackTime = Time.deltaTime + _attackCooldown;
            break;
        }
    }
}
