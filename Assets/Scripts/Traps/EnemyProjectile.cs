using UnityEngine;

public class EnemyProjectile : EnemyDamage      //will damage the player everytime it touches 
{
    private float speed;
    [SerializeField] private float resetTime;       //to deactivate obj. after certain amt. of time
    private float lifetime;

    private GameObject player;
    private Vector3 playerPosition;

    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;
    private void Awake()
    {
        speed = 8f;
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectile()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerPosition = (player.transform.position - transform.position).normalized;
        }
        else
        {
            Debug.LogError("Player not found to fix the position");
        }

        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);    
        coll.enabled = true;
    }

    private void Update()
    {
        if(hit)         //stop projectile from moving further if it hits something.
            return;

        //float movementSpeed = speed * Time.deltaTime;
        //transform.Translate(movementSpeed, 0, 0);

        GetComponent<Rigidbody2D>().velocity = playerPosition*speed;

        lifetime += Time.deltaTime;
        if(lifetime> resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.tag == "Player" || collision.tag == "Player_bullet"){
            hit = true;
            base.OnTriggerEnter2D(collision);       //execute logic from parent script 1st
            coll.enabled = false;
        
            if(anim != null)
                anim.SetTrigger("explode");         //when obj. is a fireball, explode it
            else
                gameObject.SetActive(false);        //deactivate if it touches any object
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
