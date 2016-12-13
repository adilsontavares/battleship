using UnityEngine;

public class Ship : MonoBehaviour
{
    public Sprite ShipSprite;

    public Board Board;

    public Index Index;
    public Index IndexEnd { get { return Direction == ShipDirection.Horizontal ? new Index(Index.Line, Index.Column + Size) : new Index(Index.Line + Size, Index.Column); } }

    public ShipDirection Direction;
    
    [Range(2, 5)]
    public int Size = 2;

    public Color GizmosColor = Color.red.WithAlpha(0.5f);
    public bool ShowGizmos = true;

    public bool IsPlaced = false;

    public Index[] GetIndexes()
    {
        var indexes = new Index[Size];

        for (int i = 0; i < Size; ++i)
        {
            if (Direction == ShipDirection.Horizontal)
                indexes[i] = new Index(Index.Line, Index.Column + i);
            else
                indexes[i] = new Index(Index.Line + i, Index.Column);
        }

        return indexes;
    }

    public bool ContainsIndex(Index index)
    {
        if (Direction == ShipDirection.Horizontal)
            return index.Line == this.Index.Line && index.Column >= this.Index.Column && index.Column < (this.Index.Column + Size);

        return index.Column == this.Index.Column && index.Line >= this.Index.Line && index.Line < (this.Index.Line + Size);
    }

    public bool Intersects(Ship item)
    {
        if (item.IndexEnd.Line < Index.Line) return false;
        if (item.IndexEnd.Column < Index.Column) return false;
        if (item.Index.Line > IndexEnd.Line) return false;
        if (item.Index.Column > IndexEnd.Column) return false;

        return true;
    }

    public void Rotate()
    {
        if (Direction == ShipDirection.Horizontal)
            Direction = ShipDirection.Vertical;
        else
            Direction = ShipDirection.Horizontal;
    }

    void OnDrawGizmos()
    {
        if (!ShowGizmos || Board == null)
            return;

        Gizmos.color = GizmosColor;

        foreach (var index in GetIndexes())
        {
            var position = Board.PositionForIndex(index);

            Gizmos.matrix = Matrix4x4.TRS(position, Board.transform.rotation, Vector3.one);
            Gizmos.DrawCube(Vector3.zero, Vector3.one * Board.ItemSize);
        }
    }

    void OnValidate()
    {
        UpdateTransform();
    }

    [ContextMenu("Update transform")]
    public void UpdateTransform()
    {
        if (Board == null)
            return;

        var position = Board.PositionForShip(this);
        var rotation = Board.RotationForShip(this);

        transform.Reset();
        transform.position = position;
        transform.rotation = Board.transform.rotation * rotation;
    }
}
