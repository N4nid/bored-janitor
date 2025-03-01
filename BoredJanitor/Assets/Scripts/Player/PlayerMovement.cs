using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 velocity;
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D body;
    float groundDistanceMargin = 0.1f;
    public bool onGround = true;
    [SerializeField] float jumpForce = 250f;
    [SerializeField] float moveForce = 500f;
    [SerializeField] float maxSpeed = 20f;
    bool isJumping = false;
    [SerializeField] float delay = 0.09f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        onGround = isOnGround();
    }

    public void jump()
    {
        if (onGround && !isJumping)
        {
            body.AddForceY(jumpForce, ForceMode2D.Impulse);
            Debug.Log("supposed to jump " + onGround);
            isJumping = true;
            Invoke("resetJump", delay);
        }
    }

    void resetJump()
    {
        isJumping = false;
        Debug.Log("reset Jump ");
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
        if (Math.Abs(body.linearVelocityX) < maxSpeed)
        {
            Debug.Log("jsad" + body.linearVelocityX);
            body.AddForceX(moveForce * direction * Time.deltaTime);
        }
    }
    bool isOnGround()
    {
        float width = 1f; // Player width 
        float height = 2.4f;
        RaycastHit2D downLookLeft = Physics2D.Raycast(transform.position - new Vector3(width / 2, height / 2f + groundDistanceMargin), Vector2.down);
        RaycastHit2D downLookRight = Physics2D.Raycast(transform.position + new Vector3(width / 2, - (height / 2f + groundDistanceMargin)), Vector2.down);
        //Debug.Log(downLookRight.collider.gameObject.name + "  " + downLookRight.distance);
        //Debug.DrawRay(transform.position - new Vector3(width / 2, 0f), Vector2.down, Color.yellow, 1);
        //Debug.DrawRay(transform.position + new Vector3(width / 2, 0f), Vector2.down, Color.yellow, 1);
        bool isOnGroundLeft = downLookLeft.collider != null && downLookLeft.distance <= 0;
        bool isOnGroundRight = downLookRight.collider != null && downLookRight.distance <= 0;
        if (isOnGroundLeft || isOnGroundRight)
        {
            return true;
        }
        return false;
    }
}
