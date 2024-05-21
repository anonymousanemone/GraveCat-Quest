using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower = 16f;

    private bool grounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Vector2 input;
    private bool isFacingRight = true;
    private bool dead;
    //public GameObject gameOverUI;

    public int health = 3;  
    public float invincibilityDuration = 1.5f; 
    private bool isInvincible = false; 
    private float invincibilityTimer = 1f;

    public HealthBar healthBar;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dead = false;
        //gameOverUI.SetActive(false);
        healthBar.SetMaxHealth(health);
    }

    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Move player
        input.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input.x * speed, rb.velocity.y);

        Jump();
        Flip();

        anim.SetBool("isMoving", input.x != 0);
        anim.SetBool("grounded", grounded);
        if (dead)
        {
            //TODO
            anim.SetBool("dead", true);
            rb.velocity = new Vector2(0,0);
            // MODIFIED BY SOPHIA
            SceneControl.instance.LoseGame();
            //gameOverUI.SetActive(true); 
        }

        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        }
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
        if (isFacingRight && input.x < 0f || !isFacingRight && input.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            TakeDamage(1);  // Take 1 damage
            anim.SetTrigger("hurt");
        }
    }

    public void TakeDamage(int damage)
    {
        if (!dead && !isInvincible)
        {
            health -= damage;
            Debug.Log("Player Health: " + health);

            if (health <= 0)
            {
                dead = true;
                anim.SetBool("dead", true);
                // MODIFIED BY SOPHIA
                SceneControl.instance.LoseGame();
                //gameOverUI.SetActive(true);  
            }

            isInvincible = true;
            invincibilityTimer = invincibilityDuration;

            healthBar.SetHealth(health);
        }
    }

    public void TriggerHurtAnimation()
    {
        anim.SetTrigger("hurt");
    }
}
