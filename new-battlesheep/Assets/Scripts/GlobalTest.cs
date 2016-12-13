using UnityEngine;
using System.Collections;

public class GlobalTest : MonoBehaviour
{
    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), new GUIContent("Start placement!")))
            ShipPlacer.Main.StartPlacement();
    }
}
