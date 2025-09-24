using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Input = UnityEngine.Input;

public class PlatformerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TrailRenderer tr;
    public float moveSpeed;
    public float jumpHeight;

    private float dashSpeed = 56f;
    private float dashTime = 0.4f;
    private float dashCooldown = 1f;
    private bool canDash = true;
    private bool isDashing;

    private Rigidbody2D rb2d;
    private float _movement;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (tr != null)
            tr.emitting = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        // Only allow normal movement if not dashing
        if (!isDashing)
        {
            rb2d.linearVelocity = new Vector2(_movement, rb2d.linearVelocity.y);
        }

        if (rb2d.linearVelocity.x > 0)
        {
            animator.SetInteger("walkdirction", +1);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (rb2d.linearVelocity.x < 0)
        {
            animator.SetInteger("walkdirction", -1);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            animator.SetInteger("walkdirction", 0);
        }

        if (rb2d.linearVelocity.y != 0)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>().x * moveSpeed;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 1)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpHeight);
        }
    }

    private IEnumerator Dash()  
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0;

        if (tr != null)
            tr.emitting = true;

        float dashDir = _movement != 0 ? Mathf.Sign(_movement) : 1f;
        rb2d.linearVelocity = new Vector2(dashDir * dashSpeed, 0);

        yield return new WaitForSeconds(dashTime);

        rb2d.gravityScale = originalGravity;
        isDashing = false;

        if (tr != null)
            tr.emitting = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;


    }
}
//linkfor dash vidieohttps://www.youtube.com/watch?v=yB6ty0Gj7tA