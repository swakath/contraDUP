//Damages player once it makes contact
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage; 

    public float getDamage()
    {
        return damage;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            collision.GetComponent<Health>().TakeDamage(damage);
    }  

}
