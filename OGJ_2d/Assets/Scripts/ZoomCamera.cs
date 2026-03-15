using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private float zoomClose = 10f;
    [SerializeField] private float zoomFar = 100f;

    private Camera _cam;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _cam =  GetComponent<Camera>();
        
        InputManager inputManager = InputManager.Instance;
        
        inputManager.OnZoomPressed +=  ZoomOut;
        inputManager.OnZoomReleased +=  ZoomIn;
        
        ZoomIn();
    }

    private void ZoomIn()
    {
        _cam.orthographicSize = zoomClose;
    }

    private void ZoomOut()
    {
        _cam.orthographicSize = zoomFar;
    }
}
