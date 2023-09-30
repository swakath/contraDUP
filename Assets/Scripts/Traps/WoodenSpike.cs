using UnityEngine;

public class WoodenSpike : MonoBehaviour
{
    [Header ("Damage Parameters")]
    private Health playerHealth;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    private Animator anim;

    [Header ("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(PlayerInSight() && playerHealth.currentHealth > 0){
            //Attack
            anim.SetTrigger("Cycle");
        }
    }

    private bool PlayerInSight() {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0 , playerLayer);

        if(hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();        //grab the player's health Component, as the only thing enemy collides is with player

        return hit.collider != null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range  * transform.localScale.x , boxCollider.bounds.size);

        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer(){
        //if player still in range, damage him
        if(PlayerInSight()){
            //Damage player health.
            playerHealth.TakeDamage(damage); 
        }
    }
}
