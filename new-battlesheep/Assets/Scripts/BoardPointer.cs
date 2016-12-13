using UnityEngine;

public class BoardPointer : MonoBehaviour
{
    public static BoardPointer Main {  get { return FindObjectOfType<BoardPointer>(); } }

    public Index FindIndex(Board board)
    {
        Debug.Assert(board != null);

        var transform = board.transform;
        var plane = new Plane(transform.up, transform.position);

        var camera = CameraController.MainCamera;
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            var point = ray.GetPoint(distance);
            var diff = point - transform.position;
            var target = Quaternion.Inverse(transform.rotation) * diff;

            var position = new Vector2(target.z, target.x);
            var index = board.FindIndex(position);

            return index;
        }

        return Index.Invalid;
    }

    void OnGUI()
    {
        //    var board = Board.PlayerBoard;
        var index = FindIndex(Board.PlayerBoard);

        GUI.Box(new Rect(100, 100, 200, 40), new GUIContent(string.Format("({0}, {1})", index.Line, index.Column)));
    }
}
