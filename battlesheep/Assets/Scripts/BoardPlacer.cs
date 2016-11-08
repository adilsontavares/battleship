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

    public KeyCode RotateItemKey = KeyCode.R;

    bool _placing = false;
    int _i;
    int _j;

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
        _i = -1;
        _j = -1;
    }

    void Update()
    {
        if (_placing)
        {
            if (Input.GetKeyDown(RotateItemKey))
                _item.Rotate();

            UpdatePlacement();
        }
    }

    void UpdatePlacement()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Board")))
        {
            int i;
            int j;

            var offset = Vector3.zero;            
            var point = hit.point + offset;

            Board.IndexForPosition(point, out i, out j);

            var min = 0;
            var max = Board.Size - _item.Size;

            if (_item.Direction == BoardItemDirection.Horizontal)
                i = Mathf.Clamp(i, min, max);
            else if (_item.Direction == BoardItemDirection.Vertical)
                j = Mathf.Clamp(j, min, max);

            if (i != _i || j != _j)
            {
                _i = i;
                _j = j;

                var position = Board.PositionForIndex(i, j);
                _moveDestin = position;
            }
        }

        _item.transform.position = Vector3.Lerp(_item.transform.position, _moveDestin, Time.deltaTime * _moveItemSpeed);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        var position = Board.PositionForIndex(_i, _j);
        Gizmos.DrawCube(position, Vector3.one);
    }
}
