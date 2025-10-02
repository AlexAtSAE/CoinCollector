using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform body;
    public Vector2 cameraSensitivity;
    public float speed;
    public bool paused;
    public float jumpForce = 1.0f;
    
    private UserSettings userSettings;
    void Start()
    {
        inputManager = InputManager.instance;
        
        userSettings = UserSettings.instance;
        
        cam = cam.GetComponent<Transform>();
    }

    // Update is called once per frame
    float anglePitch = 0.0f;
    float angleYaw = 0.0f;
    void Update()
    {
        if (!userSettings.gamePaused)
            PlayerDefaultInputs();
            AffectPlayer();
    }


    void AffectPlayer()
    {
        Vector2 movementInputValue = inputManager.MovementInput.Value;
        if (!movementInputValue.Equals(Vector2.zero))
        {
            Vector3 fwd = body.forward * speed * Time.deltaTime * movementInputValue.y;
            Vector3 right = body.right * speed * Time.deltaTime * movementInputValue.x;
            rb.AddForce(fwd + right);
        }
        Vector2 cameraInputValue = inputManager.RotateView.Value;
        anglePitch += cameraInputValue.x * Time.deltaTime * userSettings.mouseSensitivity.x;
        angleYaw += cameraInputValue.y * Time.deltaTime * userSettings.mouseSensitivity.y;
        angleYaw = Mathf.Clamp(angleYaw, -75f, 80f);
        cam.rotation = Quaternion.Euler(-angleYaw, anglePitch, 0);
        body.rotation = Quaternion.Euler(0, anglePitch, 0);

        if (inputManager.JumpInput.Value)
        {
            rb.AddForce(new  Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
        

    }

    void PlayerDefaultInputs()
    {
        if (inputManager.InteractInput.InputPressed == true)
            Debug.Log($"Interact");

        if (inputManager.RefreshKeybinds.InputPressed == true)
        {
            inputManager.RefreshInputs();
        }
            
    }
}
