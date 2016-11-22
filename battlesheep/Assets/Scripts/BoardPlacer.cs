using UnityEngine;

public class BoardPlacer : MonoBehaviour
{
    public delegate void BeginPlaceItemCallback(BoardItem item);
    public delegate void PlaceItemCallback(BoardItem item);
    public delegate void CancelPlaceItemCallback(BoardItem item);

    public BeginPlaceItemCallback OnBeginPlaceItem;
    public PlaceItemCallback OnPlaceItem;
    public CancelPlaceItemCallback OnCancelPlaceItem; 

    public static BoardPlacer Main { get { return Object.FindObjectOfType<BoardPlacer>(); } }

    private Board _board;

    [SerializeField]
    private BoardItem _item;
    
    private Transform _itemParent;

    public KeyCode RotateItemKey = KeyCode.R;
    public string PlaceItemButton = "Fire1";

    public Material GroundValidMaterial;
    public Material GroundInvalidMaterial;

    bool _placing = false;
    public bool IsPlacing { get { return _placing; } } 

    Camera _mainCamera;

    void Start()
    {
        _board = Board.Main;
        _mainCamera = Camera.main;

        CreateItemParent();

        Debug.Assert(_board != null, "BoardPlacer must have a Board to manage.");

        if (_item != null)
            BeginPlacement(_item);
    }

    void CreateItemParent()
    {
        var go = new GameObject("Current");
        
        _itemParent = go.transform;
        _itemParent.parent = this.transform;
        _itemParent.Reset();
    }

    public void BeginPlacement(BoardItem item)
    {
        if (_placing)
            return;

        if (item == null)
        {
            Debug.LogWarning("Cannot place a BoardItem that is null.");
            return;
        }

        _placing = true;

        _item = item;
        _item.transform.parent = _itemParent;

        _item.Ground.Show();

        if (OnBeginPlaceItem != null)
            OnBeginPlaceItem(_item);
    }

    public void CancelPlacement()
    {
        if (!_placing)
            return;
        _placing = false;
        
        Destroy(_item.gameObject);

        FinishPlacement(false);
    }

    void Place()
    {
        if (!_placing)
            return;

        if (_board.CanPlaceItem(_item, _item.Index))
        {
            _placing = false;
            _board.PlaceItem(_item, _item.Index);

            FinishPlacement(true);
        }
    }

    void FinishPlacement(bool success)
    {
        var item = _item;

        _item.Ground.Hide();
        _item = null;

        if (success && OnPlaceItem != null)
            OnPlaceItem(item);
        else if (!success && OnCancelPlaceItem != null)
            OnCancelPlaceItem(item); 
    } 

    void Update()
    {
        if (_placing)
        {
            if (Input.GetKeyDown(RotateItemKey))
                _item.Rotate();

            UpdatePlacement();

            if (Input.GetButtonDown(PlaceItemButton))
                Place();
        }
    }

    void UpdatePlacement()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Board")))
        {
            Index index;

            var offset = Vector3.zero;            
            var point = hit.point + offset;

            index = _board.IndexForPosition(point);

            if (_item.Direction == BoardItemDirection.Horizontal)
                index.J = Mathf.Clamp(index.J, _item.Size - 1, _board.Size);
            else if (_item.Direction == BoardItemDirection.Vertical)
                index.I = Mathf.Clamp(index.I, 0, _board.Size - _item.Size);

            if (index != _item.Index)
            {
                _item.Index = index;

                var validPosition = _board.CanPlaceItem(_item, index);
                var groundMaterial = validPosition ? GroundValidMaterial : GroundInvalidMaterial;

                _item.Ground.Material = groundMaterial;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        if (_item != null)
        {
            Gizmos.color = Color.green;

            var position = _board.PositionForIndex(_item.Index);
            Gizmos.DrawCube(position, Vector3.one);
        }
    }
}
