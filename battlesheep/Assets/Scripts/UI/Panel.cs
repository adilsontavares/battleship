using Tween.Animation;
using Tween.Animation.Ease;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Panel : MonoBehaviour 
{
	private CanvasGroup _canvasGroup;

	private Transform _child; 
	private bool _hidden = false;

	public bool StartsHidden = true;

	void Awake()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
		_child = transform.GetChild(0);
	}

	void Start()
	{
		if (StartsHidden)
		{
			_hidden = false;
			Hide(false);
		}
		else
		{
			_hidden = true;
			Show(true);
		}
	}

	public void Hide(bool animated)
	{
		if (_hidden)
			return;
		_hidden = true;

		_canvasGroup.interactable = false;
		_canvasGroup.blocksRaycasts = false;
		
		if (!animated)
		{
			_canvasGroup.alpha = 0;
			_child.localScale = Vector3.zero;
		}
		else 
		{
			var ease = new EaseBackOut();

			this.CreateAnimation<EaseLinear>(0.4f, 0, "Visibility", updateCallback: delegate(AnimationBehaviour anim, float time)
			{
				_canvasGroup.alpha = time;
				_child.transform.localScale = Vector3.one * ease.ConvertTime(time);
			},
			inverse: true,
			single: true);
		}
	}

	public void Hide()
	{
		Hide(true);
	}

	public void Show(bool animated)
	{
		if (!_hidden)
			return;
		_hidden = false;

		_canvasGroup.interactable = true;
		_canvasGroup.blocksRaycasts = true;

		if (!animated)
		{
			_canvasGroup.alpha = 1;
			_child.localScale = Vector3.one;
		}
		else 
		{
			var ease = new EaseBackOut();

			this.CreateAnimation<EaseLinear>(0.4f, 0, "Visibility", updateCallback: delegate(AnimationBehaviour anim, float time)
			{
				_canvasGroup.alpha = time;
				_child.transform.localScale = Vector3.one * ease.ConvertTime(time);
			},
			single: true);
		}
	}

	public void Show()
	{
		Show(true);
	}
}
