using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public float moveSpeed;
    public float jumpHeight = 200.0f;
    public float speed = 3.0f;
    private float dashSpeed = 24f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;
    private bool canDash = true;
    private bool isDashing = false;
    private bool grounded = true;
    private bool canDoubleJump = true;
    private Rigidbody2D rb2d;
    private float _movement;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 move = Input.GetAxis("Horizontal") * transform.right * speed * Time.deltaTime;
        transform.Translate(move);
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

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

        animator.SetBool("isJumping", rb2d.linearVelocity.y != 0);
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>().x * moveSpeed;
    }

    void Jump()
    {
        if (grounded)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpHeight);
            grounded = false;
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpHeight);
            canDoubleJump = false;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0;

        float dashDir = _movement != 0 ? Mathf.Sign(_movement) : 1f;
        rb2d.linearVelocity = new Vector2(dashDir * dashSpeed, 0);

        yield return new WaitForSeconds(dashTime);

        rb2d.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;


    }

    // Add this method to detect landing on the ground
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        
        grounded = true;
        canDoubleJump = true;  
        
    }
}