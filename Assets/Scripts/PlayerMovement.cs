using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.Mathematics;
//Script brought to you by the flipping goat
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer armsprite;
    public Vector2 moveInput;
    private bool isPressingMove;
    private bool isRolling;
    private int facingdirection = 1;
    private Vector2 recoilOffsett;
    [SerializeField] ArmAndGunScript mouse;

    [Header("importante stuff")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float rollForce = 10f;
    public const float maxSpeed = 5f;
    public float acceleration = 35f;
    public float deceleration = 25f;
    private int jumpCount = 0;
    public int maxJumpCount = 2;

    float r;

    public LayerMask groundLayer;
    public Vector2 groundCheckSize = new Vector2(1f, 1f);
    public float groundCheckDistance = 0.1f;
    public Color groundCheckGizmoColor = Color.green;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        if (Grounded())
        {
            jumpCount = 0;
        }
        
        if(moveInput.x != 0){
            isPressingMove = true;
        }else{
            isPressingMove = false;
        }
        if (mouse.mousePos.x < 960 && facingdirection > 0)
        {
            FlipSprite("left");
        }
        else //(mouse.mousePos.x > 960 && facingdirection < 0)
        {
            FlipSprite("right");
        }

 
    }
    void FixedUpdate(){
        float targetSpeed = moveInput.x * maxSpeed;
        float tempAccel = acceleration;

        if(!isPressingMove){
            tempAccel = deceleration;
        }

        float temp = Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, tempAccel * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(temp, rb.linearVelocity.y)+recoilOffsett;
        recoilOffsett = Vector2.Lerp(recoilOffsett, Vector2.zero, Time.fixedDeltaTime * 10f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        
    }
    public void OnJump(InputAction.CallbackContext context){
        if (!context.performed) return;

        if (Grounded() || jumpCount < maxJumpCount)
        {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
    }
    public bool Grounded(){
        return Physics2D.BoxCast(transform.position, groundCheckSize, 0f, Vector2.down, groundCheckDistance, groundLayer);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = groundCheckGizmoColor;

        Vector3 startCenter = transform.position;
        Vector3 endCenter = startCenter + Vector3.down * groundCheckDistance;
        Vector3 half = new Vector3(groundCheckSize.x * 0.5f, groundCheckSize.y * 0.5f, 0f);

        Gizmos.DrawWireCube(startCenter, groundCheckSize);
        Gizmos.DrawWireCube(endCenter, groundCheckSize);

        Gizmos.DrawLine(startCenter + new Vector3(-half.x, half.y, 0f), endCenter + new Vector3(-half.x, half.y, 0f));
        Gizmos.DrawLine(startCenter + new Vector3(half.x, half.y, 0f), endCenter + new Vector3(half.x, half.y, 0f));
        Gizmos.DrawLine(startCenter + new Vector3(-half.x, -half.y, 0f), endCenter + new Vector3(-half.x, -half.y, 0f));
        Gizmos.DrawLine(startCenter + new Vector3(half.x, -half.y, 0f), endCenter + new Vector3(half.x, -half.y, 0f));
    }
    public void OnRoll(InputAction.CallbackContext context){
        if(context.performed && !isRolling){
            StartCoroutine(Roll());
        }
    }
    private IEnumerator Roll(){
        isRolling = true;
        rb.AddForce(Vector2.right * moveInput.x * rollForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.8f);
        isRolling = false;
    }

    public void FlipSprite(string direction)
    {
        if (direction == "right")
        {
            spriteRenderer.flipX = false;
            armsprite.flipX = false;
            armsprite.flipY = false;
        }
        else if (direction == "left")
        {
            spriteRenderer.flipX = true;
            armsprite.flipY = true;
        }



    }
    public void GetRecoiled(Vector2 direction)
    {
        rb.AddForce(direction, ForceMode2D.Impulse);
    }
}
