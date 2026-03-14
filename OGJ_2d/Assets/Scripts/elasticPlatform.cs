using UnityEngine;
using UnityEngine.InputSystem;

public class ElasticPlatform : MonoBehaviour
{
    [Header("Stretch")]
    [SerializeField] private float stretchFactor = 2.0f;
    [SerializeField] private float maxScaleMultiplier = 3.0f;

    [Header("Scale Speed")]
    [SerializeField] private float baseScaleSpeed = 10.0f;
    [SerializeField] private float minScaleSpeed = 0.75f;

    [Header("Return Animation")]
    [SerializeField] private float returnDuration = 0.8f;

    private Camera mainCamera;
    private Collider2D col;

    private Vector3 originalScale;
    private bool isDragging;
    private Vector3 dragStartWorldPos;

    private Vector3 currentTargetScale;
    private Vector3 returnStartScale;

    private bool isReturning;
    private float returnTimer;

    private void Awake()
    {
        mainCamera = Camera.main;
        col = GetComponent<Collider2D>();
        originalScale = transform.localScale;
        currentTargetScale = originalScale;
    }

    private void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null || mainCamera == null || col == null) return;

        Vector3 mouseScreen = mouse.position.ReadValue();
        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            if (col.OverlapPoint(mouseWorld))
            {
                isDragging = true;
                isReturning = false;
                dragStartWorldPos = mouseWorld;
            }
        }

        if (isDragging && mouse.leftButton.isPressed)
        {
            UpdateDragScale(mouseWorld);
            SmoothScaleTowardTarget();
        }

        if (isDragging && mouse.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
            StartReturn();
        }

        if (!isDragging && isReturning)
        {
            AnimateReturnElastic();
        }
    }

    private void UpdateDragScale(Vector3 currentMouseWorld)
    {
        Vector3 dragDelta = currentMouseWorld - dragStartWorldPos;

        float x = dragDelta.x;
        float y = dragDelta.y;

        float absX = Mathf.Abs(x);
        float absY = Mathf.Abs(y);

        if (dragStartWorldPos.x < transform.position.x)
            x *= -1;
        
        if (dragStartWorldPos.y < transform.position.y)
            y *= -1;
        

        float originalArea = originalScale.x * originalScale.y;

        Vector3 targetScale = originalScale;

        // Choose only one axis at a time
        if (absX >= absY)
        {
            float desiredX = originalScale.x + x * stretchFactor;
            float minX = originalScale.x / maxScaleMultiplier;
            float maxX = originalScale.x * maxScaleMultiplier;
            desiredX = Mathf.Clamp(desiredX, minX, maxX);

            float desiredY = originalArea / desiredX;
            targetScale = new Vector3(desiredX, desiredY, originalScale.z);
        }
        else
        {
            float desiredY = originalScale.y + y * stretchFactor;
            float minY = originalScale.y / maxScaleMultiplier;
            float maxY = originalScale.y * maxScaleMultiplier;
            desiredY = Mathf.Clamp(desiredY, minY, maxY);

            float desiredX = originalArea / desiredY;
            targetScale = new Vector3(desiredX, desiredY, originalScale.z);
        }

        currentTargetScale = targetScale;
    }

    private void SmoothScaleTowardTarget()
    {
        Vector3 current = transform.localScale;

        // Bigger object = slower scaling, linearly
        float sizeRatioX = current.x / originalScale.x;
        float sizeRatioY = current.y / originalScale.y;
        float dominantRatio = Mathf.Max(sizeRatioX, sizeRatioY);

        float adjustedSpeed = baseScaleSpeed / dominantRatio;
        adjustedSpeed = Mathf.Max(adjustedSpeed, minScaleSpeed);

        transform.localScale = Vector3.MoveTowards(
            current,
            currentTargetScale,
            adjustedSpeed * Time.deltaTime
        );
    }

    private void StartReturn()
    {
        isReturning = true;
        returnTimer = 0f;
        returnStartScale = transform.localScale;
    }

    private void AnimateReturnElastic()
    {
        returnTimer += Time.deltaTime;
        float t = Mathf.Clamp01(returnTimer / returnDuration);
        float eased = EaseOutElastic(t);

        transform.localScale = Vector3.LerpUnclamped(returnStartScale, originalScale, eased);

        if (t >= 1f)
        {
            transform.localScale = originalScale;
            isReturning = false;
        }
    }

    private float EaseOutElastic(float x)
    {
        const float c4 = (2f * Mathf.PI) / 3f;

        if (x == 0f) return 0f;
        if (x == 1f) return 1f;

        return Mathf.Pow(2f, -10f * x) * Mathf.Sin((x * 10f - 0.75f) * c4) + 1f;
    }
}