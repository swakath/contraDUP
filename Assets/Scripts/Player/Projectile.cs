using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;            //to determine dir. of shooting fireball
    private BoxCollider2D boxCollider;
    private Animator anim;
    private float lifetime;

    private bool isHorizontal;

    private void Awake()
    {
        //Grab reference
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>(); 
        isHorizontal = true;
    }

    private void Update()
    {
        if(hit)
            return;
        
        float movementSpeed = speed * Time.deltaTime * direction;
        if(isHorizontal)
            transform.Translate(movementSpeed,0,0);                 //move obj. by movement speed on x axis 
        else
            transform.Translate(0,movementSpeed,0);
        
        lifetime += Time.deltaTime;
        if(lifetime > 3)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null) {
            //if fireball hit any other object 
            hit = true;     
            boxCollider.enabled = false;
            
            if(collision.tag == "Enemy" || collision.tag == "EnemyMachine")
                collision.GetComponent<Health>().TakeDamage(1);         //reduce enemy's health by 1, if player's shot connects to enemy

            anim.SetTrigger("explode");

            // if(collision.tag == "Ground")
            // {
            //     //shoot through the collider   
            // }
            
        }

        anim.SetTrigger("explode");
    }


    //to shoot fireball L/R  & to reset state of the fireball
    public void SetDirection(float _direction, bool isHorizontal)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;     
        boxCollider.enabled = true;
        this.isHorizontal = isHorizontal;
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
