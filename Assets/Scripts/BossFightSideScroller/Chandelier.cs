using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CURRENTLY NOT USED
public class Chandelier : MonoBehaviour
{
    private Rigidbody2D rb;

    // public GameObject winScreen;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; 
    }

    public void TriggerFall(float delay)
    {
        StartCoroutine(FallRoutine(delay));
    }

    private IEnumerator FallRoutine(float delay)
    {
        yield return StartCoroutine(Shake(0.5f, 0.1f));
        yield return new WaitForSeconds(delay);
        rb.gravityScale = 1;  // Enable gravity to make it fall
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-magnitude, magnitude) * transform.localScale.x;
            float offsetY = Random.Range(-magnitude, magnitude) * transform.localScale.y;

            transform.position = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Boss"))
    //     {
    //         // Logic to trigger the win screen
    //         winScreen.SetActive(true);
    //     }
    // }
}
