using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    public GameObject tentaclePrefab;
    private Animator anim;

    public float maintainDistance = 7.0f;
    public float moveSpeed = 2.0f;
    public float breakTime = 1.0f; // Time in seconds for the boss to take a break after reaching the player
    public int damage = 1;
    public float attackRate = 3.0f;

    private void Start()
    {
        StartCoroutine(PerformAttack());
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = (player.position - transform.position).normalized;

        // Boss moves towards the player if the distance is greater than the maintainDistance
        if (distance > maintainDistance)
        {
            // Move towards the player
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
            anim.SetBool("walk", true);
        }
        else if (distance < maintainDistance)
        {
            // Move away from the player
            transform.position -= (Vector3)(direction * moveSpeed * Time.deltaTime);
            anim.SetBool("walk", true);
        }
        else
        {
            // If the player is at the maintainDistance, stop walking
            anim.SetBool("walk", false);
            StartCoroutine(TakeBreak());
        }
    }

    private IEnumerator TakeBreak()
    {
        // Boss takes a break after reaching the player
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
        // Instantiate the tentacle attack at the player's position, but at a fixed y-value that matches the ground level
        float groundY = -1.55f; // Set this to the ground level of your game
        Vector3 attackPosition = new Vector3(player.position.x, groundY, player.position.z);
        GameObject tentacle = Instantiate(tentaclePrefab, attackPosition, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<player_controller>().TakeDamage(damage);
        }
    }
}

