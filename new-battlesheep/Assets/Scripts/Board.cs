using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    private List<Ship> _ships = new List<Ship>();
    public Ship[] Ships { get { return _ships.ToArray(); } }

    public bool DrawsGizmos = true;
    public Color GizmosColor = Color.green;

    public GameObject SlotPrefab;

    public float ItemSize = 1f;
    public float Spacing = 0.15f;
    public int Size = 10;

    void Start()
    {
        CreateSlots();
    }

    void CreateSlots()
    {
        for (int i = 0; i < Size; ++i)
        {
            for (int j = 0; j < Size; ++j)
            {
                var gameObject = (GameObject)Instantiate(SlotPrefab, this.transform);
                var transform = gameObject.transform;

                transform.rotation = this.transform.rotation;
                transform.position = PositionForIndex(new Index(i, j));
            }
        }
    }

    public Ship ItemAt(Index index)
    {
        foreach (var item in _ships)
            if (item.ContainsIndex(index))
                return item;
                
        return null;
    }

    public bool Contains(Ship item)
    {
        return _ships.Contains(item);
    }

    public void Place(Ship item)
    {
        if (Contains(item))
            return;

        _ships.Add(item);
    }

    public void Move(Ship item, Index index)
    {
        if (!Contains(item))
            return;

        item.Index = index;
    }

    public bool IsValid()
    {
        for (int i = 0; i < _ships.Count; ++i)
        {
            var item1 = _ships[i];

            for (int j = i + 1; j < _ships.Count; ++j)
            {
                var item2 = _ships[j];

                if (item1.Intersects(item2))
                    return false;
            }
        }

        return true;
    }

    public void OnDrawGizmos()
    {
        if (!DrawsGizmos)
            return;

        Gizmos.color = GizmosColor;

        var height = 0.2f;

        var boardSize = Vector3.one * GetDimension();
        boardSize.y = height;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boardSize);
        Gizmos.color = GizmosColor.WithAlpha(0.3f);

        var slotSize = Vector3.one * ItemSize;
        slotSize.y = height;

        Gizmos.matrix = Matrix4x4.identity;

        for (int i = 0; i < Size; ++i)
        {
            for (int j = 0; j < Size; ++j)
            {
                var position = PositionForIndex(new Index(i, j));

                Gizmos.matrix = Matrix4x4.TRS(position, transform.rotation, Vector3.one);
                Gizmos.DrawCube(Vector3.zero, slotSize);
            }
        }
    }

    public float GetDimension()
    {
        return (ItemSize * Size) + (Spacing * (Size - 1));
    }

    public Vector3 PositionForIndex(Index index)
    {
        var offsetX = GetDimension() * -0.5f + ItemSize * 0.5f;
        var offsetY = GetDimension() * -0.5f + ItemSize * 0.5f;

        var deltaX = ItemSize + Spacing;
        var deltaY = ItemSize + Spacing;

        return transform.position + transform.rotation * new Vector3(offsetX + deltaX * index.Column, 0, offsetY + deltaY * index.Line);
    }
}
