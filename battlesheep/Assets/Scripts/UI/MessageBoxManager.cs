using UnityEngine;
using System.Collections;

public class MessageBoxManager : MonoBehaviour 
{
	public static MessageBoxManager Main { get { return FindObjectOfType<MessageBoxManager>(); } }

	[SerializeField]
	private GameObject _prefab;

	private Canvas _canvas;

	void Awake()
	{
		_canvas = GetComponentInChildren<Canvas>();
	}

	public static MessageBox Show(string title, string description)
	{
		return Show(title, description, null);
	}

	public static MessageBox Show(string title, string description, string button)
	{
		var instance = (GameObject)Instantiate(Main._prefab);
		var messageBox = instance.GetComponent<MessageBox>();
		var t = instance.transform as RectTransform;

		t.SetParent(Main._canvas.transform);
		t.Reset();

		t.anchorMin = new Vector2(0, 0);
		t.anchorMax = new Vector2(1, 1);

		t.sizeDelta = Vector3.zero;

		messageBox.SetTitle(title);
		messageBox.SetDescription(description);

		if (!string.IsNullOrEmpty(button))
			messageBox.SetButtonTitle(button);

		messageBox.Show();

		return messageBox;
	}
}
