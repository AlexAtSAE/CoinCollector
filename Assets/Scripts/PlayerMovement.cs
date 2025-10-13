using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform body;
    public float speed;
    public float jumpForce = 1.0f;

    [HideInInspector] [SerializeField] private float damping;
    [Range(0, 1)][SerializeField] private float DefaultDamping;
    [Range(0, 1)][SerializeField] private float SlidingDamping;
    
    public UserSettings userSettings;
 
    private float jumpCD;
    private float DefaultCameraFOV;
    [SerializeField] private float DeltaCameraFOV;

    [HideInInspector] public bool sliding;
    void Start()
    {
        inputManager = InputManager.instance;
        DefaultCameraFOV = userSettings.FOV;
    }

    // Update is called once per frame
    float anglePitch = 0.0f;
    float angleYaw = 0.0f;
    void Update()
    {
        if (!userSettings.gamePaused)
        {
            PlayerDefaultInputs();
            AffectPlayer();
        }
    }


    void AffectPlayer()
    {
        Vector2 movementInputValue = inputManager.MovementInput.Value;
        //Move it
        if (!movementInputValue.Equals(Vector2.zero))
        {
            Vector3 fwd = body.forward * speed * Time.deltaTime * movementInputValue.y;
            Vector3 right = sliding ? Vector3.zero : body.right * speed * Time.deltaTime * movementInputValue.x;
            rb.linearVelocity += (fwd + right)/Mathf.Max(1,rb.linearVelocity.magnitude);
        }
        //Damp it
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * damping, rb.linearVelocity.y,rb.linearVelocity.z*damping);

        //Jump it
        if (inputManager.JumpInput.Value && jumpCD < 1)
        {
            rb.linearVelocity = rb.linearVelocity + new Vector3(0, jumpForce, 0);
            jumpCD += 1f;
        }
        //Cool it
        jumpCD = Mathf.Max(-0.1f, jumpCD-Time.deltaTime);
            
        //Rotate it
        Vector2 cameraInputValue = inputManager.RotateView.Value;
        anglePitch += cameraInputValue.x * Time.deltaTime * userSettings.mouseSensitivity.x;
        angleYaw += cameraInputValue.y * Time.deltaTime * userSettings.mouseSensitivity.y;
        angleYaw = Mathf.Clamp(angleYaw, -75f, 80f);
        cam.rotation = Quaternion.Euler(-angleYaw, anglePitch, 0);
        body.rotation = sliding ? body.rotation : Quaternion.Euler(0, anglePitch, 0);


    }

    void PlayerDefaultInputs()
    {
        if (inputManager.InteractInput.InputPressed)
            Debug.Log($"Interact");

        if (inputManager.RefreshKeybinds.InputPressed)
            inputManager.RefreshInputs();
        
        if(inputManager.SlidingInput.Value)
        {
            sliding = true;
            damping = SlidingDamping;
            Camera.main.fieldOfView = Mathf.Lerp(DefaultCameraFOV,DeltaCameraFOV+DefaultCameraFOV,inputManager.SlidingInput.TimeHeld/0.2f);
        }
        else
        {
            sliding = false;
            damping = DefaultDamping;
            Camera.main.fieldOfView = Mathf.Lerp(DefaultCameraFOV + DeltaCameraFOV, DefaultCameraFOV, inputManager.SlidingInput.TimeSinceRelease / 0.2f);
        }
            
    }
}
