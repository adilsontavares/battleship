using System.Collections.Generic;
using Tween.Animation;
using Tween.Animation.Ease;
using UnityEngine;

public class CameraContainer : MonoBehaviour
{
    public string Name;

    [SerializeField]
    private float _orthographicSize = 5;

    [ContextMenu("Add Main Camera")]
    void AddMainCamera()
    {
        var mainCamera = CameraController.Main;

        if (mainCamera == null)
            Debug.LogWarning("Main CameraController not found...");
        else
            AddCamera(mainCamera, false);
    }

    public void AddCamera(CameraController camera, bool animated = true)
    {
        camera.transform.parent = transform;
        
        if (animated)
        {
            var data = new Dictionary<string, object>();
            data["camera"] = camera;
            data["position"] = camera.transform.localPosition;
            data["rotation"] = camera.transform.localRotation;
            data["orthographicSize"] = camera.GetComponent<Camera>().orthographicSize;

            this.CreateAnimation<EaseQuadInOut>(0.5f, 0f, "move-to-container", updateCallback: OnAnimMoveToContainer, data: data);
        }
        else
        {
            camera.transform.Reset();
            camera.GetComponent<Camera>().orthographicSize = _orthographicSize;
        }
    }

    void OnAnimMoveToContainer(AnimationBehaviour anim, float time)
    {
        var data = (Dictionary<string, object>)anim.Data;

        var camera = data["camera"] as CameraController;
        var position = (Vector3)data["position"];
        var rotation = (Quaternion)data["rotation"];
        var orthographicSize = (float)data["orthographicSize"];

        camera.transform.localPosition = Vector3.Lerp(position, Vector3.zero, time);
        camera.transform.localRotation = Quaternion.Slerp(rotation, Quaternion.identity, time);
        camera.GetComponent<Camera>().orthographicSize = Mathf.Lerp(orthographicSize, _orthographicSize, time);
    }

    void OnValidate()
    {
        foreach (var camera in GetComponentsInChildren<CameraController>())
        {
            camera.transform.Reset();
            camera.GetComponent<Camera>().orthographicSize = _orthographicSize;
        }
    }
}
