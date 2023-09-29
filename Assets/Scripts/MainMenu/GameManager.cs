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
        _playerScore = _playerKillScored + (Time.time/10);
        SceneManager.LoadScene("ScoreCard");
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
            GameObject instantiatedObject = Instantiate(gameObjects[_gameObjectIndex]);
            OnObjectInstantiated.Invoke(instantiatedObject);
        }

        else if (scene.name == "Level3")
        {
            GameObject instantiatedObject = Instantiate(gameObjects[_gameObjectIndex]);
            OnObjectInstantiated.Invoke(instantiatedObject);
        }
    }
}
