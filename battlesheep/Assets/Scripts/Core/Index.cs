public struct Index
{
    public int I;
    public int J;

    public static Index Invalid { get { return new Index(-1, -1); } }  

    public Index(int i, int j)
    {
        I = i;
        J = j;
    }
    
    public static bool operator ==(Index a, Index b)
    {
        return a.I == b.I && a.J == b.J;
    }

    public static bool operator !=(Index a, Index b)
    {
        return a.I != b.I || a.J != b.J;
    }

    public override bool Equals(object obj)
    {
        return this == (Index)obj;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
