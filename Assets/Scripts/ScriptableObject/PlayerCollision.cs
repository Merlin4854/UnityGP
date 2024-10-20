using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public EntityStats playerStats;
    public EntityStats enemyStats;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Il player e il nemico si toccano, entrambi subiscono danni
            playerStats.TakeDamage(enemyStats.Damage.Value);
            enemyStats.TakeDamage(playerStats.Damage.Value);
        }
    }
}
