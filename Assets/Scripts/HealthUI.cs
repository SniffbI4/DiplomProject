using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private Health health;

    private void Awake()
    {
        health = transform.parent.gameObject.GetComponent<Health>();
        health.OnHealthChanged += Health_OnHealthChanged;
    }

    private void Health_OnHealthChanged(float defuse, bool isDamaged)
    {
        ShowUI(defuse);
    }

    public virtual void ShowUI(float health)
    {
        //функционал в дочерних классах
    }
}
