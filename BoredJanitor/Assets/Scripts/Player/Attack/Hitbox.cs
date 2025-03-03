using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Hitbox : MonoBehaviour
{
    List<GameObject> hittedObjects = new List<GameObject>();
    Rigidbody2D body;
    public float lifeTime = 100;
    public int attackIndex;
    public int weaponIndex;
    public Transform playerTrans;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag .Equals("Enimy") && !containsGameObj(hittedObjects,collision.gameObject)) {
            if (weaponIndex == 0) {
                if (attackIndex == 0) {
                    collision.gameObject.GetComponent<EnemyMangager>().damage(25,playerTrans.position.x);
                }
                if (attackIndex == 1) {
                    collision.gameObject.GetComponent<EnemyMangager>().damage(50,playerTrans.position.x);
                    
                }
            }
            else
            {
              if (attackIndex == 0) {
                    collision.gameObject.GetComponent<EnemyMangager>().damage(35,playerTrans.position.x);
                }
                if (attackIndex == 1) {
                    Debug.Log(collision.gameObject.name);
                    collision.gameObject.GetComponent<EnemyMangager>().damage(50,playerTrans.position.x);
                    body.AddForceY(-body.linearVelocityY * 40);
                    body.AddForceY(1500);
                    Debug.Log(body.linearVelocityY);
                }  
            }
            hittedObjects.Add(collision.gameObject);
        }
    }
    static bool containsGameObj(List<GameObject> objs, GameObject ob) {
        for(int i = 0; i < objs.Count; i++) {
            Debug.Log(objs[i].name + " , " + ob.name);
            if (GameObject.ReferenceEquals(objs[i],ob)){return true;}
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0){
            Destroy(gameObject);
        }
    }
}
