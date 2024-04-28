using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleDamage : MonoBehaviour
{
    public int damage = 1; 
    public float lifetime = 5f;
    private Animator anim;

    private void Start()
    {
        // Start a coroutine to destroy the tentacle after the specified lifetime
        StartCoroutine(DestroyAfterLifetime());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<player_controller>().TakeDamage(damage);
            other.gameObject.SendMessage("TriggerHurtAnimation", SendMessageOptions.DontRequireReceiver);
            // Destroy(gameObject);
        }
    }

    public void OnAnimationFinish()
    {
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterLifetime()
    {
        // Wait for the specified lifetime duration
        yield return new WaitForSeconds(lifetime);

        // Destroy the tentacle instance after the lifetime duration
        Destroy(gameObject);
    }
}
