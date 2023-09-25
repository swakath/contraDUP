using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("Spikehead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;

    // [Header ("SFX")]
    // [SerializeField] private AudioClip impactSound;

    //called every time the spikehead gets activated
    private void OnEnable()
    {
        Stop();             //makes sure object starts from idle posi. n doesnt goes crazy by attking straightaway
    }       
    private void Update()
    {
        //Move spikehead to destn. only if it is attking
        if(attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;   //if spikehead not attking
            if(checkTimer > checkDelay)
                CheckForPlayer();
        }
    }   

    private void CheckForPlayer() 
    {
        //chk if player in range
        CalculateDirections();

        //chk all 4 dir.
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if(hit.collider != null && !attacking)          //spikehead detects player in-range & not attacking 
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range;        //right dir. range
        directions[1] = -transform.right * range;       //left dir. range
        directions[2] = transform.up * range;           //up dir. range
        directions[3] = -transform.up * range;          //down dir. range
    }

    private void Stop()
    {
        destination = transform.position;           //set destn. as current position, inorder to stop it
        attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // SoundManger.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        
        Stop();         //stop spikehead once it hits player
    }
}
