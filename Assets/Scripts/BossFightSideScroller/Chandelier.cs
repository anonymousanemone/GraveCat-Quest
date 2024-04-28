using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chandelier : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable gravity for the chandelier initially
    }

    public void EnableGravityAfterDelay(float delay)
    {
        StartCoroutine(EnableGravityCoroutine(delay));
    }

    private IEnumerator EnableGravityCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.gravityScale = 1; 
    }
}