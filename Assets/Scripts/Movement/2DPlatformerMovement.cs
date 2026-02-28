using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    // The following section comes from a Unity discussion about making a simple jump script.
    public Vector2 jump;
    public bool isGrounded;
    // The portion ends here.
    Rigidbody2D rb;
    public float maxSpeed = 0.01f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // The portion begins again here
        jump = new Vector2(0.0f, 3.0f);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    // The portion ends here.

    void Update()
    {
        float moveTime = maxSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + new Vector3(moveTime, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + new Vector3(-moveTime, 0f, 0f);
        }
        // The portion begins again here
        if (Input.GetKey(KeyCode.W) && isGrounded || Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(jump, ForceMode2D.Impulse);
            isGrounded = false;
            // The portion ends here.
        }
    }
    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
}
