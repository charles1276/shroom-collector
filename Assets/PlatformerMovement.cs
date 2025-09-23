
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
    public float dashDistance = 15f;
    bool isDashing;
    float doubleTapTime;
    KeyCode lastKeycode;
    public Rigidbody2D rb2d;

    private float _movement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //dash left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (doubleTapTime> Time.time && lastKeycode == KeyCode.A)
            {
                StartCoroutine(Dash(-1f));
            }
            else
            {
                doubleTapTime = Time.time + 0.2f;
            }
            lastKeycode = KeyCode.A;
            //dash right
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (doubleTapTime > Time.time && lastKeycode == KeyCode.D)
                {
                    StartCoroutine(Dash(1f));     
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }
                lastKeycode = KeyCode.D;
        }
        rb2d.linearVelocityX = _movement;
        if (rb2d.linearVelocityX > 0)
            {
                animator.SetBool("isWalking", true);
            }
        else if (rb2d.linearVelocityX < 0)
            {

                animator.SetBool("isWalkingLeft", true);
            }
        else
            {
                animator.SetBool("isWalking", false);

            }

        if (rb2d.linearVelocityY != 0)
            {
                animator.SetBool("isJumping", true);
            }
        else
            {
                animator.SetBool("isJumping", false);
            }
        } }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb2d.linearVelocity = new Vector2(_movement * moveSpeed, rb2d.linearVelocity.y);
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
    IEnumerator Dash(float direction)
    {
        isDashing = true;
       
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 0f);
      
        rb2d.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity= rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
        rb2d.gravityScale = gravity;
        rb2d.gravityScale = 5f;
    }

}
//linkfor dash vidieohttps://www.youtube.com/watch?v=yB6ty0Gj7tA