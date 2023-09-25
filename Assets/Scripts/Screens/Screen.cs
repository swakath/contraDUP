using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector3[] initialPosition;

    private void Awake()
    {
        //save the initial positions of all enemies 
        initialPosition = new Vector3[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null)
                initialPosition[i] = enemies[i].transform.position;
        }
    }

    public void ActivateScreen(bool _status) 
    {
        //activate/deactivate enemies present in a room
        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i];
            }
        }
    }
}
