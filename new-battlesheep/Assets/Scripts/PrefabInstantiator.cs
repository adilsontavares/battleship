using UnityEngine;
using System.Collections;

public class PrefabInstantiator : MonoBehaviour
{
    public enum InstantiateOption
    {
        OnAwake,
        OnStart,
        Manually
    }

    public InstantiateOption StartupOption;
    public GameObject Prefab;

    private bool _instantiated = false;
    public bool Instantiated {  get { return _instantiated; } }

    public bool CanInstantiate()
    {
        return !_instantiated && Prefab != null;
    }

    void Awake()
    {
        if (StartupOption == InstantiateOption.OnAwake)
            Instantiate();
    }

    void Start()
    {
        if (StartupOption == InstantiateOption.OnStart)
            Instantiate();
    }

    [ContextMenu("Instantiate now")]
    public GameObject Instantiate()
    {
        if (!CanInstantiate())
            return null;

        if (Application.isPlaying)
            _instantiated = true;

        return CreateInstance();
    }

    public GameObject CreateInstance()
    {
        var instance = GameObject.Instantiate(Prefab);
        var transform = instance.transform;

        transform.parent = this.transform;
        transform.Reset();

        return instance;
    }
}
