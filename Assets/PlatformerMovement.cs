
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlatformerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public float moveSpeed;
    public float jumpHeight;
    

    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;
    private bool canDash = true;
    private float dashTimer;

    private Rigidbody2D rb2d;

    private float _movement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            Coroutine dashCoroutine = StartCoroutine(Dash());
        }

        rb2d.linearVelocityX = _movement;
        if (rb2d.linearVelocityX > 0)
        {
            animator.SetInteger("walkdirction", +1);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (rb2d.linearVelocityX < 0)
        {

            animator.SetInteger("walkdirction", -1);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            animator.SetInteger("walkdirction", 0);

        }

        if (rb2d.linearVelocityY != 0)
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
            rb2d.linearVelocityY = jumpHeight;



        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0;
        rb2d.linearVelocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        yield return new WaitForSeconds(dashTime);
        rb2d.gravityScale = originalGravity;

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
//linkfor dash vidieohttps://www.youtube.com/watch?v=yB6ty0Gj7tA