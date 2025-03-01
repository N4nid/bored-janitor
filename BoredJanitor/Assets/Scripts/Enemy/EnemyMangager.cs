using UnityEngine;

public class EnemyMangager : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float knockbackMultplyer = 10f;
    GameObject player;
    Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void damage(float damageAmount, float comingFromX)
    {
        rb = GetComponent<Rigidbody2D>();
        float knockbackDirection = (comingFromX > transform.position.x) ? -1f : 1f;
        health -= damageAmount;
        rb.AddForceX(damageAmount * knockbackDirection * knockbackMultplyer);
        if (health <= 0)
        {
            player.GetComponent<PlayerManager>().gainMotivation(20f);
            player.GetComponent<PlayerManager>().killCounter++;
            Destroy(gameObject);
        }
    }

    public bool isDead()
    {
        return health > 0;
    }

}
