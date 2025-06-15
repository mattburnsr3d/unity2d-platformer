using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Creates a reference to the rigidbody for the player character
    private Rigidbody2D rb2d;

    // Creates a reference to the Animator for the player character
    private Animator animator;

    // This annotation allows to change the value directly from unity
    [SerializeField]
    private float speed;

    private bool grounded;

    // Awake is called every time the script is loaded
    private void Awake()
    {
        // Grab references from game object
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // linearVelocity = Controls the velocity of the player's movement
        // Vector2 = Controls the position, it's directly linked to the "Transform" option.
        //       x = Up/Down
        //       y = Left/Right
        //       z = Backwards/Fowards (not present in 2D)
        rb2d.linearVelocity = new Vector2(horizontalInput * speed, rb2d.linearVelocityY);

        // Flips character sprite based on the X axis value
        // Negative = Character is moving left
        // Positive = Character is moving right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector2.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector2(-1, 1);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        // Set animator parameters
        animator.SetBool("run", horizontalInput != 0);  // horizontalInput will be 0 if no inputs are being triggered (idle)
        animator.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        animator.SetTrigger("jump");
        grounded = false;
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocityX, speed);    // Maintain the X axis speed, change the value on the Y axis
    }

    // Detects collisions between 2D objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
