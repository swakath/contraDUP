using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set; }
    private Animator anim;
    private bool dead;

    [Header ("iFrames")]
    [SerializeField] private float iFramesDuration;             // invulnerabilityDuration
    [SerializeField] private int noOfFlashes;                   // to display before a player turns back to normal state
    private SpriteRenderer spriteRend;                          // ref to sprite renderer to change the color of player during invulnerable duration 

    [Header ("Components")]
    [SerializeField] private Behaviour[] components;
    
    [Header ("Sounds")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    private bool invulnerable;

    private void Awake()
    {
        if (gameObject.CompareTag("Player"))
        {
            currentHealth = GameManager.Instance.PlayerHealth;
        }
        else
        {
            currentHealth = startingHealth;
        }
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage) 
    {
        if(invulnerable)
            return;             //no dmg to player, if vulnerable

            //currentHealth -= _damage;             //reducing players health

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0 , startingHealth);           //reduce n check if within bounds
        //Debug.Log(currentHealth);
        
        if(gameObject.CompareTag("Player"))
            GameManager.Instance.PlayerHealth = currentHealth;

        if (gameObject.CompareTag("Player") || gameObject.CompareTag("Enemy")){         
            if(currentHealth > 0)
            {
                //player hurt
                anim.SetTrigger("hurt");
                
                //get iframes
                StartCoroutine(Invulnerability());

                SoundManger.instance.PlaySound(hurtSound);
            } 
            else
            {
                if(!dead) {                         //player only dies once
                    if (gameObject.CompareTag("Enemy"))
                    {
                        float winPoint = 0f;
                        //winPoint = 10* gameObject.GetComponent<EnemyDamage>().getDamage();
                        winPoint = 20 * startingHealth;

                        GameManager.Instance.IncrementPlayerKillScored(winPoint);
                    }
                    if(gameObject.CompareTag("Player"))
                    {
                        GameManager.Instance.PlayerDeadSequence();
                    }
                       
                    //player dead
                    anim.SetTrigger("die");

                    // deactivates all attached classes
                    foreach (Behaviour component in components)
                        component.enabled = false;
                    
                    dead = true;
                    SoundManger.instance.PlaySound(deathSound);
                }
            }
        }
        else if(gameObject.CompareTag("EnemyMachine"))               //machine turrets
        {
            if(!dead && currentHealth == 0) {
                
                float winPoint = 0f;
                //winPoint = 10f * gameObject.GetComponent<Health>().getStartingHealth();
                winPoint = 20f * startingHealth;

                GameManager.Instance.IncrementPlayerKillScored(winPoint);

                anim.SetTrigger("destroyed");
                // deactivates all attached classes
                foreach (Behaviour component in components)
                    component.enabled = false;
                    
                dead = true;
                SoundManger.instance.PlaySound(deathSound);
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0 , startingHealth);   
    }

    private IEnumerator Invulnerability() 
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(9, 10, true);       //ignores player and enemy collision present at layer 9,10

        //invulnerability duration
        for (int i = 0; i < noOfFlashes; i++)
        {
            spriteRend.color = new Color(92 / 255f, 92 / 255f, 92 / 255f, 0.5f);      //RGB code for blue + abit transparent
            yield return new WaitForSeconds(iFramesDuration/(noOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration/(noOfFlashes * 2)); 
        }

        Physics2D.IgnoreLayerCollision(9, 10, false);       //invulerability duration ended, reset collision avoidance

        invulnerable = false;
    }

    private void Deactivate() 
    {
        gameObject.SetActive(false); 
    }
}
