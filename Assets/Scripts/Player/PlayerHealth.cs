using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Salute massima del player
    private int currentHealth;  // Salute attuale del player
    public Slider healthSlider; // Riferimento allo Slider della salute

    // Metodo chiamato all'inizio del gioco
    void Start()
    {
        currentHealth = maxHealth; // Imposta la salute attuale alla salute massima
        healthSlider.maxValue = maxHealth; // Imposta il valore massimo dello Slider
        healthSlider.value = currentHealth; // Imposta il valore iniziale dello Slider
    }

    // Metodo per infliggere danno al player
    public void TakeDamage(int amount)
    {
        currentHealth -= amount; // Riduce la salute attuale

        // Aggiorna il valore dello Slider della salute
        healthSlider.value = currentHealth;

        // Controlla se la salute attuale è minore o uguale a zero
        if (currentHealth <= 0)
        {
            Die(); // Chiama il metodo Die se la salute è zero o inferiore
        }
    }

    // Metodo per gestire la morte del player
    void Die()
    {
        // Implementa la logica per la morte del player (es. ricarica il livello, mostra una schermata di game over, ecc.)
        Debug.Log("Player is dead!");
        // Aggiungi qui la logica desiderata per la morte del player
    }
}
