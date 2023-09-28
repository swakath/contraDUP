using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attkCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private List<GameObject> fireballs = new List<GameObject>();

    [SerializeField] private AudioClip fireballSound;
    private GameObject FireBallHolder;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        //Grab references
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        FireBallHolder = GameObject.Find("FireballHolder");
        for (int i = 0; i < FireBallHolder.transform.childCount; i++)
        {
            GameObject child = FireBallHolder.transform.GetChild(i).gameObject;
            fireballs.Add(child);
            //Do something with child
        }

        // Debug.LogFormat("cooldown: {0}, attack: {1}",cooldownTimer,attkCooldown);
    }

    private void Start()
    {
        //Debug.Log("Start is called");
    }
    private void Update()
    {
        //Debug.Log(cooldownTimer + attkCooldown);
        if(Input.GetMouseButton(0) && cooldownTimer > attkCooldown)
        {
            //Debug.Log("I got a mouse input");
            Attack();
        }

        
        cooldownTimer += Time.deltaTime;
        // Debug.LogFormat("cooldown: {0}, attack: {1}", cooldownTimer, attkCooldown);
    }

    private void Attack() 
    {
        SoundManger.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        //pool fireballs
        fireballs[FindFireball()].transform.position = firePoint.position;   //everytime takes a fireball, and sets its initial position to firepoint
        if(!GetComponent<PlayerMovement>().isUp)
            fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x), true);     //get the dir. to fire {player facing} 
        else
            fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.y), false);     //get the dir. to fire {player facing}
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Count; i++)
        {
            if(!fireballs[i].activeInHierarchy)
            return i;
        }
        return 0;
    }
}
