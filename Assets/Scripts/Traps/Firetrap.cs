using UnityEngine;
using System.Collections;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float  damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;         //time after which fire starts once player hovers above it
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    [Header ("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    private bool triggered;         //when the trap gets triggered 
    private bool active;            //trap active and can hurt player

    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(playerHealth != null && active)              //take dmg only if health not null and trap active
        {
            playerHealth.TakeDamage(damage);
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();
            if(!triggered)
                //trigger the fire trap
                StartCoroutine(ActivateFiretrap());
            
            if(active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            playerHealth = null;                //if player exits the firetrap area, he shouldn't be hurt
    }
    
    private IEnumerator ActivateFiretrap() 
    {
        //turn the sprite red to notify the player and trigger the trap
        triggered = true;
        spriteRend.color = Color.red;           //turn the sprite red to indicate player firetrap is about to get activated

        //wait for delay, activate trap, turn on animation, return the color back to normal
        yield return new WaitForSeconds(activationDelay);
        SoundManger.instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white;           //turn the sprite back to white [Initial color] FIRE started
        active = true;
        anim.SetBool("activated", true);

        //wait for X sec, deactivate trap and reset all var. and reset animation to idle    [animator]
        yield return new WaitForSeconds(activeTime);
        active = false;         //deactivate firetrap
        triggered = false;
        anim.SetBool("activated", false);
    }
}
