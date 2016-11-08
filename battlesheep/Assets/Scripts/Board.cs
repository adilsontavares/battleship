using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject _defaultItem;

    [SerializeField]
    private float _itemSize = 1;

    [SerializeField]
    [Range(1, 15)]
    private int _size = 10;

    [SerializeField]
    private float _separator = 0.2f;

    private BoardItem[,] _items;

    public float Width { get { return (_size * _itemSize) + (_size - 1f) * _separator; } }

    public float Height { get { return (_size * _itemSize) + (_size - 1f) * _separator; } }

    void Awake()
    {
        if (_defaultItem != null && _defaultItem.GetComponent<BoardItem>() == null)
            Debug.LogError("Default item has no component BoardItem");

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

    void CreateItems()
    {
        _items = new BoardItem[_size, _size];

        if (_defaultItem == null)
            return;

        for (int i = 0; i < _items.GetLength(0); ++i)
        {
            for (int j = 0; j < _items.GetLength(1); ++j)
            {
                var item = Instantiate(_defaultItem);
                var transform = item.transform;

                transform.name = string.Format("Board Item [{0}, {1}]", i, j);
                transform.parent = this.transform;
                transform.Reset();

                var boardItem = item.GetComponent<BoardItem>();
                boardItem.Board = this;

                _items[i, j] = boardItem;
            }
        }
    }

    void Rebuild()
    {
        Clear();
        CreateItems();
    }

    void OnDrawGizmos()
    {
        var offsetX = Width * -0.5f;
        var offsetZ = Height * -0.5f;
        var offset = new Vector3(offsetX, 0, offsetZ);
        var delta = new Vector3(_itemSize + _separator, 0, _itemSize + _separator);

        Gizmos.color = Color.black.WithAlpha(0.5f);

        // Vertical lines
        for (int i = 0; i <= _size; ++i)
        {
            // Begin
            var from1 = offset + new Vector3(delta.x * i, 0, 0);
            var to1 = offset  + new Vector3(delta.x * i, 0, Width);

            // End
            var from2 = from1 + Vector3.left * _separator;
            var to2 = to1  + Vector3.left * _separator;

            if (i != _size)
                Gizmos.DrawLine(from1, to1);

            if (i != 0)
                Gizmos.DrawLine(from2, to2);
        }

        // Horizontal lines
        for (int i = 0; i <= _size; ++i)
        {
            // Begin
            var from1 = offset + new Vector3(0, 0, delta.z * i);
            var to1 = offset  + new Vector3(Height, 0, delta.z * i);

            // End
            var from2 = from1 + Vector3.back * _separator;
            var to2 = to1  + Vector3.back * _separator;

            if (i != _size)
                Gizmos.DrawLine(from1, to1);

            if (i != 0)
                Gizmos.DrawLine(from2, to2);
        }

        Gizmos.color = Color.blue.WithAlpha(0.3f);

        for (int i = 0; i < _size; ++i)
        {
            for (int j = 0; j < _size; ++j)
            {
                var position = offset + 
                    Vector3.right * (delta.x * i + _itemSize * 0.5f) +
                    Vector3.forward * (delta.z * j + _itemSize * 0.5f);

                var scale = Vector3.one * _itemSize;
                scale.y = 0.01f; 

                Gizmos.DrawCube(position, scale);
            }
        }
    }
}