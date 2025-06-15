using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Creates a references to game object's components
    private Rigidbody2D rb2d;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private float horizontalInput;

    private float wallJumpCooldown;

    // This annotation allows to change the value directly from unity
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpPower;

    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private LayerMask wallLayerMask;

    // Awake is called every time the script is loaded
    private void Awake()
    {
        // Grab references from game object
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Flips character sprite based on the X axis value
        // Negative = Character is moving left
        // Positive = Character is moving right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector2.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector2(-1, 1);

        // Set animator parameters
        animator.SetBool("run", horizontalInput != 0);  // horizontalInput will be 0 if no inputs are being triggered (idle)
        animator.SetBool("grounded", IsGrounded());

        if (wallJumpCooldown > 0.2f)
        {
            // linearVelocity = Controls the velocity of the player's movement
            // Vector2 = Controls the position, it's directly linked to the "Transform" option.
            //       x = Up/Down
            //       y = Left/Right
            //       z = Backwards/Fowards (not present in 2D)
            rb2d.linearVelocity = new Vector2(horizontalInput * speed, rb2d.linearVelocityY);

            if (OnWall() && !IsGrounded())
            {
                rb2d.gravityScale = 0;
                rb2d.linearVelocity = Vector2.zero;
            }
            else
                rb2d.gravityScale = 2f;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            animator.SetTrigger("jump");
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocityX, jumpPower);             // Maintain the X axis speed, change the value on the Y axis
        }
        else if (OnWall() && !IsGrounded())
        {
            if (horizontalInput == 0)
            {
                rb2d.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0f);
            }
            else
                rb2d.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6f);  // Pushes player against the wall for wall jumping mechanics

            wallJumpCooldown = 0f;
        }
    }

    // Detects collisions between 2D objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private bool IsGrounded()
    {
        // Casts a ray from a point of origin,
        // if the line intersects with an object with a collider then returns "true".
        return !Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            Vector2.down,
            0.1f,
            groundLayerMask
        ).collider.IsUnityNull();
    }

    private bool OnWall() =>
        !Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            new(transform.localScale.x, 0),
            0.1f,
            wallLayerMask
        ).collider.IsUnityNull();
}
