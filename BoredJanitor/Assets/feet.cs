using System;
using UnityEngine;

public class feet : MonoBehaviour
{
    public Boolean high;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Boolean GetHigh(){
        return high;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        high = true;
    }
    void OlisionExit2D(Collision2D collision)
    {
        high = false;
    }

}
