using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Animator))]
public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    private int score;

    [SerializeField] Text ammoInCase;
    [SerializeField] Text allAmmo;
    [SerializeField] Text scoreText;

    private Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        else
            Destroy(this.gameObject);
    }

    private void Weapon_OnWeaponActivated(int x, int y)
    {
        ShowAmmo(x, y);
    }

    private void Weapon_OnWeaponDeactivated()
    {
        animator.Play("New State");
    }

    private void Weapon_OnWeaponAmmoChanged(int x, int y)
    {
        ShowAmmo(x, y);
    }

    private void Weapon_OnWeaponReloadStart(float time)
    {
        animator.speed = 1 / time;
        animator.Play("ReloadAnimation");
    }

    private void Start()
    {
        score = 0;
        ShowScore();
        animator = GetComponent<Animator>();

        Weapon.OnWeaponReloadStart += Weapon_OnWeaponReloadStart;
        Weapon.OnWeaponAmmoChanged += Weapon_OnWeaponAmmoChanged;
        Weapon.OnWeaponDeactivated += Weapon_OnWeaponDeactivated;
        Weapon.OnWeaponActivated += Weapon_OnWeaponActivated;
    }

    public void ShowAmmo (int ammoInCase, int allAmmo)
    {
        this.ammoInCase.text = ammoInCase.ToString();
        this.allAmmo.text = allAmmo.ToString();
    }

    public void AddScore (int score)
    {
        this.score += score;
        ShowScore();
    }

    private void ShowScore ()
    {
        scoreText.text = this.score.ToString(); ;
    }
}
