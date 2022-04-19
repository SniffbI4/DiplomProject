using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthUI : MonoBehaviour
{
    [SerializeField]
    private Health health;

    private void Awake()
    {
        if (health == null)
        {
            health = transform.parent.gameObject.GetComponent<Health>();
        }
    }

    private void OnEnable()
    {
        health.OnHealthChanged += Health_OnHealthChanged;
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= Health_OnHealthChanged;
    }

    private void Health_OnHealthChanged(float defuse, bool isDamaged)
    {
        ShowUI(defuse);
    }

    public abstract void ShowUI(float health);
}
