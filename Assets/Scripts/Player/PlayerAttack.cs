using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attkCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    // [SerializeField] private AudioClip fireballSound;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        //Grab references
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attkCooldown && playerMovement.CanAttk())
            Attack();
        
        cooldownTimer += Time.deltaTime;
    }

    private void Attack() 
    {
        // SoundManger.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        //pool fireballs
        fireballs[FindFireball()].transform.position = firePoint.position;   //everytime takes a fireball, and sets its initial position to firepoint
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));     //get the dir. to fire {player facing} 

    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if(!fireballs[i].activeInHierarchy)
            return i;
        }
        return 0;
    }
}
