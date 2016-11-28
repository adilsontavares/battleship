using UnityEngine;
using UnityEngine.UI;

public class MessageBox : Panel 
{
	[SerializeField]
	private Text _title;

	[SerializeField]
	private Text _description;

	[SerializeField]
	private Button _button;

	public delegate void ButtonClickCallback();
	public ButtonClickCallback OnButtonClick;

	void Awake()
	{
		_button.onClick.AddListener(DidClickButton);
	}

	void DidClickButton()
	{
		if (OnButtonClick != null)
			OnButtonClick();

		Destroy(gameObject);
	}

	public void SetTitle(string title)
	{
		_title.text = title;
	}

	public void SetDescription(string description)
	{
		_description.text = description;
	}

	public void SetButtonTitle(string title)
	{
		_button.GetComponent<Text>().text = title;
	}
}
