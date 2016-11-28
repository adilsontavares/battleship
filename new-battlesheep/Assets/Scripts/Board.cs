using UnityEngine;
using System.Collections.Generic;

public class Board 
{
    private List<BoardItem> _items = new List<BoardItem>();
    public BoardItem[] Items { get { return _items.ToArray(); } }

    public BoardItem ItemAt(Index index)
    {
        foreach (var item in _items)
            if (item.ContainsIndex(index))
                return item;
                
        return null;
    }

    public bool Contains(BoardItem item)
    {
        return _items.Contains(item);
    }

    public void Place(BoardItem item)
    {
        if (Contains(item))
            return;

        _items.Add(item);
    }

    public void Move(BoardItem item, Index index)
    {
        if (!Contains(item))
            return;

        item.Index = index;
    }

    public bool IsValid()
    {
        for (int i = 0; i < _items.Count; ++i)
        {
            var item1 = _items[i];

            for (int j = i + 1; j < _items.Count; ++j)
            {
                var item2 = _items[j];

                if (item1.Intersects(item2))
                    return false;
            }
        }

        return true;
    }
}
