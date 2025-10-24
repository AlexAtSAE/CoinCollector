using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform body;
    [Header("----Movement Settings----")] 
    //Condense these into scriptable object??
    [Header("Speed")]
    public float speed;
    public float slideAccelerationSpeed;
    public float maxSpeed;
    public float maxSlidingSpeed;
    public float slideMinSpeed;
    [Header("Jumping")]
    public float jumpForce = 300.0f;
    public float AdditionalGravity = 30.0f;
    public float slideJumpMaxMultiplier = 3.0f;
    public float slideJumpSpeedToMultRatio = 50f;
    [SerializeField] private LayerMask jumpResetLayer; // ground
    
    [Header("Damping")]
    [Range(0, 1)][SerializeField] private float DefaultDamping;
    [Range(0, 1)][SerializeField] private float SlidingDamping;
    
    [Header("Global settings")]
    public UserSettings userSettings;
    
    private float DefaultCameraFOV;
    private float DeltaCameraFOV;

    [SerializeField] [CanBeNull] private TextMeshProUGUI speedText;
    [HideInInspector] public bool sliding;
    [HideInInspector] public bool canJump = true;
    void Start()
    {
        inputManager = InputManager.instance;
        DefaultCameraFOV = userSettings.FOV;
    }
    
    

    void Update()
    {
        if (!userSettings.gamePaused)
        {
            PlayerDefaultInputs();
            RotateCamera();
        }

    }
    void FixedUpdate()
    {
        if (!userSettings.gamePaused)
        {
            MovePlayer();
        }

        if (speedText != null) 
            speedText.text =$"HorizontalSpeed: {new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).magnitude}\n" + 
                            $"Speed: {rb.linearVelocity.magnitude}";
    }
    
    void MovePlayer()
    {
        rb.AddForce(Vector3.down * AdditionalGravity);
        Vector2 movementInputValue = inputManager.MovementInput.Value;
        //Move it
        if (!movementInputValue.Equals(Vector2.zero))
        {
            float mult = sliding && rb.linearVelocity.magnitude >= maxSpeed ? slideAccelerationSpeed : speed;
            Vector3 fwd =  mult * movementInputValue.y * body.forward ;
            Vector3 right = sliding ? Vector3.zero : mult *  movementInputValue.x * body.right;
            rb.AddForce(fwd + right);
        }
        
        //Damp it
        if(!sliding)
            DampVelocity(DefaultDamping,DefaultDamping);
        else
        {
            if (rb.linearVelocity.magnitude > slideMinSpeed) DampVelocity(SlidingDamping,SlidingDamping);
            else DampVelocity(1,1);
        }
        
        //Clamp it
        if(!sliding)
            ClampVelocity(maxSpeed);
        if(sliding)
            ClampVelocity(maxSlidingSpeed);
        
            

        //Jump it
        Vector2 xzVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z);
        if (Physics.Raycast(body.position - (Vector3.up * 0.99f), Vector3.down, 0.1f, jumpResetLayer)) 
            canJump = true;
        else canJump = false;
            
        if (inputManager.JumpInput.Value && canJump)
        {
            if (sliding) rb.AddForce(new Vector3(0, Mathf.Clamp(jumpForce * (xzVelocity.magnitude/slideJumpSpeedToMultRatio),jumpForce,jumpForce*slideJumpMaxMultiplier), 0));
            else rb.AddForce(new Vector3(0, jumpForce, 0));
            canJump = false;
        }
    }

    float cameraAnglePitch;
    float cameraAngleYaw;
    void RotateCamera()
    {
        Vector2 cameraInputValue = inputManager.RotateView.Value;
        cameraAnglePitch += cameraInputValue.x * userSettings.mouseSensitivity.x;
        cameraAngleYaw += cameraInputValue.y * userSettings.mouseSensitivity.y;
        cameraAngleYaw = Mathf.Clamp(cameraAngleYaw, -75f, 80f);
        cam.rotation = Quaternion.Euler(-cameraAngleYaw, cameraAnglePitch, 0);
        body.rotation = sliding ? body.rotation : Quaternion.Euler(0, cameraAnglePitch, 0);
    }

    void DampVelocity(float factorX, float factorZ, float factorY = 1.0f)
    {
        Vector3 velocity = rb.linearVelocity;
        velocity.x *= factorX;
        velocity.y *= factorY;
        velocity.z *= factorZ;
        rb.linearVelocity = velocity;
    }

    void ClampVelocity(float maxSpeed)
    {
        Vector2 xzVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z);
        if (xzVelocity.magnitude >= maxSpeed)
        {
            Vector2 xzVelNorm = xzVelocity.normalized;
            rb.linearVelocity = new Vector3(xzVelNorm.x*maxSpeed, rb.linearVelocity.y , xzVelNorm.y*maxSpeed);;
        }
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
            Camera.main.fieldOfView = Mathf.Lerp(DefaultCameraFOV,DeltaCameraFOV+DefaultCameraFOV,inputManager.SlidingInput.TimeHeld/0.2f);
        }
        else
        {
            sliding = false;
            Camera.main.fieldOfView = Mathf.Lerp(DefaultCameraFOV + DeltaCameraFOV, DefaultCameraFOV, inputManager.SlidingInput.TimeSinceRelease / 0.2f);
        }
            
    }
}
