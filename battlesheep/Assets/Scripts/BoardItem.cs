using Tween.Animation;
using Tween.Animation.Ease;
using UnityEngine;

public enum BoardItemDirection
{
    Horizontal,
    Vertical
}

public class BoardItem : MonoBehaviour
{
    Board _board { get { return Board.Main; } }

    public Transform Model;
    public Index Index;

    [Range(1, 6)]
    public int Size = 2;

    [SerializeField]
    private BoardItemDirection _direction;
    public BoardItemDirection Direction
    {
        get { return _direction; }
        set
        {
            if (_direction != value)
            {
                _direction = value;
                DirectionDidChange();
            }
        }
    }

    Quaternion _fromRotation;
    Quaternion _toRotation;

    Transform[] _grounds;

    public bool Odd { get { return Size % 2 == 0; } }

    public float Width { get { return (_direction == BoardItemDirection.Vertical ? _board.ItemSize : (_board.ItemSize * Size + _board.Spacing * (Size - 1))); } }
    public float Height { get { return (_direction == BoardItemDirection.Horizontal ? _board.ItemSize : (_board.ItemSize * Size + _board.Spacing * (Size - 1))); } }
    public float Length { get { return (_direction == BoardItemDirection.Horizontal ? Width : Height); } }

    void Start()
    {
        if (Model == null)
            Model = transform.GetChild(0);

        Debug.Assert(_board != null, "No board was found.");
        Debug.Assert(Model != null, "BoardItem has no Model.");

        Model.Reset();
        UpdateModelTransform();

        transform.localRotation = GetModelRotation();

        CreateGrounds();
    }

    public Index[] GetIndexes()
    {
        var indexes = new Index[Size];

        for (int i = 0; i < indexes.Length; ++i)
        {
            if (Direction == BoardItemDirection.Horizontal)
                indexes[i] = new Index(Index.I, Index.J + i);
            else
                indexes[i] = new Index(Index.I + i, Index.J);
        }

        return indexes;
    }

    public bool ContainsIndex(Index index)
    {
        if (index.I < Index.I || index.J < Index.J)
            return false;

        if (Direction == BoardItemDirection.Horizontal)
            return index.I == Index.I && index.J < (Index.J + Size);

        if (Direction == BoardItemDirection.Vertical)
            return index.I < (Index.I + Size) && index.J == Index.J;

        return false;
    }

    void CreateGrounds()
    {
        _grounds = new Transform[Size];

        for (int i = 0; i < Size; ++i)
        {
            var ground = Instantiate(_board.ItemGroundPrefab);
            var transform = ground.transform;

            transform.parent = this.transform;
            transform.Reset();
            transform.localScale = Vector3.one * _board.ItemSize;
            transform.localPosition = Vector3.forward * (_board.ItemSize + _board.Spacing) * i;

            _grounds[i] = transform;
        }
    }

    void UpdateModelTransform()
    {
        Model.localPosition = Vector3.forward * (-_board.ItemSize * 0.5f - _board.Spacing * 0.5f + Length * 0.5f);
    }

    Quaternion GetModelRotation()
    {
        Quaternion rotation = Quaternion.identity;

        if (_direction == BoardItemDirection.Horizontal)
            rotation = Quaternion.AngleAxis(90f, Vector3.up);
        else if (_direction == BoardItemDirection.Vertical)
            rotation = Quaternion.identity;

        return rotation;
    }

    void DirectionDidChange()
    {
        _fromRotation = transform.localRotation;
        _toRotation = GetModelRotation();

        this.CreateAnimation<EaseQuintOut>(0.45f, 0f, "rotate-item", updateCallback: OnAnimRotate, single: true);
    }

    public void Rotate()
    {
        if (_direction == BoardItemDirection.Horizontal)
            Direction = BoardItemDirection.Vertical;
        else if (_direction == BoardItemDirection.Vertical)
            Direction = BoardItemDirection.Horizontal;
    }

    void OnAnimRotate(AnimationBehaviour anim, float time)
    {
        transform.localRotation = Quaternion.Lerp(_fromRotation, _toRotation, time);
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        Gizmos.color = Color.red.WithAlpha(0.4f);

        var offset = -_board.ItemSize * 0.5f + Length * 0.5f;
        var position = Vector3.zero;

        if (_direction == BoardItemDirection.Horizontal)
            position = Vector3.right * offset;
        else if (_direction == BoardItemDirection.Vertical)
            position = Vector3.forward * offset;

        Gizmos.DrawCube(position + transform.position, new Vector3(Width, 0.001f, Height));
    }
}