using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Health), typeof(ActorView))]
public class Mutant : MonoBehaviour
{
    [SerializeField] float EnemyLevel;
    [SerializeField] float defaultWalkSpeed;
    [SerializeField] float defaultRunSpeed;

    [SerializeField] private float distanceToAttackPlayer;
    [SerializeField] private float distanceToRun;

    [SerializeField] private float delayBetweenAttack;
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask attackedLayer;

    private NavMeshAgent navMeshAgent;
    private Health health;
    private ActorView actorView;
    private Collider collider;

    private float savedTime;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        actorView = GetComponent<ActorView>();
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        navMeshAgent.speed = defaultWalkSpeed;
        actorView.PlayEnemyWalkByLevel(EnemyLevel);
    }

    void Update()
    {
        if (!health.IsAlive)
            return;

        Vector3 target = EnemySpawner.instance.GetPlayerPosition().position;
        navMeshAgent.destination = target;
        transform.LookAt(target, Vector3.up);

        if (Vector3.Distance(target, transform.position) < distanceToRun)
        {
            navMeshAgent.speed = defaultRunSpeed;
            actorView.PlayEnemyRunByLevel(EnemyLevel);

            if (Vector3.Distance(target, transform.position) < distanceToAttackPlayer && savedTime <= Time.time)
            {
                navMeshAgent.isStopped = true;
                actorView.PlayEnemyAttackByLevel(EnemyLevel);
                savedTime = (Time.time + delayBetweenAttack);
            }
        }
    }

    public void TakeDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, attackedLayer);
        if (colliders.Length != 0)
            colliders[0].GetComponent<Health>().ApplyDamage(damage);
    }

    public void EnemyCanGo ()
    {
        navMeshAgent.isStopped = false;
    }


    public void Refresh()
    {
        navMeshAgent.speed = defaultWalkSpeed;
        actorView.PlayEnemyWalkByLevel(EnemyLevel);

        navMeshAgent.isStopped = false;
        health.Refresh();
        collider.enabled = true;
    }

}
