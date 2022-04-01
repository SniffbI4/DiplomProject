using UnityEngine;

[RequireComponent (typeof(Animator))]
public class ActorView : MonoBehaviour
{
    private Animator animator;  

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Weapon.WeaponReloadStart = PlayReloadStart;
        Weapon.WeaponReloadStop = PlayReloadStop;
    }

    private void Weapon_OnWeaponReloadStart(float time)
    {
        animator.SetTrigger(GameData.ANIMATION_RELOAD_TRIGGER);
    }

    public void PlayDeathAnimation ()
    {
        animator.SetTrigger(GameData.ANIMATION_DEATH_TRIGGER);
    }

    public void PlayAttackAnimation ()
    {
        animator.SetTrigger(GameData.ANIMATION_ATTACK_TRIGGER);
    }

    public void PlayWalk (float x, float y)
    {
        if (!animator.GetBool(GameData.ANIMATION_WALK_BOOL))
            animator.SetBool(GameData.ANIMATION_WALK_BOOL, true);
        animator.SetFloat(GameData.HORIZONTAL_AXIS, x);
        animator.SetFloat(GameData.VERTICAL_AXIS, y);
    }

    public void PlayEnemyWalkByLevel (float levelValue)
    {
        if (!animator.GetBool(GameData.ANIMATION_WALK_BOOL))
        {
            animator.SetBool(GameData.ANIMATION_WALK_BOOL, true);
            animator.SetBool(GameData.ANIMATION_RUN_BOOL, false);
        }
        animator.SetFloat(GameData.ANIM_WALK_ENEMY_LEVEL, levelValue);
    }

    public void PlayEnemyRunByLevel (float levelValue)
    {
        if (!animator.GetBool(GameData.ANIM_RUN_ENEMY_LEVEL))
        {
            animator.SetBool(GameData.ANIMATION_WALK_BOOL, false);
            animator.SetBool(GameData.ANIMATION_RUN_BOOL, true);
        }
        animator.SetFloat(GameData.ANIM_RUN_ENEMY_LEVEL, levelValue);
    }

    public void PlayEnemyAttackByLevel (float levelValue)
    {
        animator.SetTrigger(GameData.ANIMATION_ATTACK_TRIGGER);
        animator.SetFloat(GameData.ANIM_ATTACK_ENEMY_LEVEL, levelValue);
    }

    public void PlayEnemyDeathByLevel (float levelValue)
    {
        animator.SetTrigger(GameData.ANIMATION_DEATH_TRIGGER);
        animator.SetFloat(GameData.ANIM_DEATH_ENEMY_LEVEL, levelValue);
    }

    public void PlayRoll ()
    {
        animator.SetTrigger(GameData.ANIMATION_ROLL_TRIGGER);
    }

    public void PlayIDLE ()
    {
        animator.SetBool("WalkBool", false);
    }

    public void PlayReloadStart(Weapon w)
    {
        animator.SetTrigger(GameData.ANIMATION_RELOAD_TRIGGER);
    }

    public void PlayReloadStop (Weapon w)
    {
        Debug.Log("Stop reloading");
    }
}
