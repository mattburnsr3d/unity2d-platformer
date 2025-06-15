using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Creates a reference to the rigidbody for the player character
    private Rigidbody2D rb2d;

    // This annotation allows to change the value directly from unity
    [SerializeField]
    private float speed;

    // Awake is called every time the script is loaded
    private void Awake()
    {
        // Check the player object for a Rigidbody2D component
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    // Update is called once per frame
    private void Update()
    {
        // linearVelocity = Controls the velocity of the player's movement
        // Vector2 = Controls the position, it's directly linked to the "Transform" option.
        //       x = Up/Down
        //       y = Left/Right
        //       z = Backwards/Fowards (not present in 2D)
        rb2d.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb2d.linearVelocityY);

        if (Input.GetKey(KeyCode.Space))
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocityX, speed);    // Maintain the X axis speed, change the value on the Y axis
    }
}
