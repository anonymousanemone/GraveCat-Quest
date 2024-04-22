using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    public GameObject tentaclePrefab;

    public float maintainDistance = 5.0f;
    public float moveSpeed = 2.0f;
    public int damage = 1;
    public float attackRate = 3.0f;

    private void Start()
    {
        StartCoroutine(PerformAttack());
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
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
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
        // Instantiate the tentacle attack at the player's position
        Vector3 attackPosition = new Vector3(player.position.x, player.position.y - 0.5f, player.position.z);
        Instantiate(tentaclePrefab, attackPosition, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Directly calling the TakeDamage method on the player
            collision.gameObject.GetComponent<player_controller>().TakeDamage(damage);
        }
    }
}
