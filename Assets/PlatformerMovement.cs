
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
    

}
//linkfor dash vidieohttps://www.youtube.com/watch?v=yB6ty0Gj7tA