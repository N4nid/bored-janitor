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
    InputAction upAttack;
    InputAction downAttack;
    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerAttackManager playerAttackManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Started");
        jumpAction = InputSystem.actions.FindAction("jump");
        moveLeftAction = InputSystem.actions.FindAction("moveLeft");
        moveRightAction = InputSystem.actions.FindAction("moveRight");
        lightAttack = InputSystem.actions.FindAction("lightAttack");
        heavyAttack = InputSystem.actions.FindAction("heavyAttack");
        upAttack = InputSystem.actions.FindAction("upAttack");
        downAttack = InputSystem.actions.FindAction("downAttack");
        Debug.Log("Actions: " + moveRightAction + "|" + moveLeftAction + "|" + jumpAction);
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
        if (heavyAttack.IsPressed())
        {
            playerAttackManager.HeavyAttack();
        }
        if (upAttack.IsPressed())
        {
            playerAttackManager.UpAttack();
        }
         if (downAttack.IsPressed())
        {
            playerAttackManager.DownAttack();
        }

    }
}
