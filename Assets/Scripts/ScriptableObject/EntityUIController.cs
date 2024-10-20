using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityUIController : MonoBehaviour
{
    public EntityStats entityStats;
    public TMP_Text nameText;      
    public Slider hpSlider;

    private void Start()
    {
        if (entityStats == null)
        {
            Debug.LogError("EntityStats non assegnato nel componente EntityUIController su " + gameObject.name);
            return;
        }

        // Assegna il nome all'elemento UI
        if (entityStats.Nome != null)
        {
            nameText.text = entityStats.Nome.Value;
        }
        else
        {
            Debug.LogError("Nome non assegnato nello ScriptableObject EntityStats su " + entityStats.name);
        }

        // Assegna i valori alla barra della vita
        if (hpSlider != null && entityStats.Hp != null && entityStats.MaxHp != null)
        {
            // Assegna i valori della vita
            hpSlider.maxValue = entityStats.MaxHp.Value;
            // Imposta il valore attuale di Hp
            hpSlider.value = entityStats.Hp.Value; 
        }
        else
        {
            if (hpSlider == null)
                Debug.LogError("hpSlider non assegnato nel componente EntityUIController su " + gameObject.name);
            if (entityStats.Hp == null)
                Debug.LogError("Hp non assegnato nello ScriptableObject EntityStats su " + entityStats.name);
            if (entityStats.MaxHp == null)
                Debug.LogError("MaxHp non assegnato nello ScriptableObject EntityStats su " + entityStats.name);
        }
    }


    private void Update()
    {
        // Aggiornamento della UI solo se tutto è assegnato
        if (hpSlider != null && entityStats != null && entityStats.Hp != null)
        {
            hpSlider.value = entityStats.Hp.Value;
        }
    }
}
