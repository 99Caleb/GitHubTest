using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpSpeed = 12f;
    private Vector2 _desiredVelocity;
    
    [Header("isGrounded")]
    public LayerMask whatIsGround;
    
    [Header("Components")]
    private Rigidbody2D _rigidbody2D;
    private InputManager _input;

    [Header("DoubleJump")] 
    private int _doubleJumps = 1;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _input = GetComponent<InputManager>();
    }

    private void Update()
    {
        _desiredVelocity = _rigidbody2D.velocity;
        if (IsPlayerGrounded() == true)
        {
            _doubleJumps = 1;
        }


        if (_input.jumpPressed && IsPlayerGrounded())
        {
            //_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpSpeed);
            _desiredVelocity.y = jumpSpeed;
            
        }

        if (_input.jumpPressed && _doubleJumps > 0)
        {
            _doubleJumps--;
            _desiredVelocity.y = jumpSpeed;
        }
        
        if (_input.jumpReleased && _desiredVelocity.y > 0f)
        {
            //_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.2f);
            _desiredVelocity.y *= 0.5f;
        }

        _rigidbody2D.velocity = _desiredVelocity;
    }
    
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_input.moveDirection.x * moveSpeed, _rigidbody2D.velocity.y);
    }

    private bool IsPlayerGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1.1f, whatIsGround);
    }
}