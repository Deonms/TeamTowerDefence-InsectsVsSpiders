using UnityEngine;

public class Slime : Blast
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null)
            return;

        // Slow-effect doorgeven aan de enemy.
        collision.gameObject.SendMessageUpwards(
            "Slimed",
            SendMessageOptions.DontRequireReceiver
        );

        // Ook normale Blast-functionaliteit uitvoeren.
        base.OnCollisionEnter2D(collision);
    }
}