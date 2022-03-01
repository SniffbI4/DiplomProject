using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHeatlh;
    [SerializeField] private HealthUI healthUI;
    public bool IsAlive { get; private set; }
    private int currentHealth;

    [SerializeField] private UnityEvent OnDead;
        
    private void Start()
    {
        currentHealth = maxHeatlh;
        IsAlive = true;
        healthUI.ShowUI(1f);    
    }

    public void ApplyDamage (int damage)
    {
        currentHealth -= damage;
        if (currentHealth<=0)
        {
            IsAlive = false;
            Dead();
        }
        float x = (float)currentHealth / (float)maxHeatlh;
        healthUI.ShowUI(x);
    }

    public void RecoveryHealth (int health)
    {
        currentHealth += health;
        if (currentHealth > maxHeatlh)
            currentHealth = maxHeatlh;

        float x = (float)currentHealth / (float)maxHeatlh;
        healthUI.ShowUI(x);
    }

    private void Dead ()
    {
        ResourseSpawner.instance.SpawnResourse(transform.position);
        OnDead.Invoke();
    }
}
