using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] Transform leftEdge;
    [SerializeField] Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;

    //[Header("Enemy Animator")]
   // [SerializeField] private Animator anim;
    private bool movingLeft;
    private Vector3 initScale;

    [Header("Idle Behaviour Duration")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    private void Awake()
    {
        initScale = enemy.localScale;       //initialize with enemy's org. scale at begg. of room/lvl
    }

    private void OnDisable()
    {
       // anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)         //enemy hasn't reached the left edge yet
                MoveInDirection(-1);
            else
            {
                //change dir.
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)        //enemy hasn't reached the right edge yet
                MoveInDirection(1);
            else
            {
                //change dir.
                DirectionChange();
            }
        }
    }


    private void DirectionChange()
    {
        //anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;           //logical NOT   -   handles turning enemy around, after some idle duration at edges
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        //anim.SetBool("moving", true);
        //Make enemy face the correct dir. 1st
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        //Then, Move it towards that dir.
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);   //y n z axis remains the same, only modify for x axis
    }

}
