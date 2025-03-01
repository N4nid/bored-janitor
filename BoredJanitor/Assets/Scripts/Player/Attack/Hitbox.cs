using System;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    List<GameObject> hittedObjects = new List<GameObject>();
    public float lifeTime = 100;
    public int attackIndex;
    public int weaponIndex;
    public Transform playerTrans;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
