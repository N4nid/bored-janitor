using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float lifeTime = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0){
            Object.Destroy(this.gameObject);
        }
        Debug.Log(lifeTime);
    }
}
