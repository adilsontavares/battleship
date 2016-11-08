using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Main {  get { return FindObjectOfType<Board>(); } }

    [SerializeField]
    private GameObject _waterPrefab;

    [SerializeField]
    private bool _createWater = true;

    [SerializeField]
    private float _waterOffset = 0.5f;

    [SerializeField]
    private float _itemSize = 1;
    public float ItemSize {  get { return _itemSize; } }

    [SerializeField]
    [Range(1, 15)]
    private int _size = 10;
    public int Size {  get { return _size; } }

    [SerializeField]
    private float _spacing = 0.2f;
    public float Spacing {  get { return _spacing; } }

    private Transform _waterParent;

    private BoardItem[,] _items;

    public float Width { get { return (_size * _itemSize) + (_size - 1f) * _spacing; } }

    public float Height { get { return (_size * _itemSize) + (_size - 1f) * _spacing; } }

    void Start()
    {
        Debug.Assert(_waterPrefab != null, "WaterPrefab cannot be null");

        _waterParent = (new GameObject("Water")).transform;
        _waterParent.parent = this.transform;
        _waterParent.Reset();

        Rebuild();
    }

    void Clear()
    {
        if (_items == null)
            return; 

        for (int i = 0; i < _items.GetLength(0); ++i)
        {
            for (int j = 0; j < _items.GetLength(1); ++j)
            {
                var item = _items[i, j];
                if (item != null)
                {
                    Destroy(item.gameObject);
                    _items[i, j] = null;
                }
            }
        }
    }

    void ClearWater()
    {
        for (int i = 0; i < _waterParent.childCount; ++i)
            Destroy(_waterParent.GetChild(i).gameObject);
    }

    void CreateWater()
    {
        if (!_createWater)
            return;

        for (int i = 0; i < _size; ++i)
        {
            for (int j = 0; j < _size; ++j)
            {
                var water = Instantiate(_waterPrefab);
                var position = PositionForIndex(i, j);

                water.transform.parent = _waterParent;
                water.transform.Reset();

                water.transform.localPosition = position + Vector3.down * _waterOffset;
            }
        }
    }

    public void IndexForPosition(Vector3 position, out int i, out int j)
    {
        var start = new Vector3(-Width * 0.5f + _itemSize * 0.5f, 0, -Height * 0.5f + _itemSize * 0.5f);
        var end = new Vector3(Width * 0.5f - _itemSize * 0.5f, 0, Height * 0.5f - _itemSize * 0.5f);
        var diff = end - start;
        var pos = position - start;

        Vector3 result;
        result.x = Mathf.Clamp01(pos.x / diff.x);
        result.z = Mathf.Clamp01(pos.z / diff.z);

        i = Mathf.RoundToInt(result.x * (_size - 1f));
        j = Mathf.RoundToInt(result.z * (_size - 1f));
    }

    public Vector3 PositionForIndex(int i, int j, BoardItemDirection direction, bool odd)
    {
        var position = PositionForIndex(i, j);

        if (odd)
        {
            if (direction == BoardItemDirection.Horizontal)
                position.x += _itemSize * 0.5f + Spacing * 0.5f;
            else if (direction == BoardItemDirection.Vertical)
                position.z += _itemSize * 0.5f + Spacing * 0.5f;
        }

        return position;
    }

    public Vector3 PositionForIndex(int i, int j)
    {
        var offsetX = Width * -0.5f;
        var offsetZ = Height * -0.5f;

        var position = new Vector3();
        position.x = offsetX + (_itemSize + _spacing) * i + _itemSize * 0.5f;
        position.z = offsetZ + (_itemSize + _spacing) * j + _itemSize * 0.5f;

        return position;
    }

    void Rebuild()
    {
        Clear();

        ClearWater();
        CreateWater();
    }

    void OnValidate()
    {
        var collider = transform.GetOrAddComponent<BoxCollider>();
        collider.size = new Vector3(Width, 0.001f, Height);
    }

    void OnDrawGizmos()
    {
        //var offsetX = Width * -0.5f;
        //var offsetZ = Height * -0.5f;
        //var offset = new Vector3(offsetX, 0, offsetZ);
        //var delta = new Vector3(_itemSize + _separator, 0, _itemSize + _separator);

        //Gizmos.color = Color.black.WithAlpha(0.5f);

        //// Draws vertical lines
        //for (int i = 0; i <= _size; ++i)
        //{
        //    // Begin
        //    var from1 = offset + new Vector3(delta.x * i, 0, 0);
        //    var to1 = offset  + new Vector3(delta.x * i, 0, Width);

        //    // End
        //    var from2 = from1 + Vector3.left * _separator;
        //    var to2 = to1  + Vector3.left * _separator;

        //    if (i != _size)
        //        Gizmos.DrawLine(from1, to1);

        //    if (i != 0)
        //        Gizmos.DrawLine(from2, to2);
        //}

        //// Draws horizontal lines
        //for (int i = 0; i <= _size; ++i)
        //{
        //    // Begin
        //    var from1 = offset + new Vector3(0, 0, delta.z * i);
        //    var to1 = offset  + new Vector3(Height, 0, delta.z * i);

        //    // End
        //    var from2 = from1 + Vector3.back * _separator;
        //    var to2 = to1  + Vector3.back * _separator;

        //    if (i != _size)
        //        Gizmos.DrawLine(from1, to1);

        //    if (i != 0)
        //        Gizmos.DrawLine(from2, to2);
        //}

        Gizmos.color = Color.blue.WithAlpha(0.3f);

        var slotScale = Vector3.one * _itemSize;
        slotScale.y = 0.01f;

        // Draws slots
        for (int i = 0; i < _size; ++i)
        {
            for (int j = 0; j < _size; ++j)
            {
                var position = PositionForIndex(i, j);
                Gizmos.DrawCube(position, slotScale);
            }
        }
    }
}