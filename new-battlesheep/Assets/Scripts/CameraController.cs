using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public static CameraController Main { get { return FindMainInstance(); } }
    public static Camera MainCamera {  get { return Main.GetComponent<Camera>(); } }

    public Camera Camera { get { return GetComponent<Camera>(); } }

    CameraFocus _focus;
    public CameraFocus Focus { get { return _focus; } }

    public float Smooth = 10f;

    Vector3 _offset;
    Vector3 _rotationUp;
    Vector3 _rotationUpVelocity;
    Vector3 _positionVelocity;
    Vector3 _offsetVelocity;
    float _orthographicSizeVelocity;
    bool _3d = false;

    void Awake()
    {
        FocusOn(_focus, false);
    }

    public void Toggle3d()
    {
        _3d = !_3d;
    }

    public void FocusOn(CameraFocus focus)
    {
        FocusOn(focus, Application.isPlaying);
    }

    public void FocusOn(CameraFocus focus, bool animated)
    {
        _focus = focus;

        if (focus && !animated)
            Camera.orthographicSize = focus.OrthographicSize;
    }

    void Update()
    {
        if (_focus != null)
            UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        var time = Time.deltaTime * Smooth;

        var offset = _focus.GetFocusPosition() - _focus.GetCameraPosition(_3d);
        _offset = Vector3.SmoothDamp(_offset, offset, ref _offsetVelocity, time);

        transform.position = Vector3.SmoothDamp(transform.position, _focus.GetCameraPosition(_3d), ref _positionVelocity, time);

        _rotationUp = Vector3.SmoothDamp(_rotationUp, _3d ? Vector3.up : Vector3.forward, ref _rotationUpVelocity, time);
        transform.rotation = Quaternion.LookRotation(_offset, _rotationUp);

        Camera.orthographicSize = Mathf.SmoothDamp(Camera.orthographicSize, _focus.OrthographicSize, ref _orthographicSizeVelocity, time);
    }

    private static CameraController FindMainInstance()
    {
        return Camera.main.GetComponent<CameraController>();
    }
}