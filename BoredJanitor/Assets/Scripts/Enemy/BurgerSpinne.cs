using UnityEngine;

public class BurgerSpinne : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    Movement movement;
    Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GetComponent<Movement>();
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    void roam()
    {
        
    }
    void Update()
    {
        
    }
}
