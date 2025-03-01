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
    [SerializeField] Animator playerAnime;

    // Update is called once per frame
    void Update()
    {
        playerAnime.SetFloat("xVel", Math.Abs(body.linearVelocityX));
        onGround = isOnGround();
        playerAnime.SetBool("isHigh",!onGround);
       // Debug.Log(onGround);
    }

    public void jump()
    {
        if (onGround && !isJumping)
        {
            body.AddForceY(jumpForce, ForceMode2D.Impulse);
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
            transform.localScale = new Vector2(-1,1);
        }
        else
        {
            direction = 1f;
            transform.localScale = new Vector2(1,1);
        }
        if (Math.Abs(body.linearVelocityX) < maxSpeed)
        {
            body.AddForceX(moveForce * direction * Time.deltaTime);
        }
    }
    bool isOnGround()
    {
        float width = 1f; // Player width 
        //float height = 2.4f;
        int numberCasts = 20;
        float widthIcrement = width / numberCasts;
        for(int i = 0; i < numberCasts; i++) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-width / 2 + widthIcrement * i,- groundDistanceMargin), Vector2.down);
            if (hit.collider != null && hit.distance <= 0){return true;}
        }
        return false;
    }
}
