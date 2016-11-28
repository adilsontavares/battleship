using UnityEngine;
using System.Collections;

public class WorldBoard : MonoBehaviour 
{
	public bool DrawsGizmos = true;
	public Color GizmosColor = Color.green;

	public float ItemSize = 1f;
	public float Spacing = 0.15f;
	public int Size = 10;

	public void OnDrawGizmos()
	{
		if (!DrawsGizmos)
			return;
		
		Gizmos.color = GizmosColor;

		var height = 0.2f;

		var boardSize = Vector3.one * GetDimension();
		boardSize.y = height;

		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
		Gizmos.DrawWireCube(transform.position, boardSize);
		Gizmos.color = GizmosColor.WithAlpha(0.3f);

		var slotSize = Vector3.one * ItemSize;
		slotSize.y = height;

		Gizmos.matrix = Matrix4x4.identity;

		for (int i = 0; i < Size; ++i)
		{
			for (int j = 0; j < Size; ++j)
			{
				var position = PositionForIndex(new Index(i, j));
				Gizmos.DrawCube(position, slotSize);
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
