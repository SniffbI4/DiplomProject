using UnityEngine;
using UnityEngine.AI;


[RequireComponent (typeof(NavMeshAgent), typeof(Health), typeof(ActorView))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float distanceToAttackPlayer;
    [SerializeField] private float delayBetweenAttack;
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask attackedLayer;

    private NavMeshAgent navMeshAgent;
    private Health health;
    private ActorView actorView;

    private float savedTime;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        actorView = GetComponent<ActorView>();
    }

    void Update()
    {
        if (health.IsAlive)
        {
            Vector3 target = EnemySpawner.instance.GetPlayerPosition().position;
            navMeshAgent.destination = target;
            transform.LookAt(target, Vector3.up);

            if (savedTime <= Time.time)
            {
                if (Vector3.Distance(EnemySpawner.instance.GetPlayerPosition().position, gameObject.transform.position) <= distanceToAttackPlayer)
                {
                    actorView.PlayAttackAnimation();
                    savedTime += Time.time + delayBetweenAttack;
                }
            }
        }
    }

    public void TakeDamage ()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, attackedLayer);
        if (colliders.Length != 0)
            colliders[0].GetComponent<Health>().ApplyDamage(damage);
    }

    public void Refresh ()
    {
        navMeshAgent.isStopped = false;
        health.Refresh();
    }
}
