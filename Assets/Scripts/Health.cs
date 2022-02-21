using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHeatlh;
    public bool IsAlive { get; private set; }
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHeatlh;
        IsAlive = true;
    }

    public void ApplyDamage (int damage)
    {
        currentHealth -= damage;
        if (currentHealth<=0)
        {
            IsAlive = false;
            Dead();
        }
    }

    private void Dead ()
    {
        Destroy(gameObject);
    }
}
