using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerMovement Variables")]
    private Vector3 movementDir;
    private Vector3 movementVector;
    float movSpeed;
    float movSpeedPen;
    private float defaultMovSpeed;
    public float MovSpeed
    {
        get { return movSpeed; }
        set { movSpeed = value; }
    }
    public bool CanMove { get; set; }
    private Vector3 smoothedMovementInput;
    private Vector3 movementInputSpeedVelocity;

    [Header("External References")]
    [SerializeField] Animator playerAnim;

   
    [Header("Dash Variables")]
    private float dashSpeed;
    private float dashDuration;
    private float dashCooldown;
    [SerializeField] private GameObject dashEffect;
    private Vector3 dashTarget;
    private bool isDashing;

    [Header("Animation Strings")]
    private const string isRunning = "isRunning";
    private const string isDashingStr = "isDashing";

    [Header("Collision Variables")]
    [SerializeField] LayerMask obstacleLayerMask;
    [SerializeField] float rayDistance;

    [Header("Player Config")]
    [SerializeField] PlayerConfig playerConfig;

    private Rigidbody playerRb;
    void Awake()
    {
        InitializeValues();
        playerRb = GetComponent<Rigidbody>();
        CanMove = true;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {   
        if(CanMove && !isDashing) {


            if (ctx.action.triggered)
            {
                movementVector = ctx.ReadValue<Vector3>();
                movementDir = movementVector.normalized;
                playerAnim.SetBool(isRunning, true);

            }
            else if (ctx.canceled)
            {

                movementVector = Vector3.zero;
            }

            if (movementVector != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(movementDir);
            }
            else
            {
                playerAnim.SetBool(isRunning, false);
            }


        }

        
    }

    public void OnDash(InputAction.CallbackContext ctx)
    {
        if(ctx.action.triggered && !isDashing)
        {
            playerAnim.SetBool(isDashingStr, true);
            dashEffect.SetActive(true);
            Vector3 dashDirection = transform.TransformDirection(Vector3.forward);
            dashTarget = transform.position + dashDirection * dashSpeed;

            RaycastHit hit;

            if(Physics.Raycast(transform.position, dashDirection, out hit, rayDistance, obstacleLayerMask))
            {
                playerAnim.SetBool(isDashingStr, false);
                dashEffect.SetActive(false);
                return;
               
            }

            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;

        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < dashDuration)
        {
            float t = elapsedTime / dashDuration;
            playerAnim.SetBool(isDashingStr, true);
            transform.position = Vector3.Lerp(startPosition, dashTarget, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        transform.position = dashTarget;
        playerAnim.SetBool(isDashingStr, false);
        dashEffect.SetActive(false);
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }

    public void StopMovement()
    {
        playerAnim.SetBool(isRunning, false);
        movementVector = Vector3.zero;

    }

    private void FixedUpdate()
    {
        smoothedMovementInput = Vector3.SmoothDamp(smoothedMovementInput, movementVector, ref movementInputSpeedVelocity, 0.1f);
        playerRb.velocity = smoothedMovementInput * movSpeed;

    }

    public void ApplyMovementSpeedPenalty()
    {
        movSpeed *= movSpeedPen;
    }

    public void RemoveMovementSpeedPenalty()
    {
        movSpeed = defaultMovSpeed;
    }

    void InitializeValues()
    {
        movSpeed = playerConfig.movementSpeed;
        movSpeedPen = playerConfig.movementSpeedPen;
        dashSpeed = playerConfig.dashSpeed;
        dashDuration = playerConfig.dashDuration;
        dashCooldown = playerConfig.dashCooldown;
        defaultMovSpeed = playerConfig.movementSpeed;
    }
}
