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

    public delegate void HealthChanged(float defuse);
    public event HealthChanged OnHealthChanged;

    public delegate void EnemyDead();
    public event EnemyDead OnEnemyDead;
        
    private void Start()
    {
        Refresh();
    }

    public void Refresh ()
    {
        if (!healthUI.gameObject.activeSelf)
            healthUI.gameObject.SetActive(true);
        currentHealth = maxHeatlh;
        IsAlive = true;

        if (OnHealthChanged != null)
            OnHealthChanged((float)currentHealth / (float)maxHeatlh);
    }

    public void ApplyDamage (int damage)
    {
        if (IsAlive)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                IsAlive = false;
                Dead();
            }
            if (OnHealthChanged != null)
                OnHealthChanged((float)currentHealth / (float)maxHeatlh);
        }
    }

    public void RecoveryHealth (int health)
    {
        currentHealth += health;
        if (currentHealth > maxHeatlh)
            currentHealth = maxHeatlh;

        if (OnHealthChanged != null)
            OnHealthChanged((float)currentHealth / (float)maxHeatlh);
    }

    private void Dead ()
    {
        healthUI.gameObject.SetActive(false);
        OnDead.Invoke();

        if (OnEnemyDead!=null)
            OnEnemyDead();
    }
}
