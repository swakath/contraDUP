using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float jumpForce = 11f;
    
    private float movementX = 0f;
    private bool isGrounded = true;
    private string WALK_ANIMATION = "walk";

    private Rigidbody2D rigb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //PlayerJump();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        PlayerAnimation();
        PlayerJump();
    }
    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }
    
    void PlayerAnimation()
    {
        if(movementX > 0f)
        {
            animator.SetBool(WALK_ANIMATION, true);
            spriteRenderer.flipX = false;
        }
        else if (movementX < 0f)
        {
            animator.SetBool(WALK_ANIMATION, true);
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool (WALK_ANIMATION, false);
        }
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Pressed Space");
            isGrounded = false;
            rigb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
