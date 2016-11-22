using Tween.Animation;
using Tween.Animation.Ease;
using UnityEngine;

public class BoardItemGround : MonoBehaviour 
{
	BoardItem _item;
	public GameObject Prefab;

	Transform[] _grounds;

	private Material _material = null;
	public Material Material
	{
		set 
		{ 
			if (_material != value)
			{
				_material = value;
				UpdateGroundsMaterials();
			}
		}
	}

	void Awake()
	{
		_item = GetComponent<BoardItem>();

		Debug.Assert(_item != null, "BoardItem cannot be null.");

		Build(false);
		Hide(false);	
	}

	public void Show()
	{
		Show(true);
	}

	public void Show(bool animated)
	{
		if (animated)
		{
			this.CreateAnimation<EaseCircOut>(0.3f, updateCallback: OnAnimShow);
		}
		else
		{
			foreach (var ground in _grounds)
				ground.localScale = Vector3.one;
		}
	}

	public void Hide()
	{
		Hide(true);
	}

	public void Hide(bool animated)
	{
		if (animated)
		{
			this.CreateAnimation<EaseCircOut>(0.3f, updateCallback: OnAnimHide);
		}
		else
		{
			foreach (var ground in _grounds)
				ground.localScale = Vector3.zero;
		}
	}

	void UpdateGroundsMaterials()
	{
		if (_grounds == null || _material == null)
			return;

		foreach (var ground in _grounds)
			UpdateGroundMaterials(ground);
	}

	void UpdateGroundMaterials(Transform transform)
	{
		for (int i = 0; i < transform.childCount; ++i)
			UpdateGroundMaterials(transform.GetChild(i));

		var renderer = transform.GetComponent<MeshRenderer>();
		if (renderer != null)
			renderer.material = _material;
	}

	void Clear()
	{
		if (_grounds == null || _grounds.Length == 0)
			return;

		for (int i = 0; i < _grounds.Length; ++i)
			Destroy(_grounds[i].gameObject);

		_grounds = null;
	}

	void Create()
	{
		int size = _item.Size;
		_grounds = new Transform[size];

		for (int i = 0; i < size; ++i)
		{
			CreateAtIndex(i);
			UpdateGroundMaterials(_grounds[i]);
		}
	}

	void CreateAtIndex(int index)
	{
		var ground = (GameObject)Instantiate(Prefab);
		var transform = ground.transform;

		var board = _item.Board;

		transform.parent = this.transform;
		transform.Reset();
		transform.localPosition = Vector3.forward * (board.Spacing + board.ItemSize) * index; 

		_grounds[index] = transform;
	}

	public void Build()
	{
		Build(true);
	}

	public void Build(bool animated)
	{
		Destroy(false);
		Create();
	}

	void OnAnimShow(AnimationBehaviour anim, float time)
	{
		foreach (var ground in _grounds)
			ground.localScale = Vector3.one * time;
	}

	void OnAnimHide(AnimationBehaviour anim, float time)
	{
		OnAnimShow(anim, 1f - time);
	}

	public void Destroy()
	{
		Destroy(true);
	}

	public void Destroy(bool animated)
	{
		if (!animated)
		{
			Clear();
		}
		else if (_grounds != null) 
		{
			this.CreateAnimation<EaseBackIn>(0.3f, 0f, inverse: true, updateCallback: OnAnimDestroy, doneCallback: OnAnimDestroyFinished);
		}
	}

	void OnAnimDestroy(AnimationBehaviour anim, float time)
	{
		foreach (var ground in _grounds)
			ground.localScale = Vector3.one * time;
	}

	void OnAnimDestroyFinished(AnimationBehaviour anim)
	{
		Clear();
	}
}
