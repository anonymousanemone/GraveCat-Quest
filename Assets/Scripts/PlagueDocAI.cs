using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueDoctorAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float raycastDistance = 3f;

    [Header("Attack Settings")]
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;

    private Transform player; 
    private Animator animator;
    private Pathfinder pathfinder;

    private List<Vector3> path;
    private int currentPathIndex = 0; 
    public bool playerDead; 
    public GameObject gameOverUI;
    

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        animator = GetComponent<Animator>();

        pathfinder = FindObjectOfType<Pathfinder>();

        gameOverUI.SetActive(false);
        playerDead = false;

    }

    private void Update()
    {
        if (player == null || playerDead)
            return;

        Vector3 direction = (player.position - transform.position).normalized;

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            AttackPlayer();
            nextAttackTime = Time.time + attackCooldown;
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (pathfinder == null || player == null) return;

        List<Pathfinder.Node> pathNodes = pathfinder.FindPath(transform.position, player.position);

        if (pathNodes != null && pathNodes.Count > 0)
        {
            path = new List<Vector3>();
            foreach (Pathfinder.Node node in pathNodes)
            {
                path.Add(node.worldPosition);
            }

            if (path.Count > 0 && currentPathIndex < path.Count)
            {
                Vector3 nextWaypoint = path[currentPathIndex];
                transform.position = Vector2.MoveTowards(transform.position, nextWaypoint, moveSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, nextWaypoint) < 0.01f)
                {
                    currentPathIndex++;
                }
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, raycastDistance, ~0, -Mathf.Infinity, Mathf.Infinity);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            else
            {
            }
        }

        if (currentPathIndex >= path.Count)
        {
            currentPathIndex = 0;
        }
    }


    //death sequence
    private void AttackPlayer()
    {
        animator.SetTrigger("attack");

        playerDead = true;

        gameOverUI.SetActive(true);
    }
}
