using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementSO : MonoBehaviour
{
    [SerializeField] private PlayerMovementConfig movementConfig; // Riferimento al ScriptableObject per i parametri
    private Camera _mainCamera;
    private InputActionSystem inputActions;
    private Rigidbody _rigidBody;
    private Vector3 _direction;
    private Vector2 _rotation;
    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;

    [SerializeField] private bool grounded = false;

    private void Awake()
    {
        inputActions = new InputActionSystem();

        inputActions.PlayerRotation.MouseAxis.performed += MouseAxisPerformed;
        inputActions.PlayerRotation.MouseAxis.canceled += MouseAxisPerformed;

        inputActions.PlayerMovementVector.Movement.performed += VectorMovementPerformed;

        inputActions.Enable();

        _rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _mainCamera = Camera.main;
        _rotation = _mainCamera.transform.rotation.eulerAngles;
    }

    private void MouseAxisPerformed(InputAction.CallbackContext context)
    {
        _rotation = context.ReadValue<Vector2>();

        if (context.phase == InputActionPhase.Canceled)
        {
            _rotation = Vector2.zero;
        }
    }

    private void VectorMovementPerformed(InputAction.CallbackContext obj)
    {
        _direction = obj.ReadValue<Vector3>();

        if (_direction == Vector3.zero)
            _rigidBody.velocity = Vector3.zero;
    }

    void Update()
    {
        CameraFollowPlayer();

        grounded = Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), -transform.up, out RaycastHit hitInfo, 3f, LayerMask.GetMask("Ground"));

        if (grounded)
        {
            Debug.Log("Grounded");
        }
    }

    private void FixedUpdate()
    {
        MovementUpdate();
    }

    private void LateUpdate()
    {
        RotationUpdate();
    }

    private void RotationUpdate()
    {
        horizontalRotation += _rotation.x * movementConfig.horizontalRotationSensibility * Time.smoothDeltaTime;
        verticalRotation += _rotation.y * movementConfig.verticalRotationSensibility * Time.smoothDeltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, -movementConfig.clampRange, movementConfig.clampRange);

        _mainCamera.transform.localRotation = Quaternion.Euler(-verticalRotation, horizontalRotation, 0);
        transform.rotation = Quaternion.Euler(0, _mainCamera.transform.rotation.eulerAngles.y, 0);
    }

    private void MovementUpdate()
    {
        var moveDirection =
            math.normalizesafe(transform.forward) * _direction.z
            + math.normalizesafe(transform.right) * _direction.x;

        _rigidBody.velocity = math.normalizesafe(moveDirection) * Time.fixedDeltaTime * movementConfig.force;
    }

    private void CameraFollowPlayer()
    {
        _mainCamera.transform.position =
            new Vector3(transform.position.x, transform.position.y + movementConfig.cameraHeightOffset, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 1000);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Vector3.down * 1f);
    }
}
