using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float speed = 5f; 
    private Vector2 movementDirection = Vector2.right;

    private bool isOriginalBird = true;

    public int scoreValue = 10;
    //[SerializeField] private LevelManager instance;

    private TDCameraController cam;

    void Update()
    {

        transform.Translate(movementDirection * speed * Time.deltaTime);

        if ((!isOriginalBird && !IsVisible()))// || FindObjectOfType<PlagueDoctorAI>().playerDead)
        {
            Destroy(gameObject);
        }
    }

    private bool IsVisible()
    {
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        //Bounds screenBounds = cam.getCameraBounds();
        return screenPosition.x >= -3 && screenPosition.x <= 3;
        //return transform.position.x >= screenBounds.min.x && transform.position.x <= screenBounds.max.x;
    }

    public void SetOriginalBird(bool isOriginal)
    {
        isOriginalBird = isOriginal;
    }

    public void SetDirection(Vector2 direction)
    {
        movementDirection = direction.normalized; 
    }

    public void FlipDirection()
    {
        speed *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelManager.instance.IncreaseScore(scoreValue);

            Destroy(gameObject);
        }
    }
}
