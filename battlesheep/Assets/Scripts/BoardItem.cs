using UnityEngine;

public enum BoardItemDirection
{
    Horizontal,
    Vertical
}

public class BoardItem : MonoBehaviour
{

    Board _board { get { return Board.Main; } }

    [SerializeField]
    private BoardItemDirection _direction;
    public BoardItemDirection Direction
    {
        get { return _direction; }
        set
        {
            _direction = value;
            UpdatePiecesPosition();
        }
    }

    BoardItemPiece[] _pieces;
    public BoardItemPiece[] Pieces { get { return _pieces; } }

    public bool Odd { get { return Size % 2 == 0; } }
    public int Size { get { return _pieces.Length; } }

    public float Width { get { return (_direction == BoardItemDirection.Vertical ? _board.ItemSize : (_board.ItemSize * _pieces.Length + _board.Spacing * (_pieces.Length - 1))); } }
    public float Height { get { return (_direction == BoardItemDirection.Horizontal ? _board.ItemSize : (_board.ItemSize * _pieces.Length + _board.Spacing * (_pieces.Length - 1))); } }
    public float Length { get { return (_direction == BoardItemDirection.Horizontal ? Width : Height); } }

    void Start()
    {
        _pieces = GetComponentsInChildren<BoardItemPiece>();

        Debug.Assert(_board != null, "No board was found.");
        Debug.Assert(_pieces.Length > 0, "BoardItem must have at lease 1 piece.");

        UpdatePiecesPosition();
    }

    void UpdatePiecesPosition()
    {
        Vector3 direction;
        Vector3 offset = Vector3.zero;

        if (_direction == BoardItemDirection.Horizontal)
        {
            direction = Vector3.right;
            //offset.x -= _board.ItemSize * 0.5f + _board.Spacing * 0.5f;
        }
        else
        {
            direction = Vector3.forward;
            //offset.z -= _board.ItemSize * 0.5f + _board.Spacing * 0.5f;
        }

        for (int i = 0; i < _pieces.Length; ++i)
            _pieces[i].transform.localPosition = offset + direction * (_board.Spacing + _board.ItemSize) * i;
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