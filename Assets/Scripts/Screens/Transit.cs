using UnityEngine;

public class Transit : MonoBehaviour
{
    [SerializeField] private Transform prevScreen;
    [SerializeField] private Transform nextScreen;
    [SerializeField] private CameraController cam;

    private bool transitionOccurred = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !transitionOccurred)
        {

            if(collision.transform.position.x < transform.position.x)           //player coming from Left
            {
                cam.MoveToNewScreen(nextScreen);
                nextScreen.GetComponent<Screen>().ActivateScreen(true);
                prevScreen.GetComponent<Screen>().ActivateScreen(false);
            }
            else    
            {
                cam.MoveToNewScreen(prevScreen);                                    //player coming from Right
                prevScreen.GetComponent<Screen>().ActivateScreen(true);
                nextScreen.GetComponent<Screen>().ActivateScreen(false);
            }

            transitionOccurred = true;          // Set the flag to true to prevent further transitions
        }
    }
}
