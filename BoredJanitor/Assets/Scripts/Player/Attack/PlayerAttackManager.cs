using System;
using UnityEngine;
using System.Collections;



public class PlayerAttackManager : MonoBehaviour
{
    public Animator animator;
    ArrayList obtainedWeapons = new ArrayList();
    public int weaponIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CollectWeapon("Broom");
        
    }


    public void endAttacking(){
        Debug.Log("ending attack");
        animator.SetInteger("attacktype",0);
    }

    void SwapWeapons(){
        if (weaponIndex < obtainedWeapons.Count-1){
            weaponIndex++;
        }
        else {
            weaponIndex = 0;
        }
    }

    void CollectWeapon(String weapon){
        obtainedWeapons.Add(weapon);
    }

    String getWeapon(){
        return (String) obtainedWeapons[weaponIndex];
    }

    public void LighthAttack(){
        animator.SetInteger("attacktype",1);
    }

    public void HeavyAttack(){
        animator.SetInteger("attacktype",2);
    }
    public void DownAttack(){
        animator.SetInteger("attacktype",3);
    }
    public void UpAttack(){
        animator.SetInteger("attacktype",4);
    }

}
    
