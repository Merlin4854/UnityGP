using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public StringVariable Nome;  
    public FloatVariable Hp;     
    public FloatVariable MaxHp;  
    public FloatVariable Speed;  
    public FloatVariable Damage; 

    private void Start()
    {
        // Assicurarsi che la vita attuale non superi la vita massima
        Hp.Value = MaxHp.Value;
    }

    public void TakeDamage(float damage)
    {
        Hp.Value -= damage;
        Hp.Value = Mathf.Clamp(Hp.Value, 0, MaxHp.Value);
        if (Hp.Value <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Logica per la morte dell'entità
        Debug.Log(Nome.Value + " è morto.");        
    }
}

