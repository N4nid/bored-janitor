using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class Turtle : MonoBehaviour
{
    [SerializeField] Transform player; 
    [SerializeField] Movement movemnet;  
    [SerializeField] float maxSpeed = 10;
    [SerializeField] float dashDistX  = 1f;
    [SerializeField] float dashDistY = 0.7f;
    [SerializeField] float jumpDistX  = 3.5f;
    [SerializeField] float jumpDistY  = 3f;

    [SerializeField] float spotDistance = 10f;
    [SerializeField] float spotFOV = 60;
    [SerializeField] float width = 1.2f;
    [SerializeField] float height = 1.2f;
    [SerializeField] float roamingRadius = 4f;
    [SerializeField] float jumpForce = 10f;
    float groundDistanceMargin = 0.1f;


    bool isFacingRight = false;
    Vector2[] roamBounds;
    bool isJumping = false;


   
    void Update()
    {
        if (isOnGround()) {
            Vector2 lookDirec = isFacingRight ? Vector2.right : Vector2.left;
            float distY = Math.Abs(player.position.y - transform.position.y);
            if(canSeePlayer(transform.position,lookDirec,spotFOV,12)) {
                gotoPlayer();
            }
            else {
                if (roamBounds == null) {roamBounds = calculateGroundRoamBounds(roamingRadius);}
                roam();
            }
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground")) {
            roamBounds = calculateGroundRoamBounds(roamingRadius);
        }
    }

    void roam() {
        Debug.Log("Is Roaming");
        float gettopoint = isFacingRight ? roamBounds[1].x : roamBounds[0].x;
            Debug.Log("Trysing to get to: " + gettopoint);
        if (transform.position.x <= roamBounds[0].x && !isFacingRight) {
            flipDirection();
        }
         if (transform.position.x >= roamBounds[1].x && isFacingRight) {
            flipDirection();
        }
        movemnet.move(maxSpeed,isFacingRight);
    }

    void gotoPlayer() {
        float distX = Math.Abs(player.position.x- transform.position.x);
        float distY = Math.Abs(player.position.y - transform.position.y);
        if (distX < jumpDistX && distY > dashDistY && (maxSpeed - movemnet.rb.linearVelocityX) < 0.2f && !isJumping) {
            isJumping = true;
            movemnet.jump(jumpForce);
            Invoke("resetJump",0.3f);
            Debug.Log("Jump");
        }
        if (distX > dashDistX || distY > dashDistY) {
            movemnet.move(maxSpeed,isFacingRight);
            Debug.Log("moving");
        }
        else {
            Debug.Log("Dahsing now"); // Dashed hier
        }
    }

    void resetJump() {
        isJumping = false;
    }

    bool isOnGround() {
        RaycastHit2D downLookLeft = Physics2D.Raycast(transform.position - new Vector3(width / 2,0f),Vector2.down);
        RaycastHit2D downLookRight = Physics2D.Raycast(transform.position + new Vector3(width / 2,0f),Vector2.down);
        bool isOnGroundLeft = downLookLeft.collider != null && downLookLeft.distance < height / 2 + groundDistanceMargin;
        bool isOnGroundRight = downLookRight.collider != null && downLookRight.distance < height / 2 + groundDistanceMargin;
        if (isOnGroundLeft || isOnGroundRight) {
            RaycastHit2D downLook = (isOnGroundLeft) ? downLookLeft : downLookRight;
            if (downLook.collider.gameObject.tag.Equals("Platform")) {
                roamBounds = downLook.collider.gameObject.GetComponent<RoamBounds>().getPosArr();
            }
            return true;
        }
        return false;
    }

    Vector2[] calculateGroundRoamBounds(float maxBoundDist) {
        float leftDist = getSmallestDist(getFullRaycast(transform.position,height - 0.1f,Vector2.left));
        float rightDist = getSmallestDist(getFullRaycast(transform.position,height - 0.1f,Vector2.right));
        if (leftDist >= maxBoundDist || leftDist == -1f) {
            leftDist = maxBoundDist;
        }
        if (rightDist >= maxBoundDist || rightDist == -1f) {
            rightDist = maxBoundDist;
        }
        Vector2 leftBound = transform.position, rightBound = transform.position;
        leftBound -= new Vector2(leftDist - width / 2 - 0.1f,0f);
        rightBound += new Vector2(rightDist - width / 2 - 0.1f,0f);
        Debug.Log("Left Dist: " + leftDist);
        Debug.Log("Right Dist: " + leftDist);
        Debug.Log("Left Bound: " + leftBound.x);
        Debug.Log("Right Bound: " + rightBound.x);
        return new Vector2[] {leftBound, rightBound};
    }

    RaycastHit2D[] getFullRaycast(Vector2 pos, float height, Vector2 directionVector) {
        RaycastHit2D[] raycasts = new RaycastHit2D[10];
        for (int i = -5; i < 5;i++) {
            raycasts[i+5] = Physics2D.Raycast(pos + new Vector2(0f,i * (height / 10)),directionVector);
        }
        return raycasts;
    }

    bool canSeePlayer (Vector2 pos, Vector2 lookDirection, float viewConeAngle, int numberCasts) {
        RaycastHit2D[] raycasts = new RaycastHit2D[numberCasts];
        float angleStepFac = 1f / (float)numberCasts;
        for (int i = 0; i < numberCasts;i++) {
            float currentAngle = (-viewConeAngle / 2) + i * angleStepFac * viewConeAngle;
            Vector3 ThreeDDirecVector = Quaternion.AngleAxis(currentAngle,Vector3.forward) * new Vector3(lookDirection.x,lookDirection.y,0f);
            Vector2 directionVector = new Vector2(ThreeDDirecVector.x,ThreeDDirecVector.y);
            raycasts[i] = Physics2D.Raycast(pos ,directionVector * spotDistance);
        }
        for (int i = 0; i < raycasts.Length; i++) {
            if(raycasts[i].collider != null && raycasts[i].collider.tag.Equals("Player") && raycasts[i].distance < spotDistance) {
                return true;
            }
        }
        return false;
    }

    public float getSmallestDist(RaycastHit2D[] hits) {
        float smallestDist = -1f;
        for(int i = 0; i < hits.Length; i++) {
            if ((hits[i].distance < smallestDist || smallestDist == -1) && hits[i].distance > 0) {
                smallestDist = hits[i].distance;
            }
        }
        return smallestDist;
    }

    void flipDirection() {
        transform.localScale = -transform.localScale;
        isFacingRight = !isFacingRight;
    }


}
