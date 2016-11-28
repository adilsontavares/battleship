using UnityEngine;

public class LiveForever : MonoBehaviour 
{
	void Start()
	{
		DontDestroyOnLoad(gameObject);
	}
}
