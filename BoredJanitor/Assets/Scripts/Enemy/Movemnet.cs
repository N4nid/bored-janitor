using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float accelaration = 10;
    public Rigidbody2D rb;
    public void move(float targetSpeedX, bool isRight)
    {
        if (Math.Abs(rb.linearVelocityX) < targetSpeedX) {
            float realAccelaration = (isRight) ? accelaration : -accelaration;
            rb.AddForceX(realAccelaration*Time.deltaTime);
            
        }
    }
    public void jump(float jumpForce) {
        rb.AddForce(Vector2.up * jumpForce);
    }
}
