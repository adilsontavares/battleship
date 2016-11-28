using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Main {  get { return ((GameObject)GameObject.FindGameObjectWithTag("Main Board")).GetComponent<Board>(); } }
    
    public GameObject WaterPrefab;
    public GameObject ItemGroundPrefab;

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

    private List<BoardItem> _items;
    public List<BoardItem> Items { get { return _items; } }

    public float Width { get { return (_size * _itemSize) + (_size - 1f) * _spacing; } }

    public float Height { get { return (_size * _itemSize) + (_size - 1f) * _spacing; } }

    void Awake()
    {
        _items = new List<BoardItem>();
    }

    void Start()
    {
        Debug.Assert(WaterPrefab != null, "WaterPrefab cannot be null");
        Debug.Assert(ItemGroundPrefab != null, "ItemGroudPrefab cannot be null");

        _waterParent = (new GameObject("Water")).transform;
        _waterParent.parent = this.transform;
        _waterParent.Reset();

        Rebuild();
    }

    void Clear()
    {
        if (_items == null)
            return; 

        for (int i = 0; i < _items.Count; ++i)
            Destroy(_items[i].gameObject);
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
                var water = Instantiate(WaterPrefab);
                var position = PositionForIndex(new Index(i, j));

                water.transform.parent = _waterParent;
                water.transform.Reset();

                water.transform.localPosition = position + Vector3.down * _waterOffset;
            }
        }
    }

    public BoardItem ItemAtIndex(Index index)
    {
        foreach (var item in _items)
        {
            if (item.ContainsIndex(index))
                return item;
        }

        return null;
    }

    public bool ContainsItemAt(Index index)
    {
        return ItemAtIndex(index) != null;
    }

    public bool CanPlaceItem(BoardItem item, Index index)
    {
        foreach (var temp in _items)
        {
            foreach (var cur in temp.GetIndexes())
            {
                foreach (var ind in item.GetIndexes())
                {
                    if (cur == ind)
                        return false;
                }
            }
        }

        return true;
    }

    public bool PlaceItem(BoardItem item, Index index)
    {
        if (!CanPlaceItem(item, index))
            return false;

        item.Index = index; 
        _items.Add(item);

        return true;
    }

    public Index IndexForPosition(Vector3 position)
    {
        var start = new Vector3(-Width * 0.5f + _itemSize * 0.5f, 0, -Height * 0.5f + _itemSize * 0.5f);
        var end = new Vector3(Width * 0.5f - _itemSize * 0.5f, 0, Height * 0.5f - _itemSize * 0.5f);
        var diff = end - start;
        var pos = position - start;

        Vector3 result;
        result.x = Mathf.Clamp01(pos.x / diff.x);
        result.z = Mathf.Clamp01(pos.z / diff.z);

        return new Index
        (
            Mathf.RoundToInt(result.x * (_size - 1f)),
            Mathf.RoundToInt(result.z * (_size - 1f))
        );
    }

    public Vector3 PositionForIndex(Index index, BoardItemDirection direction, bool odd)
    {
        var position = PositionForIndex(index);

        if (odd)
        {
            if (direction == BoardItemDirection.Horizontal)
                position.x += _itemSize * 0.5f + Spacing * 0.5f;
            else if (direction == BoardItemDirection.Vertical)
                position.z += _itemSize * 0.5f + Spacing * 0.5f;
        }

        return position;
    }

    public Vector3 PositionForIndex(Index index)
    {
        var offsetX = Width * -0.5f;
        var offsetZ = Height * -0.5f;

        var position = new Vector3();
        position.x = offsetX + (_itemSize + _spacing) * index.I + _itemSize * 0.5f;
        position.z = offsetZ + (_itemSize + _spacing) * index.J + _itemSize * 0.5f;

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
        Gizmos.color = Color.blue.WithAlpha(0.3f);

        var slotScale = Vector3.one * _itemSize;
        slotScale.y = 0.01f;

        // Draws slots
        for (int i = 0; i < _size; ++i)
        {
            for (int j = 0; j < _size; ++j)
            {
                var position = PositionForIndex(new Index(i, j));
                Gizmos.DrawCube(position, slotScale);
            }
        }
    }
}