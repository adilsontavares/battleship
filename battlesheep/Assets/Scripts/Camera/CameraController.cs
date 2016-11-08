using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Main { get { return Camera.main.GetComponent<CameraController>(); } }
    public CameraContainer Container { get { return transform.GetComponentInParent<CameraContainer>(); } }

    public CameraContainer DefaultContainer;
    public CameraContainer OptionalContainer;

    public bool CanSwapContainers = true;

    void Start()
    {
        MoveToContainer(DefaultContainer, false);
    }

    public void MoveToContainer(CameraContainer container, bool animated = true)
    {
        container.AddCamera(this, animated);
    }

    public void MoveToContainer(string name, bool animated = true)
    {
        foreach (var container in FindObjectsOfType<CameraContainer>())
        {
            if (container.Name == name)
            {
                MoveToContainer(container);
                return;
            }
        }

        Debug.LogWarning(string.Format("Could not move CameraController to container '{0}' (Not found)", name));
    }

    void Update()
    {
        if (CanSwapContainers && Input.GetKeyDown(KeyCode.Space))
        {
            if (Container == DefaultContainer)
                MoveToContainer(OptionalContainer);
            else
                MoveToContainer(DefaultContainer);
        }
    }
}
