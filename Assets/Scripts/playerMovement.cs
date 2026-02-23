using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    bool isWalking = false;
    bool canMove = true;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseController.IsGamePaused)
        {
            rb.linearVelocity = Vector2.zero;
            //animator.SetBool("isMoving", false);
            return;
        }
        rb.linearVelocity = moveInput * moveSpeed;


        
    }




    

    

    
    public void Move(InputAction.CallbackContext context)
    {

        if (context.canceled)
        {
            isWalking = false;
            //animator.SetBool("isMoving", false);
            //animator.SetFloat("LastInputX", moveInput.x);
            //animator.SetFloat("LastInputY", moveInput.y);
        }
        else if (moveInput.x != 0 || moveInput.y != 0)
        {
            isWalking = true;
        }
        if (canMove)
        {
            moveInput = context.ReadValue<Vector2>();
            //animator.SetFloat("InputX", moveInput.x);
            //nimator.SetFloat("InputY", moveInput.y);

        }
        else
        {
            return;
        }


    }
}
