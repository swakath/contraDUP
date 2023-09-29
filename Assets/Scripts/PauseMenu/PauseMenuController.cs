using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    private string curButtonName;
    private string CNT_BUTTON = "Button_Continue";
    private string EXIT_BUTTON = "Button_Exit";

    private int selectedIndex;
    // Start is called before the first frame update

    private double totalScore;
    private String outString;
    public Text myText;

    // Start is called before the first frame update
    private void Start()
    {
        totalScore = Math.Truncate(GameManager.Instance.PlayerScore);
        outString = totalScore.ToString();
        int txtLen = outString.Length;

        for (int i = 0; i < 10 - txtLen; i++)
        {
            outString = "0" + outString;
        }
        myText.text = outString;
    }


    public void PauseSequence()
    {
        curButtonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(curButtonName);
        if (curButtonName == CNT_BUTTON)
        {
            //Debug.Log("Blue Button pressed");
            //selectedIndex = 0;
            GameManager.Instance.StopPauseSequence();
        }
        else if (curButtonName == EXIT_BUTTON)
        {
            #if UNITY_EDITOR
                //Debug.Log("This is editor exit");    
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
    }

}
