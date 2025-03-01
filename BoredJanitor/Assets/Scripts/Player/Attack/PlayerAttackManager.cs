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
//a
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("starting attack");
            animator.SetInteger("attacktype",1);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("starting attack");
            animator.SetInteger("attacktype",2);
        }
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

    void LighthAttack(Vector2 direction){
        if(direction == Vector2.right){
            animator.SetInteger("attacktype",1);
        }else if (direction == Vector2.left){

        }else if (direction == Vector2.up){

        }else if (direction == Vector2.down){
        
        }

    }

    void HeavyAttack(Vector2 direction){
        
    }

}
    
