using UnityEngine;

public class Enemy_sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private bool movingLeft;
    private float leftBound;                //limiting saw's movement
    private float rightBound;

    private void Awake()
    {
        leftBound = transform.position.x - movementDistance;
        rightBound = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if(movingLeft)
        {
            if(transform.position.x > leftBound)            //saw hasnt reached the LEFTBOUND
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = false;                         //reached leftBound, now move saw to right
        }   
        else                        //saw moving right
        {
            if(transform.position.x < rightBound)            //saw hasnt reached the RIGHTBOUND
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = true;                         //reached rightBound, now move saw to left
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
