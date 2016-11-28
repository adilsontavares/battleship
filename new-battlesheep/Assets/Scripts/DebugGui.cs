using UnityEngine;
using System.Collections;

public class DebugGui : MonoBehaviour
{
    bool _showGui = false;

    void OnGUI()
    {
        if (!_showGui)
        {
            _showGui = GUILayout.Button("DEBUG");
            return;
        }

        GUILayout.Label("# CAMERA");
        
        var cameraManager = CameraManager.Main;
        if (cameraManager != null)
        {
            if (GUILayout.Button("Move to Player Board"))
                cameraManager.FocusOnPlayerBoard();

            if (GUILayout.Button("Move to Enemy Board"))
                cameraManager.FocusOnEnemyBoard();

            if (GUILayout.Button("Move to Game"))
                cameraManager.FocusOnGame();

            if (GUILayout.Button("Toggle 3D"))
                cameraManager.Toggle3d();
        }
    }
}
