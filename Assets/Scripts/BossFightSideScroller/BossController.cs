using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    public GameObject tentaclePrefab;
    private Animator anim;
    private Animator playerAnim;

    public float maintainDistance = 7.0f;
    public float moveSpeed = 2.0f;
    public float breakTime = 3f; 
    public int damage = 1;
    public float startMovingDistance = 4.0f;
    public float attackRate = 3.0f;

    private int regularAttackCount = 0; 
    public int regularAttackThreshold = 6; 
    private bool canMove = false;

    private bool specialAttackCompleted = false;


    private void Start()
    {
        StartCoroutine(PerformAttack());
        anim = GetComponent<Animator>();
        playerAnim = player.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!canMove)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= startMovingDistance)
            {
                canMove = true;
            }
        }

        if (canMove)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = (player.position - transform.position).normalized;

        if (distance > maintainDistance + 1.0f)
        {
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
            anim.SetBool("walk", true);
        }
        else if (distance < maintainDistance - 1.0f)
        {
            transform.position -= (Vector3)(direction * moveSpeed * Time.deltaTime);
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
            // canMove = false; 
            StartCoroutine(TakeBreak()); 
        }
    }



    private IEnumerator TakeBreak()
    {
        // Boss takes a break after reaching the player
        anim.SetBool("walk", false);
        yield return new WaitForSeconds(breakTime);
    }

    private IEnumerator PerformAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackRate);
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        float groundY = -1.6f; 
        Vector3 attackPosition = new Vector3(player.position.x, groundY, player.position.z);
        GameObject tentacle = Instantiate(tentaclePrefab, attackPosition, Quaternion.identity);
        
        regularAttackCount++;
        
        if (regularAttackCount >= regularAttackThreshold)
        {
            StartCoroutine(SpecialAttack());
            regularAttackCount = 0; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent <player_controller>().TakeDamage(damage);
            playerAnim.SetTrigger("hurt");
        }
    }

    private IEnumerator ShakeBoss(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;


            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }

    private IEnumerator SpecialAttack()
    {
        float spacing = 1.4f; 
        float groundY = -1.6f;
        float startOffset = 2.5f;

        // anim.SetBool("walk", false);
        // anim.SetTrigger("attack1");
        StartCoroutine(ShakeBoss(0.5f, 0.1f));


        for (int i = 0; i < 18; i++)
        {
            Vector3 attackPosition = transform.position + Vector3.right * (i + startOffset) * spacing;
            attackPosition.y = groundY;
            GameObject tentacle = Instantiate(tentaclePrefab, attackPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.8f);
        }

        specialAttackCompleted = true;
        StartCoroutine(FinalAttack());
    }

    private IEnumerator FinalAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackRate);

            if (specialAttackCompleted)
            {
                anim.SetBool("walk", false);
                // anim.SetTrigger("attack1");
                StartCoroutine(ShakeBoss(0.5f, 0.1f));

                yield return new WaitForSeconds(1.0f);

                GameObject[] chandeliers = GameObject.FindGameObjectsWithTag("chandelier");
                foreach (GameObject chandelier in chandeliers)
                {
                    Rigidbody2D rb = chandelier.GetComponent<Rigidbody2D>();
                    rb.gravityScale = 1; // Enable gravity for chandelier

                    chandelier.SetActive(false);
                    chandelier.transform.position = new Vector3(chandelier.transform.position.x, 5.0f, chandelier.transform.position.z);
                }
                yield return new WaitForSeconds(3.0f);
                foreach (GameObject chandelier in chandeliers)
                {
                    chandelier.SetActive(true);
                }

                specialAttackCompleted = false;
            }
            else
            {
                AttackPlayer();
            }
        }
    }

}
