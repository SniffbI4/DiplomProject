using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHeatlh;
    [SerializeField] private HealthUI healthUI;
    
    public bool IsAlive { get; private set; }
    public int MaxHealth => this.maxHeatlh;
    private int currentHealth;

    [SerializeField] private UnityEvent OnDead;

    public delegate void HealthChanged(float defuse, bool isDamaged=false);
    public event HealthChanged OnHealthChanged;

    public delegate void EnemyDead();
    public event EnemyDead OnEnemyDead;
        
    private void Start()
    {
        Refresh();
    }

    public void Refresh ()
    {
        if (healthUI!=null && !healthUI.gameObject.activeSelf)
            healthUI.gameObject.SetActive(true);
        currentHealth = maxHeatlh;
        IsAlive = true;

        OnHealthChanged?.Invoke((float)currentHealth / (float)maxHeatlh);
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
            OnHealthChanged?.Invoke((float)currentHealth / (float)maxHeatlh, true);
        }
    }

    public void RecoveryHealth (int health)
    {
        currentHealth += health;
        if (currentHealth > maxHeatlh)
            currentHealth = maxHeatlh;

        OnHealthChanged?.Invoke((float)currentHealth / (float)maxHeatlh);
    }

    private void Dead ()
    {
        if (healthUI!=null)
            healthUI.gameObject.SetActive(false);
        OnDead.Invoke();

        if (OnEnemyDead!=null)
            OnEnemyDead();
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
