using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestDelegateToScore : MonoBehaviour
{
    [SerializeField] int score;

    private Health health;
    private ActorView animator;
    private NavMeshAgent agent;
    private Collider collider;

    private void Awake()
    {
        health = GetComponent<Health>();
        animator = GetComponent<ActorView>();
        agent = GetComponent<NavMeshAgent>();
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        health.OnEnemyDead += Health_OnEnemyDead;
    }

    private void Health_OnEnemyDead()
    {
        animator.PlayDeathAnimation();
        agent.isStopped = true;
        collider.enabled = false;
        ResourseSpawner.instance.SpawnResourse(transform.position);
        UIManager.instance.AddScore(score);
    }
}
