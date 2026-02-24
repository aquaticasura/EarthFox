using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Vector2 moveInput;
    private bool isPressingMove;
    [Header("importante stuff")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public const float maxSpeed = 5f;
    public float acceleration = 35f;
    public float deceleration = 25f;


    public LayerMask groundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        if (playerInput == null)
        {
            return;
        }

        playerInput.ActivateInput();
        if (playerInput.currentActionMap == null)
        {
            playerInput.SwitchCurrentActionMap("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(moveInput.x != 0){
            isPressingMove = true;
        }else{
            isPressingMove = false;
        }
    }
    void FixedUpdate(){
        float targetSpeed = moveInput.x * maxSpeed;
        float tempAccel = acceleration;

        if(!isPressingMove){
            tempAccel = deceleration;
        }

        float temp = Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, tempAccel * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(temp, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        
    }
    public void OnJump(InputAction.CallbackContext context){
        if(context.performed && Grounded()){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    public bool Grounded(){
        return Physics2D.BoxCast(transform.position, new Vector2(1,1), 0f, Vector2.down, 0.1f, groundLayer);
    }
}
