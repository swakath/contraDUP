using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl_switch : MonoBehaviour
{
    /*
        Used to swith the levels for one scene to other. Called at the end of each level once the 
        level trigger is hit.
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            GameManager.Instance.UpdatePlayerScore();

            if (currentSceneName == "Level1")
                SceneManager.LoadScene("Level2");

            else if (currentSceneName == "Level2")
                SceneManager.LoadScene("Level3");

            else if (currentSceneName == "Level3")
                SceneManager.LoadScene("ScoreCard");
        }
    }
}
