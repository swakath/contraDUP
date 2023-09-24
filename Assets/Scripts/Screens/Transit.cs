using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform prevScreen;
    [SerializeField] private Transform nextScreen;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.transform.position.x < transform.position.x)           //player coming from Left
            {
                cam.MoveToNewScreen(nextScreen);
                // nextScreen.GetComponent<Door>().ActivateRoom(true);
                // prevScreen.GetComponent<Door>().ActivateRoom(false);
            }
            else    
            {
                cam.MoveToNewScreen(prevScreen);                                    //player coming from Right
                // prevScreen.GetComponent<Room>().ActivateRoom(true);
                // nextScreen.GetComponent<Room>().ActivateRoom(false);
            }
        }
    }
}
