using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;            //to determine dir. of shooting fireball
    private BoxCollider2D boxCollider;
    private Animator anim;
    private float lifetime;

    private void Awake()
    {
        //Grab reference
        anim = GetComponent<Animator>   ();
        boxCollider = GetComponent<BoxCollider2D>(); 
    }

    private void Update()
    {
        if(hit)
            return;
        
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed,0,0);                 //move obj. by movement speed on x axis 

        lifetime += Time.deltaTime;
        if(lifetime > 3)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if fireball hit any other object 
        hit = true;     
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        // if(collision.tag == "Enemy")
        //     collision.GetComponent<Health>().TakeDamage(1);         //reduce enemy's health by 1, if player's shot connects to enemy
    }


    //to shoot fireball L/R  & to reset state of the fireball
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;     
        boxCollider.enabled = true;

        //for fireball dir.
        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;                 //flip dir. of fireball_x if oppo. to player
        
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate() 
    {
        gameObject.SetActive(false); 
    }
}
