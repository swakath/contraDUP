using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameMovePauseMenu : MonoBehaviour
{
    
    private string curButtonName;
    private Vector3 continuePos = new Vector3(-2.66f, -0.75f, 90);
    private Vector3 exitPos = new Vector3(-2.66f, -1.75f, 90);
    private string prevButton;
    private string CNT_BUTTON = "Button_Continue";
    private string EXIT_BUTTON = "Button_Exit";

    private AudioSource myAudio;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = continuePos;
        prevButton = CNT_BUTTON;
        myAudio = GetComponent<AudioSource>();
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
            if(curButtonName == CNT_BUTTON)
            {
                transform.position = continuePos;
            }
            else if(curButtonName == EXIT_BUTTON)
            {
                transform.position = exitPos;
            }
        }
    }
}
