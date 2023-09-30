using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameMove : MonoBehaviour
{
    
    private string curButtonName;
    private Vector3 bluePos = new Vector3(-10.8f, 0.5f, 0f);
    private Vector3 redPos = new Vector3(-10.8f, -0.5f, 0f);
    private Vector3 greenPos = new Vector3(-10.8f, -1.5f, 0f);

    private string prevButton;
    private string BLUE_BUTTON = "Button_Blue";
    private string RED_BUTTON = "Button_Red";
    private string GREEN_BUTTON = "Button_Green";

    private AudioSource myAudio;
    // Start is called before the first frame update
    private void Awake()
    {
        transform.position = bluePos;
        prevButton = BLUE_BUTTON;
        myAudio = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject != null)
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
            else if(curButtonName == GREEN_BUTTON)
            {
                transform.position = greenPos;
            }
        }
    }
}
