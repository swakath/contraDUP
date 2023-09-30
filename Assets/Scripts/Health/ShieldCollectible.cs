using UnityEngine;

public class ShieldCollectible : MonoBehaviour
{
    private int shieldFlashCount;      //time for which player remains unvulnerable
    [SerializeField] private AudioClip pickupSound;


    private void Awake()
    {
        shieldFlashCount = 30;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Use CompareTag for efficiency
        {
            SoundManger.instance.PlaySound(pickupSound);

            Health playerHealth = collision.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.noOfFlashes = shieldFlashCount;
                playerHealth.iFramesDuration = 10f;
                playerHealth.startSheild();
            }

            gameObject.SetActive(false); // Deactivate collectible once picked up
        }
    }
}
