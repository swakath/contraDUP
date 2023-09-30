using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    public GameObject[] gameObjects;


    public delegate void ObjectInstantiated(GameObject instantiatedObject);
    public static event ObjectInstantiated OnObjectInstantiated;

    private int _gameObjectIndex;
    private float _playerHealth;
    private double _playerKillScored;
    private double _playerScore;
    private string currentScene;

    GameObject camera;
    GameObject SoundManager;

    private Transform lastCameraTransform;

    public int gameObjectIndex 
    {  
        get { return _gameObjectIndex; }
        set { _gameObjectIndex = value; }
    }

    public float PlayerHealth
    {
        get { return _playerHealth; }
        set { _playerHealth = value; }
    }

    public double PlayerKillScored
    {
        get { return _playerKillScored;}
    }

    public double PlayerScore
    {
        get { return _playerScore; }
    }

    public void IncrementPlayerKillScored(float killScore)
    {
        if (killScore > 0)
        {
            _playerKillScored += killScore;
        }
    }

    public void PlayerDeadSequence()
    {
        _playerScore = _playerKillScored + (Time.time * 10);
        Debug.Log(_playerScore);
        SceneManager.LoadScene("ScoreCard");
    }
    /*
    public void ActivateObjectInScene(string sceneName)
    {
        // Find the scene by name
        Scene targetScene = SceneManager.GetSceneByName(sceneName);

        if (targetScene.IsValid())
        {
            // Use SceneManager.GetActiveScene().GetRootGameObjects() to find objects in the scene
            GameObject[] rootObjects = targetScene.GetRootGameObjects();

            foreach (GameObject obj in rootObjects)
            {
                if(obj.name !="Player")
                    obj.SetActive(true); // Activate the object
            }
        }
        else
        {
            Debug.LogWarning("Scene '" + sceneName + "' not found.");
        }
    }

    public void DeactivateObjectInScene(string sceneName)
    {
        // Find the scene by name
        Scene targetScene = SceneManager.GetSceneByName(sceneName);

        if (targetScene.IsValid())
        {
            // Use SceneManager.GetActiveScene().GetRootGameObjects() to find objects in the scene
            GameObject[] rootObjects = targetScene.GetRootGameObjects();

            foreach (GameObject obj in rootObjects)
            {
                if (obj.name != "Player")
                    obj.SetActive(false); // Activate the object
            }
        }
        else
        {
            Debug.LogWarning("Scene '" + sceneName + "' not found.");
        }
    } */

    public void StartPauseSequence()
    {
        Time.timeScale = 0f; // Pause the game
        _playerScore = _playerKillScored + (Time.time * 10);

        camera = GameObject.FindWithTag("MainCamera");
        SoundManager = GameObject.FindWithTag("SoundManager");
        
        lastCameraTransform = camera.transform;

        Scene pauseMenuScene = SceneManager.GetSceneByName("PauseMenu");

        if (!pauseMenuScene.isLoaded)
        {  
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive); // Load the pause menu scene additively
            camera.transform.position = new Vector3(0f, 0f, -10.0f);
            camera.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            camera.transform.localScale = new Vector3(1f, 1f, 1f);
            SoundManager.GetComponent<SoundManger>().PlayPauseAudio();
        }
    }

    public void StopPauseSequence()
    { 
        camera = GameObject.FindWithTag("MainCamera");
        SoundManager = GameObject.FindWithTag("SoundManager");

        UnityEngine.SceneManagement.Scene pauseMenuScene = SceneManager.GetSceneByName("PauseMenu");
        if (pauseMenuScene.isLoaded)
        {
            SceneManager.UnloadSceneAsync("PauseMenu"); // Unload the pause menu scene
            camera.transform.position = lastCameraTransform.position;
            camera.transform.rotation = lastCameraTransform.rotation;
            camera.transform.localScale = lastCameraTransform.localScale;
            SoundManager.GetComponent<SoundManger>().PlayLevelAudio();
        }
        Time.timeScale = 1f; // Resume the game
    }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            _playerHealth = 4f;
            _playerKillScored = 0f;
            _playerScore = 0f;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishLoading;
    }
    void OnLevelFinishLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
        {
            currentScene = "Level1";
            GameObject instantiatedObject = Instantiate(gameObjects[_gameObjectIndex]);
            OnObjectInstantiated.Invoke(instantiatedObject);
        }

        else if (scene.name == "Level2")
        {
            currentScene = "Level2";
            GameObject instantiatedObject = Instantiate(gameObjects[_gameObjectIndex]);
            OnObjectInstantiated.Invoke(instantiatedObject);
        }

        else if (scene.name == "Level3")
        {
            currentScene = "Level3";
            GameObject instantiatedObject = Instantiate(gameObjects[_gameObjectIndex]);
            OnObjectInstantiated.Invoke(instantiatedObject);
        }
    }
}
