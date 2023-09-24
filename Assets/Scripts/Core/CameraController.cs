//Moves the camera according to players movement
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;


    //player camera
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update()
    {
        //Player Camera
        transform.position = new Vector3(player.position.x + lookAhead,transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);  

    }

    public void MoveToNewScreen(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
