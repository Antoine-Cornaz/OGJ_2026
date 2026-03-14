using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    
    [SerializeField] private float _speed = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        GameManager.Instance.OnGameReset += OnGameReset;
    }

    private void OnGameReset()
    {
        Debug.Log($"PlayerMovement: OnGameReset");
        transform.position = Vector3.zero;
        _rb.linearVelocity = Vector2.zero;
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

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        Debug.Log($"PlayerMovement: OnMove {value}");
    }


    private void GoLeft()
    {
        _rb.AddTorque(_speed);
    }

    private void GoRight()
    {
        _rb.AddTorque(-_speed);
    }
}
