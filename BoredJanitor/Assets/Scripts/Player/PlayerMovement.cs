using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 velocity;
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D body;
    float groundDistanceMargin = 0.1f;
    public bool onGround = true;
    float jumpForce = 100f;
    float moveForce = 100f;
    float maxSpeed = 100f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!onGround)
        {
            onGround = isOnGround();
        }
    }

    public void jump()
    {
        if (onGround)
        {
            body.AddForceY(jumpForce * Time.deltaTime, ForceMode2D.Impulse);
            onGround = false;
            Debug.Log("supposed to jump " + onGround);
        }
    }

    public void move(bool left)
    {
        float direction;
        if (left)
        {
            direction = -1f;

        }
        else
        {
            direction = 1f;
        }
        if (Mathf.Abs(body.linearVelocityX) < maxSpeed)
        {
            Debug.Log("please work" + body.linearVelocityX);
            body.AddForceX(direction * moveForce * Time.deltaTime, ForceMode2D.Force);
        }
    }
    bool isOnGround()
    {
        float width = 1f; // Player width 
        float height = 2.3f;
        RaycastHit2D downLookLeft = Physics2D.Raycast(transform.position - new Vector3(width / 2, 0f), Vector2.down);
        RaycastHit2D downLookRight = Physics2D.Raycast(transform.position + new Vector3(width / 2, 0f), Vector2.down);
        bool isOnGroundLeft = downLookLeft.collider != null && downLookLeft.distance < height / 2 + groundDistanceMargin;
        bool isOnGroundRight = downLookRight.collider != null && downLookRight.distance < height / 2 + groundDistanceMargin;
        if (isOnGroundLeft || isOnGroundRight)
        {
            Debug.Log("YAY on ground");
            return true;
        }
        return false;
    }
}
