using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public float moveSpeed;
    public float jumpHeight;
    public float dashSpeed;

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
         if (rb2d.linearVelocityX != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (rb2d.linearVelocityY != 0)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>().x * moveSpeed;
        
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>()== 1)
        {
            rb2d.linearVelocityY = jumpHeight;
        }
        
    }
    public void move(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>().x * dashSpeed;
    }
    
}
