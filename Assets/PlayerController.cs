using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;

    bool isDashing= false;
    bool touchingGround;
    [SerializeField] int speed = 0;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Hi");
            touchingGround = true;
        } //else {
            //touchingGround = false;
        //}
    }

    void OnCollisionExit2D (Collision2D collision){
        if (!(collision.gameObject.CompareTag("Ground")))
        {
            touchingGround = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = movementVector;
        if (!isDashing){
            rb.velocity = new Vector2(speed * movementVector.x, rb.velocity.y);
        }
    }

    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();

        Debug.Log(movementVector);
    }

    void OnJump(InputValue value)
    {
        if (touchingGround){
        rb.AddForce(new Vector2(0, 400));
        touchingGround = false;
        }
        
    }

    void OnDash(InputValue value)
    {
        if (!isDashing && movementVector.x !=0){
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash(){
        isDashing = true;
        rb.velocity = new Vector2(movementVector.x * dashSpeed, rb.velocity.y);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }
}
