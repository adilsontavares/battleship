using UnityEngine;
using System.Collections;

public class BoardItem 
{
    public Index Index;
    public Index IndexEnd { get { return Direction == ShipDirection.Horizontal ? new Index(Index.Line, Index.Column + Size) : new Index(Index.Line + Size, Index.Column); } }

    public ShipDirection Direction;

    public int Size;

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

    public bool Intersects(BoardItem item)
    {
        if (item.IndexEnd.Line < Index.Line) return false;
        if (item.IndexEnd.Column < Index.Column) return false;
        if (item.Index.Line > IndexEnd.Line) return false;
        if (item.Index.Column > IndexEnd.Column) return false;

        return true;
    }
}
