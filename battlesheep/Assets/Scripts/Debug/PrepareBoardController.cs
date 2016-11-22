using UnityEngine;

public class PrepareBoardController : MonoBehaviour 
{
	[SerializeField]
	private BoardItem _item;

	public BoardPlacer Placer;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			if (Placer.IsPlacing)
				Placer.CancelPlacement();
			else
				Placer.BeginPlacement(_item); 
		}
	}
}
