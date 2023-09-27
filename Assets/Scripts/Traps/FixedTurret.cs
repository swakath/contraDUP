using UnityEngine;

public class FixedTurret : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bullets;
    private float cooldownTimer;

    [Header ("SFX")]
    [SerializeField] private AudioClip bulletSound;

    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    private void Attack() 
    {
        cooldownTimer = 0;

        SoundManger.instance.PlaySound(bulletSound);
        bullets[FindAmmo()].transform.position = firePoint.position; 
        bullets[FindAmmo()].GetComponent<EnemyProjectile>().ActivateProjectile();

    }

    //Pool ammo
    private int FindAmmo()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if(!bullets[i].activeInHierarchy)
                return i;
        }
        return 0;       //just reuse 1st bullet if all busy  
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime; 
        if(cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0)
            Attack();
    }
}
