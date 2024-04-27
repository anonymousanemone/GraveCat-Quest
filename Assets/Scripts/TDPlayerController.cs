using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float speed;
    private Vector2 input;
    private bool isFacingRight = true;
    private bool dead;
    private bool climbing;
    [SerializeField] private float standDistance;
    [SerializeField] private LayerMask doorMask;

    //movement is from : https://www.youtube.com/watch?v=DBGvx-cCUMw&list=PLy1Xj-4F5G_cytIH8by-bZ9TVj5qKMlZn&index=2&ab_channel=EPICDEV-GameDevelopment
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        //murdered by plague doctor

        if (FindObjectOfType<PlagueDoctorAI>() && FindObjectOfType<PlagueDoctorAI>().playerDead)
        {
            anim.SetBool("dead", true);
            rb.velocity = new Vector2(0,0);
            //Debug.Log("dead");
            return;
        }

        //move
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(input.x * speed, input.y * speed);
        //walking animation
        if (input != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        //change direcitons
        Flip();
        Climb();

        
    }

    private void Flip()
    {
        //change facing direction
        if (isFacingRight && input.x < 0f || !isFacingRight && input.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Climb()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, standDistance, doorMask);
        //Debug.Log(input.y);
        //Debug.Log(hitInfo.collider);
        if (hitInfo.collider != null)
        {
            climbing = true;
        }
        else
        {
            climbing = false;
        }
        anim.SetBool("climb", climbing);
    }

    //when collide with plague doc, die
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlagueCrow"))
        {
            dead = true;
            anim.SetBool("dead", true);
        }
        
    }
    //for bird eating
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bird"))
        {
            anim.SetTrigger("swat");
        }
    }
}
