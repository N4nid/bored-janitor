using System;
using UnityEngine;
using System.Collections;



public class PlayerAttackManager : MonoBehaviour
{
    public Animator animator;
    ArrayList obtainedWeapons = new ArrayList();
    [SerializeField] PlayerAttacks attacks;
    [SerializeField] soundManager sound;
    public int weaponIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CollectWeapon("Broom");

    }


    public void endAttacking()
    {
        Debug.Log("ending attack");
        animator.SetInteger("attacktype", 0);
    }

    void SwapWeapons()
    {
        if (weaponIndex < obtainedWeapons.Count - 1)
        {
            weaponIndex++;
        }
        else
        {
            weaponIndex = 0;
        }
    }

    void CollectWeapon(String weapon)
    {
        obtainedWeapons.Add(weapon);
    }

    String getWeapon()
    {
        return (String)obtainedWeapons[weaponIndex];
    }

    public void LighthAttack()
    {
        animator.SetInteger("attacktype", 1);
        //attacks.CreateHitbox(0,0);
    }

    public void createLightHitbox()
    {
        attacks.CreateHitbox(0, 0);
    }
    public void createHeavyHitbox()
    {
        attacks.CreateHitbox(0, 1);
        Debug.Log("Me HeavyAttack :D");
    }

    public void HeavyAttack()
    {
        animator.SetInteger("attacktype", 2);
        //attacks.CreateHitbox(0,1);
    }
    public void DownAttack()
    {
        animator.SetInteger("attacktype", 3);
    }
    public void UpAttack()
    {
        animator.SetInteger("attacktype", 4);
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

