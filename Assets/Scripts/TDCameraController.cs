using UnityEngine;

//code from: https://www.youtube.com/watch?v=PA5DgZfRsAM&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=6&ab_channel=Pandemonium

public class TDCameraController : MonoBehaviour
{
    //Room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    private Camera mainCamera;
    private Bounds cameraBounds;

    //Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Awake() => mainCamera = Camera.main;

    private void Start()
    {
        var height = mainCamera.orthographicSize;
        var width = height * mainCamera.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        cameraBounds = new Bounds();
        cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0f),
            new Vector3(maxX, maxY, 0.0f)
            );
    }

    public Bounds getCameraBounds()
    {
        return cameraBounds;
    }

    private void Update()
    {
        //Room camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

        //Follow player
        transform.position = new Vector3(player.position.x + lookAhead, player.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    //public void MoveToNewRoom(Transform _newRoom)
    //{
    //    currentPosX = _newRoom.position.x;
    //}

}