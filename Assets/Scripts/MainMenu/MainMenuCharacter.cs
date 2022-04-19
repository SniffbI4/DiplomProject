using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class MainMenuCharacter : MonoBehaviour
{
    private Animator animator;

    private bool isAiming;
    private float animatorMaskWeight;
    private float animationDeltaTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animatorMaskWeight = 0;
    }

    public void ChangeAimState ()
    {
        isAiming = !isAiming;
        animatorMaskWeight = isAiming ? 1 : 0;
        animationDeltaTime = isAiming ? Time.deltaTime*10 : Time.deltaTime*3;
    }

    private void Update()
    {
        float currentWeight = animator.GetLayerWeight(1);

        if (currentWeight == animatorMaskWeight)
            return;

        animator.SetLayerWeight(1, Mathf.Lerp(currentWeight, animatorMaskWeight, animationDeltaTime));
    }
}
