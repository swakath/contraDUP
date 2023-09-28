using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Awake()
    {
        totalHealthBar.fillAmount = 0.4f;
        
        playerHealth = null;
    }

    

    private void OnEnable()
    {
        GameManager.OnObjectInstantiated += HandleObjectInstantiated;
    }

    private void OnDisable()
    {
        GameManager.OnObjectInstantiated -= HandleObjectInstantiated;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if(playerHealth != null)
            currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void HandleObjectInstantiated(GameObject instantiatedObject)
    {
        if (instantiatedObject.tag == "Player")
        {
            //Debug.Log("[Health Bar]: I Received the player");
            playerHealth = instantiatedObject.GetComponent<Health>();
        }
    }

}