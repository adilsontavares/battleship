using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour
{
    public float OrthographicSize = 5;

    public Vector3 GetCameraPosition(bool is3d)
    {
        var offset = 10f;

        if (is3d)
        {
            var position = transform.position;
            position += Vector3.up * offset;
            position += Vector3.right * offset;
            position += Vector3.back * offset;

            return position;
        }

        return transform.position + Vector3.up * 10f;
    }

    public Vector3 GetFocusPosition()
    {
        return transform.position;
    }

    public Quaternion GetRotation(bool is3d)
    {
        return Quaternion.LookRotation(transform.position - GetCameraPosition(is3d));
    }
}
