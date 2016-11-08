using UnityEngine;

public class DebugInformation : MonoBehaviour
{
    bool _hidden = true;

    void OnGUI()
    {
        string content;

        if (_hidden)
        {
            content = "INFORMAÇÕES";
        }
        else
        {
            content =
                "Pressione R para rotacionar o item atual.\n" + 
                "Pressione ESPAÇO para alternar a visão da câmera.";
        }

        if (GUILayout.Button(content))
            _hidden = !_hidden;
    }
}
