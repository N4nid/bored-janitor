using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class Turtle : MonoBehaviour
{
    Transform player;
    [SerializeField] Movement movemnet;
    [SerializeField] float maxSpeed = 10;
    [SerializeField] float jumpDistX = 3.5f;

    [SerializeField] float spotDistance = 20f;
    [SerializeField] float spotFOV = 90;
    [SerializeField] float width = 1.2f;
    [SerializeField] float height = 1.2f;
    [SerializeField] float roamingRadius = 4f;
    [SerializeField] float jumpForce = 10f;
    float directionX = 1f;
    float groundDistanceMargin = 0.1f;
    Animator crabAnimator;


    bool isFacingRight = false;

    bool isJumping = false;

    void Start()
    {
        crabAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }


    void Update()
    {
        if (isOnGround())
        {
            crabAnimator.SetBool("isJumping",false);
            Vector2 lookDirec = isFacingRight ? Vector2.right : Vector2.left;
            float distY = Math.Abs(player.position.y - transform.position.y);
            if (canSeePlayer(transform.position, lookDirec, spotFOV, 12))
            {
                gotoPlayer();
            }
            else
            {

                roam();
            }
        }
        else {
            crabAnimator.SetBool("isJumping",true);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.transform)
    }

    void roam()
    {
        //Debug.Log("Roaming");   
        float smallestDist = getSmallestDist(getFullRaycast(transform.position, height, Vector2.left * directionX)) ;
        if (smallestDist < 0.1f && smallestDist != -1f)
        {
            flipDirection();
        }
        movemnet.move(maxSpeed, isFacingRight);
    }

    void gotoPlayer()
    {
        float distX = Math.Abs(player.position.x - transform.position.x);
        float distY = Math.Abs(player.position.y - transform.position.y);
        //Debug.Log(distY);
        if (distX < jumpDistX && (maxSpeed - Math.Abs(movemnet.rb.linearVelocityX)) < 2f && !isJumping)
        {
            isJumping = true;
            movemnet.jump(jumpForce);
            Invoke("resetJump", 0.3f);
            //Debug.Log("Jump");
        }
        else
        {
            movemnet.move(maxSpeed, isFacingRight);
            //Debug.Log("moving");
        }
    }   

    void resetJump()
    {
        isJumping = false;
    }

    bool isOnGround()
    {
        RaycastHit2D downLookLeft = Physics2D.Raycast(transform.position - new Vector3(width / 2, height / 2 + groundDistanceMargin), Vector2.down);
        RaycastHit2D downLookRight = Physics2D.Raycast(transform.position + new Vector3(width / 2, -height / 2 - groundDistanceMargin), Vector2.down);
        bool isOnGroundLeft = downLookLeft.collider != null && downLookLeft.distance <= 0;
        bool isOnGroundRight = downLookRight.collider != null && downLookRight.distance <= 0;
        if (isOnGroundLeft && !isOnGroundRight && isFacingRight)
        {
            //Debug.Log("switching becuase of ground");
            flipDirection();
        }
        if (!isOnGroundLeft && isOnGroundRight && !isFacingRight)
        {
            // Debug.Log("switching becuase of ground");
            flipDirection();
        }
        if (isOnGroundLeft || isOnGroundRight)
        {
            return true;
        }
        return false;
    }

    RaycastHit2D[] getFullRaycast(Vector2 pos, float height, Vector2 directionVector)
    {
        RaycastHit2D[] raycasts = new RaycastHit2D[10];
        for (int i = -5; i < 5; i++)
        {
            //Debug.Log((pos + new Vector2((-width / 2 - 0.05f) * transform.localScale.x, i * (height / 10) + 0.1f)));
            raycasts[i + 5] = Physics2D.Raycast(pos + new Vector2((-width / 2 - 0.05f) * directionX, i * (height / 10) + 0.1f), directionVector);
            //Debug.DrawRay(pos + new Vector2((-width / 2 - 0.05f) * transform.localScale.x, i * (height / 10) + 0.1f), directionVector * 10, Color.yellow);
        }
        return raycasts;
    }

    bool canSeePlayer(Vector2 pos, Vector2 lookDirection, float viewConeAngle, int numberCasts)
    {
        RaycastHit2D[] raycasts = new RaycastHit2D[numberCasts];
        float angleStepFac = 1f / (float)numberCasts;
        for (int i = 0; i < numberCasts; i++)
        {
            float currentAngle = (-viewConeAngle / 2) + i * angleStepFac * viewConeAngle;
            Vector3 ThreeDDirecVector = Quaternion.AngleAxis(currentAngle, Vector3.forward) * new Vector3(lookDirection.x, lookDirection.y, 0f);
            Vector2 directionVector = new Vector2(ThreeDDirecVector.x, ThreeDDirecVector.y);
            raycasts[i] = Physics2D.Raycast(pos + new Vector2(width / 2 + .1f, 0f) * lookDirection, directionVector * spotDistance);
            //Debug.DrawRay(pos + new Vector2(width/2+.1f,0f) * lookDirection,directionVector * spotDistance, Color.yellow);
        }
        for (int i = 0; i < raycasts.Length; i++)
        {
            if (raycasts[i].collider != null)
            {
                Debug.Log(raycasts[i].collider.gameObject.name);    
            }
            if (raycasts[i].collider != null && raycasts[i].collider.tag.Equals("Player") && raycasts[i].distance < spotDistance)
            {
                return true;
            }
        }
        return false;
    }

    public float getSmallestDist(RaycastHit2D[] hits)
    {
        float smallestDist = -1f;
        for (int i = 0; i < hits.Length; i++)
        {
            if ((hits[i].distance < smallestDist || smallestDist == -1) && hits[i].collider != null && hits[i].collider.gameObject.tag != "Ground")
            {
                Debug.Log(hits[i].collider.gameObject.name);
                smallestDist = hits[i].distance;
            }
        }
        return smallestDist;
    }

    bool isSeeingPlayer()
    {
        Vector2 lookVector = (isFacingRight) ? Vector2.right : Vector2.left;
        RaycastHit2D SeeingUp = Physics2D.Raycast(transform.position + new Vector3(0, height / 2), lookVector);
        RaycastHit2D SeeingDown = Physics2D.Raycast(transform.position - new Vector3(0, height / 2), lookVector);
        bool isSeeingUp = SeeingUp.collider != null && SeeingUp.distance < spotDistance && SeeingUp.collider.gameObject.tag == "Player";
        bool isSeeingDown = SeeingDown.collider != null && SeeingDown.distance < spotDistance && SeeingDown.collider.gameObject.tag == "Player";
        return isSeeingDown || isSeeingUp;
    }

    void flipDirection()
    {
        transform.localScale *= new Vector2(-1,1);
        directionX *= -1;
        isFacingRight = !isFacingRight;
    }


}
