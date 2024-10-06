using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RaycastCollect : MonoBehaviour
{
    public float raycastDistance = 5f;
    public Text collectPrompt;

    private GameObject currentCollectible;
    private InputActionSystem inputActions;

    private void Awake()
    {
        inputActions = new InputActionSystem();
    }

    private void OnEnable()
    {
        inputActions.Player.Collect.performed += OnCollect;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Collect.performed -= OnCollect;
        inputActions.Player.Disable();
    }

    private void Start()
    {
        collectPrompt.gameObject.SetActive(false);
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Collectible"))
            {
                currentCollectible = hit.collider.gameObject;
                collectPrompt.gameObject.SetActive(true);
            }
            else
            {
                collectPrompt.gameObject.SetActive(false);
                currentCollectible = null;
            }
        }
        else
        {
            collectPrompt.gameObject.SetActive(false);
            currentCollectible = null;
        }
    }

    private void OnCollect(InputAction.CallbackContext context)
    {
        if (currentCollectible != null)
        {
            CollectObject(currentCollectible);
        }
    }

    private void CollectObject(GameObject collectible)
    {
        Debug.Log("Oggetto raccolto: " + collectible.name);
        Destroy(collectible);
        collectPrompt.gameObject.SetActive(false);
    }
}
