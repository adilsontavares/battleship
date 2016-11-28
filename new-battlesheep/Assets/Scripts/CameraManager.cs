using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Main {  get { return FindObjectOfType<CameraManager>(); } }

    public CameraFocus PlayerBoardFocus;
    public CameraFocus EnemyBoardFocus;
    public CameraFocus GameFocus;

    void Start()
    {
        FocusOnPlayerBoard();
    }

    [ContextMenu("Focus on Player Board")]
    public void FocusOnPlayerBoard()
    {
        FocusOn(PlayerBoardFocus);
    }

    [ContextMenu("Focus on Enemy Board")]
    public void FocusOnEnemyBoard()
    {
        FocusOn(EnemyBoardFocus);
    }

    [ContextMenu("Focus on Game")]
    public void FocusOnGame()
    {
        FocusOn(GameFocus);
    }

    public void Toggle3d()
    {
        var controller = CameraController.Main;

        if (controller != null)
            controller.Toggle3d();
    }

    void FocusOn(CameraFocus focus)
    {
        var controller = CameraController.Main;

        if (controller != null)
            controller.FocusOn(focus);
    }
}
