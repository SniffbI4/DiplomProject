using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Animator))]
public class UIManager : MonoBehaviour
{
    private int score;

    [SerializeField] Text ammoInCase;
    [SerializeField] Text allAmmo;
    [SerializeField] Text scoreText;
    [SerializeField] Text weaponName;

    private Animator animator;

    private void Start()
    {
        score = 0;
        animator = GetComponent<Animator>();

        Weapon.WeaponInfoChanged = ShowWeaponInfo;
        Debug.Log("UIManagerStart");
    }

    private void ShowWeaponInfo (Weapon w)
    {
        ammoInCase.text = w.currentAmmoInClip.ToString();
        allAmmo.text = w.currentAmmo.ToString();
        weaponName.text = w.weaponName;
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

    public void ChangeName (string name)
    {
        this.weaponName.text = name;
    }
}
