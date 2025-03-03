using UnityEngine;

public class EnemyMangager : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float enemyDamage = 10f;
    [SerializeField] float knockbackMultplyer = 10f;
    //[SerializeField] bool isBox = false;
    GameObject player;
    Rigidbody2D rb;
    PlayerManager playerManager;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
    }

    public void damage(float damageAmount, float comingFromX)
    {
        rb = GetComponent<Rigidbody2D>();
        float knockbackDirection = (comingFromX > transform.position.x) ? -1f : 1f;
        health -= damageAmount;
        rb.AddForceX(damageAmount * knockbackDirection * knockbackMultplyer);
        if (health <= 0)
        {
            playerManager.gainMotivation(20f);
            playerManager.killCounter++;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {

            if (!playerManager.isBored)
            {
                playerManager.loseMotivation(enemyDamage);
            }

        }
    }

    public bool isDead()
    {
        return health > 0;
    }

}
