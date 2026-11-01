using Unity.VisualScripting;
using UnityEngine;

public class Blast : MonoBehaviour
{
    [SerializeField]private int _Damage = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        print("Blast hit " + collision.gameObject.name);
        //verander Health en TakeDamge naar wat jij al heb, ze zijn niet uniek
        collision.gameObject.GetComponent<SpiderMovement>().TakeDamage(1);
    }
}
