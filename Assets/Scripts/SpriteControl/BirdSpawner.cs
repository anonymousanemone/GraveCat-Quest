using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab; 
    public float spawnInterval = 2f;
    public float spawnDistance = 10f;
    public float birdLifetime = 5f; // Time in seconds before a bird despawns

    private float nextSpawnTime = 0f; 

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            Vector3 spawnPosition = CalculateSpawnPosition();

            GameObject newBird = Instantiate(birdPrefab, spawnPosition, Quaternion.identity);

            BirdMovement birdMovement = newBird.GetComponent<BirdMovement>();
            if (birdMovement != null)
            {
                if (spawnPosition.x < transform.position.x) 
                {
                    birdMovement.SetDirection(Vector2.right);
                }
                else 
                {
                    SpriteRenderer spriteRenderer = newBird.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.flipX = true;
                    }
                    birdMovement.SetDirection(Vector2.left); 
                }
            }

            StartCoroutine(DestroyBirdAfterTime(newBird)); 

            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        float spawnX = transform.position.x + Mathf.Sign(Random.Range(-1f, 1f)) * spawnDistance;
        float spawnY = Random.Range(Camera.main.ScreenToWorldPoint(Vector2.zero).y, Camera.main.ScreenToWorldPoint(Vector2.up * Screen.height).y);
        return new Vector3(spawnX, spawnY, 0f);
    }

    IEnumerator DestroyBirdAfterTime(GameObject bird)
    {
        yield return new WaitForSeconds(birdLifetime);
        Destroy(bird);
    }
}
