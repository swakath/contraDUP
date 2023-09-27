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

    public int gameObjectIndex 
    {  
        get { return _gameObjectIndex; }
        set { _gameObjectIndex = value; }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
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
    }
}
