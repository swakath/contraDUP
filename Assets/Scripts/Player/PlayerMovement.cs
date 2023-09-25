using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components attached to player
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float x_input;

    [Header ("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>(); 
    }

    private void Update()
    {
        // L/R movement
        x_input = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(x_input * speed, body.velocity.y);
        
        // flips player when moving left or right
        if(x_input > 0.01f)                         //right             ( One -> (1,1,1) )
            transform.localScale = Vector3.one;         
        else if(x_input < -0.01f)                   //left
            transform.localScale = new Vector3(-1, 1, 1);

        // Set anim parameters 
        anim.SetBool("run", x_input != 0);      // player idle  -> horizontal i/p = 0           BASE-CASE [f    Player not running]
        anim.SetBool("grounded", IsGrounded());


        if(Input.GetKey(KeyCode.Space) && IsGrounded()){
            Jump();

            if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
                SoundManger.instance.PlaySound(jumpSound); 
        }
    }

    private void Jump() 
    {
        if(IsGrounded()){
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
    }

    private bool IsGrounded() 
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttk()
    {
        return x_input == 0 && IsGrounded();       //player not moving L/R, on ground
    }
}

