using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    public EntityStats entityStats;
    public TMP_Text nameText;      
    public Slider hpSlider;    
    public Transform uiTransform; 
    public Vector3 offset;     
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main; 

        if (entityStats == null)
        {
            Debug.LogError("EntityStats non assegnato nel componente EnemyUIController su " + gameObject.name);
            return;
        }

        if (entityStats.Nome != null)
        {
            nameText.text = entityStats.Nome.Value;
        }
        else
        {
            Debug.LogError("Nome non assegnato nello ScriptableObject EntityStats su " + entityStats.name);
        }

        if (hpSlider != null && entityStats.Hp != null)
        {
            hpSlider.maxValue = entityStats.MaxHp.Value;
            hpSlider.value = entityStats.Hp.Value;
        }
    }

    private void Update()
    {
        if (hpSlider != null && entityStats != null && entityStats.Hp != null)
        {
            hpSlider.value = entityStats.Hp.Value; 
        }

        // Aggiorna la posizione della UI sopra la testa del nemico
        if (uiTransform != null)
        {
            uiTransform.position = transform.position + offset;
            uiTransform.LookAt(uiTransform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        }
    }
}
