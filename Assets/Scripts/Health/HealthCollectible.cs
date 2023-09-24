using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;      //amt. of health to be restored back to player's HP
    // [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") {
            // SoundManger.instance.PlaySound(pickupSound);
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);                    // deactivate collectible once picked up
        }
    }
}
