using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  private Vector3 velocity;
  [SerializeField] GameObject player;
  [SerializeField] Rigidbody2D body;
  [SerializeField] float jumpForce;
  [SerializeField] float pullUpForce = 40f;
  [SerializeField] float moveForce = 15000f;
  [SerializeField] float maxSpeed = 4f;
  [SerializeField] float delay = 0.09f;
  [SerializeField] Animator playerAnime;
  [SerializeField] float pullupRayLength = 1f;
  [SerializeField] float pullupFactor = 0.3f;
  [SerializeField] float test = 0.3f;
  bool isJumping = false;
  float direction;
  float groundDistanceMargin = 0.1f;
  public bool onGround = true;
  public bool canDoPullUp = true;



  // Update is called once per frame
  void Update()
  {
    playerAnime.SetFloat("xVel", Math.Abs(body.linearVelocityX));
    onGround = isOnGround();
    canDoPullUp = canClimbLedge();

    playerAnime.SetBool("isHigh", !onGround);
  }

  public void jump()
  {
    if (canDoPullUp && body.linearVelocityY <= 0)
    {
      //body.AddForceY(pullUpForce, ForceMode2D.Impulse);
      body.AddForce(new Vector2(pullUpForce * direction * pullupFactor, pullUpForce), ForceMode2D.Impulse);
    }
    else if (onGround && !isJumping)
    {
      body.AddForceY(jumpForce, ForceMode2D.Impulse);
      isJumping = true;
      Invoke("resetJump", delay);
    }
  }

  void resetJump()
  {
    isJumping = false;
  }

  public void move(bool left)
  {
    if (left)
    {
      direction = -1f;
      transform.localScale = new Vector2(-1, 1);
    }
    else
    {
      direction = 1f;
      transform.localScale = new Vector2(1, 1);
    }
    if (Math.Abs(body.linearVelocityX) < maxSpeed)
    {
      body.AddForceX(moveForce * direction * Time.deltaTime);
    }
  }

  bool canClimbLedge()
  {
    float height = 2.2f;
    for (int i = 0; i < 3; i++)
    {
      Vector2 rayDirection = (direction == -1) ? Vector2.left : Vector2.right;
      Vector3 origin = new Vector3(player.transform.localScale.x, height + (i * 0.2f));
      Debug.DrawRay(transform.position + origin, rayDirection * pullupRayLength, Color.green, 0.2f, true);
      RaycastHit2D eyeLevel = Physics2D.Raycast(transform.position + origin, rayDirection, pullupRayLength);


      if (eyeLevel.collider != null)
      {

        //Debug.Log(eyeLevel.collider.gameObject.name + "  " + eyeLevel.collider.gameObject.tag + "   " + eyeLevel.distance);
        if (eyeLevel.collider.gameObject.tag == "Platform")
        {
          //Debug.Log("CAN CLIMB LEDGE");
          return true;
        }
      }
    }
    return false;
  }

  bool isOnGround()
  {
    float width = 1f; // Player width 
    int numberCasts = 20;
    float widthIcrement = width / numberCasts;



    for (int i = 0; i < numberCasts; i++)
    {
      RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-width / 2 + widthIcrement * i, -groundDistanceMargin), Vector2.down);
      if (hit.collider != null && hit.distance <= 0) { return true; }
    }
    return false;
  }
}
