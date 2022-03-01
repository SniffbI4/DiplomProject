using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Animator))]
public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [SerializeField] Text ammoInCase;
    [SerializeField] Text allAmmo;

    private Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        Destroy(this.gameObject);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowAmmo (int ammoInCase, int allAmmo)
    {
        this.ammoInCase.text = ammoInCase.ToString();
        this.allAmmo.text = allAmmo.ToString();
    }

    public void ReloadAnim (float time)
    {
        animator.speed = 1 / time;
        animator.Play("ReloadAnimation");
    }

    public void StopAnimation ()
    {
        animator.Play("New State");
    }
}
