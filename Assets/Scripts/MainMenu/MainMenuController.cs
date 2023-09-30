using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    private string curButtonName;
    private string BLUE_BUTTON = "Button_Blue";
    private string RED_BUTTON = "Button_Red";

    private int selectedIndex;
    // Start is called before the first frame update
    public void PlayGame()
    {
        curButtonName =  UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        if(curButtonName == BLUE_BUTTON)
        {
            //Debug.Log("Blue Button pressed");
            selectedIndex = 0;
        }
        else if(curButtonName == RED_BUTTON)
        {
            //Debug.Log("Red Button pressed");
            selectedIndex = 0;
        }
        GameManager.Instance.gameObjectIndex = selectedIndex;

        SceneManager.LoadScene("Level2");
    }
}
