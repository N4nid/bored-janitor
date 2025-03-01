using System;
using UnityEngine;
using UnityEngine.Rendering;

public class DustBunny : MonoBehaviour
{
    [SerializeField] float spotDistance = 10f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform player;
    [SerializeField] float maxSpeed = 4;
    [SerializeField] float accelaration = 300;
    [SerializeField] float angularAcccelaration = 1000;
    [SerializeField] float maxAngularSpeed = 10;

    [SerializeField] float roamHeight = 4;
    [SerializeField] float playerDistanceThreshhold = 0.2f;
    [SerializeField] float health = 50f;
    [SerializeField] EnemyMangager mangager;
    float direction = 1f;
    bool isMovingUp = true;
    float roamLowerY;
    float roamHigherY;
    bool hasSeenPlayer;

    void Start()
    {
        roamLowerY = transform.position.y - 0.1f;
        roamHigherY = roamLowerY + roamHeight;
    }

    void Update()
    {
        if (canSeePlayer(transform.position,transform.rotation * Vector2.right * direction,50f,12) || hasSeenPlayer) {
           if (!hasSeenPlayer) {
            rb.AddForceY(-rb.linearVelocityY * 40);
           }
           hasSeenPlayer = true;
           goToPlayer();
        }
        else {
            roam();
        }
    }

    void roam()
    {
        if (transform.position.y > roamHigherY) {
            isMovingUp = false;
            switchDirection(false);
        } 
        if (transform.position.y < roamLowerY) {
            isMovingUp = true;
            switchDirection(true);
        } 
        Vector2 direcVector = isMovingUp ? Vector2.up : Vector2.down;
        moveInDirec(direcVector,maxSpeed,accelaration);
    }
    
    void goToPlayer() {
        Vector2 lookAtVector = player.position - transform.position;
        float distanceToPlayer = Vector2.Distance(player.position,transform.position);
        rotateToDirec(Vector2.Angle(Vector2.left,lookAtVector));
        if (distanceToPlayer > playerDistanceThreshhold) {
            moveInDirec(lookAtVector,maxSpeed,accelaration);
        }
        else {
            if (rb.linearVelocity.magnitude > 0.1f) {
                rb.AddForce(-rb.linearVelocity);
            }
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    void moveInDirec(Vector2 direction, float maxSpeed,float accelaration) {
        if (Math.Sqrt(Math.Pow(rb.linearVelocityX,2)+Math.Pow(rb.linearVelocityY,2)) < maxSpeed ||  20 < Math.Abs(Vector2.Angle(direction,rb.linearVelocity))) {
            rb.AddForce(direction*accelaration*Time.deltaTime);
        }
    }

    void rotateToDirec(float targetRotation) {
        this.transform.localRotation = Quaternion.Euler(0f,0f,targetRotation);
    }

    bool canSeePlayer (Vector2 pos, Vector2 lookDirection, float viewConeAngle, int numberCasts) {
        RaycastHit2D[] raycasts = new RaycastHit2D[numberCasts];
        float angleStepFac = 1f / (float)numberCasts;
        for (int i = 0; i < numberCasts;i++) {
            float currentAngle = (-viewConeAngle / 2) + i * angleStepFac * viewConeAngle;
            Vector3 ThreeDDirecVector = Quaternion.AngleAxis(currentAngle,Vector3.forward) * new Vector3(lookDirection.x,lookDirection.y,0f);
            Vector2 directionVector = new Vector2(ThreeDDirecVector.x,ThreeDDirecVector.y);
            raycasts[i] = Physics2D.Raycast(pos ,directionVector * spotDistance);
            Debug.DrawRay(pos,directionVector * spotDistance,Color.yellow);
        }
        for (int i = 0; i < raycasts.Length; i++) {
            if(raycasts[i].collider != null && raycasts[i].collider.tag.Equals("Player") && raycasts[i].distance < spotDistance) {
                return true;
            }
        }
        return false;
    }

    void switchDirection(bool isRight) {
        if (isRight) {
            direction = 1f;
            transform.localScale = new Vector2(Math.Abs(transform.localScale.x),Math.Abs(transform.localScale.y));
        }
        else {
            direction = -1f;
            transform.localScale = new Vector2(-Math.Abs(transform.localScale.x),-Math.Abs(transform.localScale.y));
        }
    }

}
