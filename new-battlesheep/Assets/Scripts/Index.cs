using System;

[Serializable]
public class Index
{
	public int Line;
	public int Column;

    public static Index Invalid
    {
        get { return new Index(-1, -1); }
    }

	public Index(int line, int column)
	{
		Line = line;
		Column = column;
	}

    public static Index operator +(Index a, Index b)
    {
        return new Index(a.Line + b.Line, a.Column + b.Column);
    }

    public static Index operator -(Index a, Index b)
    {
        return new Index(a.Line - b.Line, a.Column - b.Column);
    }
}