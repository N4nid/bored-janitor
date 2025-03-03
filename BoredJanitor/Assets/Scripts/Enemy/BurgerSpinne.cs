using UnityEngine;

public class BurgerSpinne : MonoBehaviour
{
    [SerializeField] float maxSpeed = 4f;
    Movement movement;
    Transform player;
    [SerializeField] float width = 1.1f;
    [SerializeField] float height= 1.2f;
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
