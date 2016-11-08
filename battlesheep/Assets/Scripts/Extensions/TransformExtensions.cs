using UnityEngine;

public static class TransformExtensions
{
    public static void Reset(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one; 
    }

    public static T GetOrAddComponent<T>(this Transform transform) where T : Component
    {
        var component = transform.GetComponent<T>();

        if (component == null)
            component = transform.gameObject.AddComponent<T>();

        return component;
    }
}