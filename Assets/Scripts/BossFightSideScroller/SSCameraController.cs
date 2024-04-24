using UnityEngine;
using UnityEngine.Tilemaps;

//code from: https://www.youtube.com/watch?v=PA5DgZfRsAM&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=6&ab_channel=Pandemonium


public class SSCameraController : MonoBehaviour
{
    //Follow player
    [SerializeField] private Transform player;
    [SerializeField] private Tilemap grid;
    [SerializeField] private float aheadDistance;

    private Bounds tilemapBounds;
    private Vector2 cameraSize;

    private void Start()
    {
        tilemapBounds = grid.GetComponent<TilemapRenderer>().bounds;
        var height = Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;
        cameraSize = new Vector2(width, height);

    }
    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;

        viewPos.x = Mathf.Clamp(player.position.x, tilemapBounds.min.x+cameraSize.x, tilemapBounds.max.x-cameraSize.x);
        viewPos.y = transform.position.y;
        viewPos.z = transform.position.z;

        transform.position = viewPos;
    }
    
    
}