using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewBehaviourScript : MonoBehaviour
{
    /*
        Main entry scene controller. I dentify the user input using buttons and loads the corresponding 
        level and starts the game. Also projects the game instruction for 10s before the start of the actual game.
    */
    private string curButtonName;
    private string BLUE_BUTTON = "Button_Blue";
    private string RED_BUTTON = "Button_Red";
    private string GREEN_BUTTON = "Button_Green";
    private GameObject EntryMenu;
    private GameObject InstructionMenu;
    private GameObject SoundManager;
    private string startLevel;
    private void Awake()
    {
        EntryMenu = GameObject.Find("Game_Entry");
        InstructionMenu = GameObject.Find("Game_Instructions");
        SoundManager = GameObject.FindWithTag("SoundManager");
        startLevel = "Level1";
    }

    private void Start()
    {
        EntryMenu.SetActive(true);
        InstructionMenu.SetActive(false);
        
    }

    private int selectedIndex;
    // Start is called before the first frame update

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f); // Delay for 10 seconds
        SceneManager.LoadScene(startLevel);
        Debug.Log("Starting Game");
    }

    public void PlayGame()
    {
        curButtonName =  UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        if(curButtonName == BLUE_BUTTON)
        {
            //Debug.Log("Blue Button pressed");
            selectedIndex = 0;
            startLevel = "Level1";
        }
        else if(curButtonName == RED_BUTTON)
        {
            //Debug.Log("Red Button pressed");
            selectedIndex = 0;
            startLevel = "Level2";
        }
        else if (curButtonName == GREEN_BUTTON)
        {
            //Debug.Log("Red Button pressed");
            selectedIndex = 0;
            startLevel = "Level3";
        }
        GameManager.Instance.gameObjectIndex = selectedIndex;

        EntryMenu.SetActive(false);
        InstructionMenu.SetActive(true);
        SoundManager.GetComponent<mainMenuSound>().PlayWaitAudio();
        StartCoroutine(StartGame());

    }
}
