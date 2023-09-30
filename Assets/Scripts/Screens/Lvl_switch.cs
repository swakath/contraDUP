using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl_switch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            GameManager.Instance.UpdatePlayerScore();

            /*
            if(currentSceneName == "Level1")
                SceneManager.LoadScene("Level2");

            else if (currentSceneName == "Level2")
                SceneManager.LoadScene("Level3");
            
            else if (currentSceneName == "Level3")
                SceneManager.LoadScene("ScoreCard");
            */

            if (currentSceneName == "Level1")
                SceneManager.LoadScene("Level2");

            else if (currentSceneName == "Level2")
                SceneManager.LoadScene("Level3");

            else if (currentSceneName == "Level3")
                SceneManager.LoadScene("ScoreCard");
        }
    }
}
