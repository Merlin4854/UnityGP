using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "ScriptableObjects/PlayerMovementConfig", order = 1)]
public class PlayerMovementConfig : ScriptableObject
{
    public float force = 100;
    public float horizontalRotationSensibility = 3;
    public float verticalRotationSensibility = 1.5f;
    public float clampRange = 45f;
    public float cameraHeightOffset = 2f;
}
