using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleDamage : MonoBehaviour
{
    public int damage = 1; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<player_controller>().TakeDamage(damage);
            // Destroy(gameObject);
        }
    }

    public void OnAnimationFinish()
    {
        Destroy(gameObject);
    }
}
