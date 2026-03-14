using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1;
 
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Vector2 _spawnPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        GameManager.Instance.OnGameReset += OnGameReset;
        InputManager.Instance.OnMovePressed += OnMove;
        _spawnPosition = transform.position;
    }

    private void OnGameReset()
    {
        Debug.Log($"PlayerMovement: OnGameReset");
        transform.position = _spawnPosition;
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        switch (_moveInput.x)
        {
            case > 0.1f:
                GoRight();
                break;
            case < -0.1f:
                GoLeft();
                break;
        }
    }

    private void OnMove(InputValue value)
    {
        Debug.Log($"PlayerMovement: OnMove {value}");
        _moveInput = value.Get<Vector2>();
    }


    private void GoLeft()
    {
        _rb.AddTorque(speed);
    }

    private void GoRight()
    {
        _rb.AddTorque(-speed);
    }
}
