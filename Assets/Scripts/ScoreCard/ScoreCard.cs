using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCard : MonoBehaviour
{
    private double totalScore;
    private String outString;
    public Text myText;
    // Start is called before the first frame update
    void Start()
    {
        totalScore = Math.Truncate(GameManager.Instance.PlayerScore);
        outString = totalScore.ToString();
        int txtLen = outString.Length;

        for(int i = 0; i < 10-txtLen; i++)
        {
            outString = "0" + outString;
        }
        myText.text = outString;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Bye Bye I am exiting");

            #if UNITY_EDITOR
                //Debug.Log("This is editor exit");    
                UnityEditor.EditorApplication.isPlaying = false;
            #endif

            Application.Quit();
        }
    }
}
