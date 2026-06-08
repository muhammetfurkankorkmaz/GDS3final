using CameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce = 8f;
    [Header("Essentials")]
    [SerializeField] GameObject visuals;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask platformLayer;

    private Rigidbody2D rb;
    float xMovement = 0;
    float constantGravity;


    public bool isOnAir { get; private set; } = false;
    bool isOnPlatform = false;
    bool canJump = false;
    bool isHangFalling = false;
    bool isHanging = false;

    bool isTurnedLeft = false;
    bool isTurning = false;

    //Timers
    float coyoteTimer;

    Collider2D characterCollider;
    Collider2D currentPlatform;
    Platform currentPlatformScript;

    [SerializeField] CHAnimation cHAnimationScript;

    [SerializeField] Transform headCheck; // small point at the top of the character
    [SerializeField] float headRayDistance = 0.2f; // distance to check platform above

    [SerializeField] AudioClip jumpSound;

    Coroutine changeToIdleCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<Collider2D>();
        constantGravity = rb.gravityScale;
        InputController.Instance.onJumpButtonPress += JumpButtonPress;
        //Time.timeScale = 0.2f;
    }

    void Update()
    {
        GroundandPlatformCheck();
        Timers();
        MovementController();
        if (isHangFalling)
        {
            HangFall();
        }
        RotationCheck();
        //if (isHanging && !isOnPlatform)
        //{
        //    // Re-enable gravity if player walks off platform
        //    rb.gravityScale = constantGravity;
        //    isHanging = false;
        //}
    }
    private void FixedUpdate()
    {
        GravityCheck();
    }

    void Timers()
    {
        if (isOnAir && canJump)
        {
            coyoteTimer += Time.deltaTime;
            if (coyoteTimer >= 0.12f)
            {
                canJump = false;
                coyoteTimer = 0;
            }
        }
    }
    void GravityCheck()
    {
        if (isHanging)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            return;
        }
        if (rb.velocity.y < -0.01f)//Falling
        {
            rb.gravityScale = Mathf.Min(rb.gravityScale + 0.015f, 15);
            cHAnimationScript.ChangeState("CharacterFall");
        }
        else if (rb.gravityScale != constantGravity)
        {
            CameraShaker.Presets.ShortShake2D(0.04f, 0.06f, 30, 3);
            cHAnimationScript.ChangeState("CharacterLand");
            if (changeToIdleCoroutine != null)
            {
                return;
            }
            changeToIdleCoroutine = StartCoroutine(ChangeToIdle());

            rb.gravityScale = constantGravity;
        }
    }

    void GroundandPlatformCheck()
    {
        //if (isHanging || isHangFalling) return;
        bool onGround = Physics2D.OverlapCircle(
       groundCheck.position,
       groundCheckRadius,
       groundLayer
   );

        // Check one-way platforms
        bool onPlatformCheck = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            platformLayer
        );

        // Combine logic
        isOnPlatform = onPlatformCheck;
        isOnAir = !(onGround || onPlatformCheck);
        if (!isOnAir)
        {
            canJump = true;
            coyoteTimer = 0;
        }
        if (onGround)
        {
            isHangFalling = false;

            if (currentPlatform != null)
            {
                Physics2D.IgnoreCollision(characterCollider, currentPlatform, false);
                if (currentPlatformScript != null)
                    currentPlatformScript.ChangeActivation(false);
            }
        }
    }

    void MovementController()
    {
        if (isHangFalling || isHanging) return;
        xMovement = InputController.Instance.XInput;

        if (xMovement == 0 && !isOnAir)
        {
            if (cHAnimationScript.currentState != "CharacterLand" && cHAnimationScript.currentState != "CharacterFall" && cHAnimationScript.currentState != "CharacterJump")
            {

                cHAnimationScript.ChangeState("CharacterIdle");
            }
        }
        // Apply movement
        if (xMovement != 0 && !isOnAir)
        {
            rb.velocity = new Vector2(xMovement * moveSpeed, rb.velocity.y);
            if (rb.velocity.y > -0.01f && rb.velocity.y < 0.01f)
            {

                cHAnimationScript.ChangeState("CharacterWalk");
            }
        }
        else
        {
            rb.velocity = new Vector2(xMovement * moveSpeed * 0.75f, rb.velocity.y);
        }
    }
    IEnumerator ChangeToIdle()
    {
        yield return new WaitForSeconds(0.15f);
        cHAnimationScript.ChangeState("CharacterIdle");
        changeToIdleCoroutine = null;
    }
    void RotationCheck()
    {
        if (visuals == null || isHangFalling || isHanging || isOnAir) return;

        if (InputController.Instance.XInput > 0 && !isTurnedLeft)
        {
            StartCoroutine(Turn(180));
        }
        else if (InputController.Instance.XInput < 0 && isTurnedLeft)
        {
            StartCoroutine(Turn(0));

        }
    }
    public void StartJumpAnimation()
    {

        cHAnimationScript.ChangeState("CharacterJump");
    }
    IEnumerator Turn(int _angle)
    {
        isTurning = true;
        float startAngle = visuals.transform.localEulerAngles.y;

        float elapsed = 0;
        float duration = 0.15f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float angle = Mathf.Lerp(startAngle, _angle, t);
            visuals.transform.eulerAngles = new Vector3(0, angle, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        isTurning = false;
        if (_angle == 0)
        {
            isTurnedLeft = false;
        }
        else
        {
            isTurnedLeft = true;
        }
    }
    void JumpButtonPress()
    {
        if (isHangFalling) return;
        //Should check the y input first is it going up or down
        if (canJump && InputController.Instance.YInput >= 0)
        {
            //Jumps
            SoundManager.Instance.PlaySoundEffect(jumpSound, 0.5f);

            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
            cHAnimationScript.ChangeState("CharacterJump");
        }
        else if (isOnPlatform)
        {
            //If on the platform it hangs
            if (currentPlatform != null)
            {
                Physics2D.IgnoreCollision(characterCollider, currentPlatform);
                isHangFalling = true;
                rb.velocity = new Vector2(0, rb.velocity.y);
                print("hang fall started");
            }

        }
        else if (isHanging)
        {
            isHanging = false;
            rb.gravityScale = constantGravity;
            currentPlatformScript.ChangeActivation(false);
            currentPlatformScript = null;
            currentPlatform = null;

        }
    }
    void HangFall()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

        RaycastHit2D hit = Physics2D.Raycast(headCheck.position, Vector2.up, headRayDistance, platformLayer);
        if (hit.collider != null)
        {
            // Stop vertical motion
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0;

            // Snap character just below platform
            Vector3 pos = transform.position;
            pos.y = hit.collider.bounds.min.y - (characterCollider.bounds.size.y / 2f);
            transform.position = pos;

            isHangFalling = false; // Finished hanging
            isHanging = true;
            if (currentPlatform != null)
            {
                Physics2D.IgnoreCollision(characterCollider, currentPlatform, false);
                currentPlatformScript.ChangeActivation(true);
                cHAnimationScript.ChangeState("CharacterHold");

            }
            else
            {
                print("CurrentPlatform is null");

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            GameObject platform = collision.gameObject;
            currentPlatform = platform.GetComponent<Collider2D>();
            currentPlatformScript = platform.GetComponent<Platform>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && !isHanging && !isHangFalling)
        {
            currentPlatform = null;
        }
    }
    void OnDrawGizmos()
    {
        if (groundCheck == null || headCheck == null) return;

        // Color based on ground/air state
        Gizmos.color = isOnAir ? Color.red : Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        // Draw head ray check for hang
        Gizmos.color = isHanging ? Color.green : Color.red;
        Gizmos.DrawWireSphere(headCheck.position, headRayDistance);

        // Draw potential hang point line
        //RaycastHit2D hit = Physics2D.Raycast(headCheck.position, Vector2.up, headRayDistance, platformLayer);
        //if (hit.collider != null)
        //{
        //    Gizmos.color = Color.cyan;
        //    Vector3 hangPos = hit.collider.bounds.min - new Vector3(0, characterCollider.bounds.size.y / 2f, 0);
        //    Gizmos.DrawLine(headCheck.position, hangPos);
        //    Gizmos.DrawSphere(hangPos, 0.05f); // optional small sphere at hang point
        //}


    }

}//Class
