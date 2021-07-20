using UnityEngine;
public static class GameObjectExtension
{
	public static T AddOrGetComponent<T>(this GameObject gameObject) where T : MonoBehaviour
	{
		T component = gameObject.GetComponent<T>();
		if (!component)
		{
			component = gameObject.AddComponent<T>();
		}
		return component;
	}
}