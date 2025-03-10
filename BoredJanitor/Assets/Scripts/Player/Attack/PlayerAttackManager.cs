using System;
using UnityEngine;
using System.Collections;



public class PlayerAttackManager : MonoBehaviour
{
    public Animator animator;
    [SerializeField] PlayerAttacks attacks;
    [SerializeField] soundManager sound;
    public int weaponIndex = 0;
    int obtainedWeapons = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CollectWeapon(1);

    }

    

    public void endAttacking()
    {
        Debug.Log("ending attack");
        animator.SetInteger("attacktype", 0);
    }

    public String getWeapon()
    {
        if (weaponIndex == 0){
            return "Broom";
        }
        return "Shovel";
    }

    public void SwapWeapons()
    {
        weaponIndex = (weaponIndex == 1) ? 0 : 1;
        animator.SetInteger("weaponIndex", weaponIndex);
    }

    void CollectWeapon(int count)
    {
        obtainedWeapons += count;
    }


    public void LighthAttack()
    {
        animator.SetInteger("attacktype", 1);
        //attacks.CreateHitbox(0,0);
    }

    public void HeavyAttack()
    {
        animator.SetInteger("attacktype", 2);
        if (weaponIndex == 1) {
                Invoke("resetAttackType",0.3f);
        }

    }

    void resetAttackType() {
        animator.SetInteger("attacktype", 0);
    }

    public void createLightHitbox()
    {
        attacks.CreateHitbox(weaponIndex, 0);
    }
    public void createHeavyHitbox()
    {
        attacks.CreateHitbox(weaponIndex, 1);
    }

    public void playLightAttackSFX()
    {
        sound.playSound(getWeapon() + "Light");// Weapon name has to be the same as in soundManager.cs / playSound()
    }
    public void playHeavyAttackSFX()
    {
        sound.playSound(getWeapon() + "Heavy"); // Weapon name has to be the same as in soundManager.cs / playSound()
    }

}

