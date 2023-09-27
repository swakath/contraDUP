using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components attached to player
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private Transform firepoint;
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float x_input;

    private SpriteRenderer spriteRen;

    private bool isProne = false;
    public bool isUp {get; private set; }
    private Vector3 orgFirepointPosition; // Store the original position of firepoint

    [Header ("SFX")]
    [SerializeField] private AudioClip jumpSound;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>(); 
        spriteRen = GetComponent<SpriteRenderer>();
        isUp = false;
    }

    private void Start()
    {
        orgFirepointPosition = firepoint.localPosition;
    }

    

    private void Update()
    {
        Vector2 spriteSize = spriteRen.sprite.bounds.size;
        // Adjust the Box Collider's size to match the sprite's size
        boxCollider.size = spriteSize;

        // L/R movement
        x_input = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(x_input * speed, body.velocity.y);
        
        // flips player when moving left or right   && player not in prone mode
        if(!isProne)
        {
            if(x_input > 0.01f)                         //right             ( One -> (1,1,1) )
                transform.localScale = Vector3.one;         
            else if(x_input < -0.01f)                   //left
                transform.localScale = new Vector3(-1, 1, 1);
        }

        // Set anim parameters 
        anim.SetBool("run", x_input != 0);      // player idle  -> horizontal i/p = 0           BASE-CASE [f    Player not running]
        anim.SetBool("grounded", IsGrounded());


        if(Input.GetKey(KeyCode.Space) && IsGrounded()){
            Jump();

            if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
                SoundManger.instance.PlaySound(jumpSound); 
        }

        if((Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow)) && IsGrounded()){
            ToggleProne();
        }

        if((Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded()){
            ToggleUp();
        }

    }

    private void ToggleProne()
    {
        if(!isProne)
        {
            anim.SetBool("prone", true);
             // Adjust the firepoint's local position for prone state
            firepoint.localPosition = new Vector3(orgFirepointPosition.x + 0.22f, orgFirepointPosition.y - 0.45f, orgFirepointPosition.z);
            
            body.velocity = new Vector2(body.velocity.x, body.velocity.y - 3);
            isProne = true;
        }
        else{
            anim.SetBool("prone", false);
            isProne = false;
        }
    }


    private void ToggleUp()
    {
        if(!isUp)
        {
            anim.SetBool("lookup", true);
            // Adjust the firepoint's local position for Up state
            firepoint.localPosition = new Vector3(0.3f, 2f, orgFirepointPosition.z);
            isUp = true;
        }
        else{
            anim.SetBool("lookup", false);
            isUp = false;
        }
    }

    private void Jump() 
    {
        if(IsGrounded()){
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            firepoint.localPosition = orgFirepointPosition;
            anim.SetBool("prone", false);
            anim.SetBool("lookup", false);
            isProne = false;
            isUp = false;
            anim.SetTrigger("jump");
        }
    }

    private bool IsGrounded() 
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

}