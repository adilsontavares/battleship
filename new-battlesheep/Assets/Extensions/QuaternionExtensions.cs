using UnityEngine;

public static class QuaternionExtensions
{
    public static Quaternion SmoothDamp(this Quaternion rotation, Quaternion target, ref Vector3 velocity, float smoothTime)
    {
        var x = velocity.x;
        var y = velocity.y;
        var z = velocity.z;

        var euler = new Vector3();

        euler.x = Mathf.SmoothDampAngle(rotation.eulerAngles.x, target.eulerAngles.x, ref x, smoothTime);
        euler.y = Mathf.SmoothDampAngle(rotation.eulerAngles.y, target.eulerAngles.y, ref y, smoothTime);
        euler.z = Mathf.SmoothDampAngle(rotation.eulerAngles.z, target.eulerAngles.z, ref z, smoothTime);

        return Quaternion.Euler(euler);
    }
}
