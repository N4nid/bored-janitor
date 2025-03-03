using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] GameObject player;
    InputAction jumpAction;
    InputAction moveLeftAction;
    InputAction moveRightAction;
    InputAction heavyAttack;
    InputAction lightAttack;
    InputAction changeWeapon;
    bool keyPressed = false;
    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerAttackManager playerAttackManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("Started");
        jumpAction = InputSystem.actions.FindAction("jump");
        moveLeftAction = InputSystem.actions.FindAction("moveLeft");
        moveRightAction = InputSystem.actions.FindAction("moveRight");
        lightAttack = InputSystem.actions.FindAction("lightAttack");
        heavyAttack = InputSystem.actions.FindAction("heavyAttack");
        changeWeapon = InputSystem.actions.FindAction("changeWeapon");
        //Debug.Log("Actions: " + moveRightAction + "|" + moveLeftAction + "|" + jumpAction);
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpAction.IsPressed())
        {
            movement.jump();
        }
        if (moveRightAction.IsPressed())
        {
            movement.move(false);
        }
        if (moveLeftAction.IsPressed())
        {
            movement.move(true);
        }
        if (lightAttack.IsPressed())
        {
            playerAttackManager.LighthAttack();
        }
        if (heavyAttack.IsPressed()&& !keyPressed)
        {
            playerAttackManager.HeavyAttack();
            keyPressed = true;
            Invoke("resetPressed",0.3f);
        }
        if (changeWeapon.IsPressed() && !keyPressed) {
            playerAttackManager.SwapWeapons();
            keyPressed = true;
            Invoke("resetPressed",0.4f);
        }

    }
    void resetPressed() {
        keyPressed = false;
    }
}
