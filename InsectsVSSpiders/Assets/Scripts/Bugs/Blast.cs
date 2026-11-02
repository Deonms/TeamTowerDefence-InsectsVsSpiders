using UnityEngine;


public class Blast : MonoBehaviour
{
    [SerializeField] private int _Damage = 1;
    [SerializeField] private float _LifeTime = 5f;

    private bool hasHit = false;

    private void Start()
    {
        Destroy(gameObject, _LifeTime);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasHit)
            return;

        SpiderMovement enemy =
            collision.gameObject.GetComponentInParent<SpiderMovement>();

        if (enemy != null)
        {
            hasHit = true;

            Debug.Log("Projectile hit enemy: " + enemy.name);

            enemy.TakeDamage(_Damage);
        }
        else
        {
            Debug.Log(
                "Projectile hit something else: " +
                collision.gameObject.name
            );
        }

        Destroy(gameObject);
    }
}