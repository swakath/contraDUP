using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPause : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            // "P" key is being held down
            Debug.Log("P key is pressed");
            GameManager.Instance.StartPauseSequence();
        }
    }
}
