using UnityEngine;

public class EnemyProjectile : EnemyDamage      //will damage the player everytime it touches 
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;       //to deactivate obj. after certain amt. of time
    private float lifetime;

    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);    
        coll.enabled = true;
    }

    private void Update()
    {
        if(hit)         //stop projectile from moving further if it hits something.
            return;

        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime> resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        hit = true;
        base.OnTriggerEnter2D(collision);       //execute logic from parent script 1st
        coll.enabled = false;
        
        if(anim != null)
            anim.SetTrigger("explode");         //when obj. is a fireball, explode it
        else
            gameObject.SetActive(false);        //deactivate if it touches any object
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
