using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components attached to player
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float x_input;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>(); 
    }

    private void Update()
    {
        x_input = Input.GetAxis("Horizontal");
        
        //flips player when moving left or right
        if(x_input > 0.01f)                         //right
            transform.localScale = Vector3.one;
        else if(x_input < -0.01f)                   //left
            transform.localScale = new Vector3(-1, 1, 1);

        //Set anim parameters 
        //anim.SetBool("run", x_input != 0);      // player idle  -> horizontal i/p = 0           BASE-CASE [f    Player not running]
    }
}

