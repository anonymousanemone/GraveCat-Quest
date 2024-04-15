using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    //jump tutorial: https://www.youtube.com/watch?v=K1xZ-rycYY8&ab_channel=bendux
    //ladder tutorial: https://www.youtube.com/watch?v=Ln7nv-Y2tf4&t=45s&ab_channel=Blackthornprod

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower = 16f;

    private bool grounded;
    [SerializeField]private Transform groundCheck;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private float distance;

    private float horizontalInput;
    private float verticalInput;
    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        //move player
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        Jump();
        Flip();

        //set animator parameters
        if (horizontalInput == 0) {
            anim.SetFloat("sit-time", anim.GetFloat("sit-time") + 0.01f);
        }
        else
        {
            anim.SetFloat("sit-time", 0);
        }
        anim.SetBool("walk", horizontalInput != 0);
        anim.SetBool("grounded", grounded);


    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetTrigger("jump");
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

}
