using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float damage;

    [Header ("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] bullets;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    
    [Header ("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header ("Sound")]
    [SerializeField] private AudioClip bulletSound;

    private Animator anim;
    private EnemyPatrol enemyPatrol;
    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if(PlayerInSight()){
            if(cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0){
                //Attack
                cooldownTimer = 0;
                anim.SetTrigger("rangedAttack");
            }
        }    

        if(enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();             //enemy patrol enabled only when player not in sight.
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

    private void RangedAttack() {
        SoundManger.instance.PlaySound(bulletSound);
        cooldownTimer = 0;
        //Shoot projectile
        bullets[FindAmmo()].transform.position = firepoint.position;
        bullets[FindAmmo()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindAmmo(){
        for (int i = 0; i < bullets.Length; i++)
        {
            if(!bullets[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
