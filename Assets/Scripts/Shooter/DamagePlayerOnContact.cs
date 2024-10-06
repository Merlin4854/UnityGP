using UnityEngine;

public class DamagePlayerOnContact : MonoBehaviour
{
    public int damageAmount = 10; // Quantità di danno da infliggere

    // Metodo chiamato quando un altro oggetto entra in collisione con questo oggetto
    private void OnCollisionEnter(Collision collision)
    {
        // Controlla se l'oggetto con cui abbiamo colliso ha il tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ottieni il componente PlayerHealth dallo script del player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Infligge il danno al player
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Controlla se l'oggetto con cui abbiamo colliso ha il tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ottieni il componente PlayerHealth dallo script del player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Infligge il danno al player
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}

