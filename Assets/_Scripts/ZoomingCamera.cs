using UnityEngine;

public class ZoomingCamera : MonoBehaviour
{
    private float _zoom;
    private readonly float _zoomMultiplier = 7f;
    private readonly float _minZoom = 4f;
    private readonly float _maxZoom = 20f;
    private readonly float _smoothTime = 0.1f;
    private float _zoomSpeed = 20f;
    
    [SerializeField] private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _zoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _zoom -= scroll * _zoomMultiplier;
        _zoom = Mathf.Clamp(_zoom, _minZoom, _maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, _zoom, ref _zoomSpeed, _smoothTime);
    }
}
