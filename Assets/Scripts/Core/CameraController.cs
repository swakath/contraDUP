//Moves the camera according to players movement
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // player camera
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;
    private float currentPosX;

    private void Awake()
    {
        player = null;
    }

    // Camera subscribes to GameManage event to get dynamically spaned Player object in the Game.
    private void OnEnable()
    {
        GameManager.OnObjectInstantiated += HandleObjectInstantiated;
    }

    private void OnDisable()
    {
        GameManager.OnObjectInstantiated -= HandleObjectInstantiated;
    }

    /* 
        Deligate Function for the subscribed GameManger Event
        Get the player tranform component as sets it to the local variable
    */
    private void HandleObjectInstantiated(GameObject instantiatedObject)
    {
        if (instantiatedObject.tag == "Player")
        {
            // Debug.Log("[Camera] : I Received the player ");
            player = instantiatedObject.GetComponent<Transform>();
        }
    }

    // Update function to enable camera to follow the player.
    private void Update()
    {
        //Player Camera
        if (player != null)
        {
            transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
            lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
        }
    }

    public void MoveToNewScreen(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }


}
