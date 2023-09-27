using UnityEngine;

public class ERunner : MonoBehaviour
{
    private EnemyPatrol enemyPatrol;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    
}
