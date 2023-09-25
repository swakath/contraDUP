using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameMove : MonoBehaviour
{
    
    private string curButtonName;
    private Vector3 bluePos = new Vector3(-6.51f, -0.5f, 90);
    private Vector3 redPos = new Vector3(-6.51f, -1.5f, 90);
    private string prevButton;
    private string BLUE_BUTTON = "Button_Blue";
    private string RED_BUTTON = "Button_Red";

    private AudioSource myAudio;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = bluePos;
        prevButton = BLUE_BUTTON;
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        curButtonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        //Debug.Log(curButtonName);
        if(curButtonName != prevButton)
        {
            myAudio.Play();
            prevButton = curButtonName;
            if(curButtonName == BLUE_BUTTON)
            {
                transform.position = bluePos;
            }
            else if(curButtonName == RED_BUTTON)
            {
                transform.position = redPos;
            }
        }
    }
}
