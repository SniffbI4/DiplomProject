using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestDelegateToScore : MonoBehaviour
{
    public static Action<TestDelegateToScore> ScoreChanged;
    
    [SerializeField] int score;
    public int Score => score;

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

    private void OnEnable()
    {
        health.OnEnemyDead += Health_OnEnemyDead;
    }

    private void OnDisable()
    {
        health.OnEnemyDead -= Health_OnEnemyDead;
    }

    private void Health_OnEnemyDead()
    {
        animator.PlayDeathAnimation();
        agent.isStopped = true;
        collider.enabled = false;
        ResourseSpawner.instance.SpawnResourse(transform.position);

        ScoreChanged?.Invoke(this);
    }
}
