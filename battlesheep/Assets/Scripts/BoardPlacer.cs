using Tween.Animation;
using Tween.Animation.Ease;
using UnityEngine;

public class BoardPlacer : MonoBehaviour
{
    public Board Board;

    [SerializeField]
    public BoardItem _item;

    [SerializeField]
    private float _moveItemSpeed = 15f;

    bool _placing = false;
    Vector3 _moveDestin;
    Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;

        Debug.Assert(Board != null, "BoardPlacer must have a Board to manage.");

        if (_item != null)
            BeginPlacement(_item);    
    }

    public void BeginPlacement(BoardItem item)
    {
        if (_placing)
            return;
        _placing = true;

        _item = item;
    }

    public void CancelPlacement()
    {
        if (!_placing)
            return;
        _placing = false;

        _item = null;
    }

    void Update()
    {
        if (_placing)
            UpdatePlacement();
    }

    void UpdatePlacement()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Board")))
        {
            int i;
            int j;

            var offset = Vector3.zero;// Vector3.left * _item.Width * 0.5f + Vector3.back * _item.Height * 0.5f;
            var point = hit.point + offset;

            Board.IndexForPosition(point, out i, out j);
            var position = Board.PositionForIndex(i, j);

            if (_moveDestin != position)
                _moveDestin = position;
        }

        _item.transform.position = Vector3.Lerp(_item.transform.position, _moveDestin, Time.deltaTime * _moveItemSpeed);
    }

    void OnGUI()
    {
        if (!_placing)
            return;

        int i;
        int j;

        Board.IndexForPosition(_item.transform.position, out i, out j);

        GUILayout.Button(string.Format("INDEX = {0}, {1}", i, j));
    }
}
