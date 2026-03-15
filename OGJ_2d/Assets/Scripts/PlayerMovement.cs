using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float maxStretch = 10;

    public float stiffness;
 
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Vector3 _spawnPosition;

    private bool stretching;
    private Vector3 stretchOrigin;
    private Vector3 originalScale;

    private Collider2D col;

    public float maxTorqueStretch;

    // Reactivate collider after stretch timer
    public float colTimer = 1;
    private bool colTimerOn;
    private float colTimerTarget;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        GameManager.Instance.OnGameReset += OnGameReset;
        InputManager.Instance.OnMovePressed += OnMove;
        _spawnPosition = transform.position;
        originalScale = transform.localScale;
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

    private void Update()
    {
        Mouse mouse = Mouse.current;

        Vector3 mouseScreen = mouse.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;


        if (mouse.leftButton.wasPressedThisFrame)
        {
            if (col.OverlapPoint(mouseWorld) 
                && _rb.totalTorque < maxTorqueStretch 
                && Mathf.Abs(_rb.linearVelocityY) < 0.5)
            {
                stretching = true;
                stretchOrigin = transform.position;
                col.enabled = false;
                _rb.Sleep();
            }
        }

        if (stretching && mouse.leftButton.isPressed)
        {
            stretchSprite(mouseWorld);
        }

        if (stretching && mouse.leftButton.wasReleasedThisFrame)
        {
            lauch(mouseWorld);
        }

        if (colTimerOn && colTimerTarget <= Time.time)
        {
            colTimerOn = false;
            col.enabled = true;
        }
    }

    private void OnMove(InputValue value)
    {
        Debug.Log($"PlayerMovement: OnMove {value}");
        if (!stretching) _moveInput = value.Get<Vector2>();
    }


    private void GoLeft()
    {
        _rb.AddTorque(speed);
    }

    private void GoRight()
    {
        _rb.AddTorque(-speed);
    }

    private void stretchSprite(Vector3 target)
    {
        Vector2 stretchDir = target - transform.position;

        float angle = 180 - Vector2.Angle(stretchDir, Vector2.right);
        transform.rotation = Quaternion.Euler(0f, 0f, stretchDir.y < 0 ? angle : - angle);

        float stretchStrength = stretchDir.magnitude / 10;
        transform.localScale = originalScale + new Vector3(stretchStrength, 0f, 0f);

        float stretchRatio = originalScale.x / transform.localScale.x;
        transform.position = stretchOrigin + (new Vector3(stretchDir.x, stretchDir.y, 0f) * 0.5f);
    }

    private void lauch(Vector3 finalStretch)
    {
        transform.localScale = originalScale;
        transform.position = stretchOrigin;
        _rb.WakeUp();
        
        Vector2 stretchDir = transform.position - finalStretch;
        if (stretchDir.magnitude > maxStretch)
        {
            stretchDir = stretchDir.normalized * maxStretch;
        }

        Vector2 stretchForce = stretchDir * stiffness;
        _rb.AddForce(stretchForce);
        Debug.Log("force added " + stretchForce);

        stretching = false;
        colTimerOn = true;   
        colTimerTarget = Time.time + colTimer;
    }
}
