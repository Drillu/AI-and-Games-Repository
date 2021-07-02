using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ComponentConverter : MonoBehaviour
{
	[MenuItem("Converter/Convert Kitchen Table")]
	public static void ConvertTable()
	{
		GameObject kitchen = GameObject.Find("Kitchen");
		GameObject newTable = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GamePlay/SceneObjects/Table.prefab");
		foreach (Transform table in kitchen.transform)
		{
			if (table.name.Trim().ToLower().StartsWith("table") && !table.GetComponent<CollectibleSpawner>())
			{
				GameObject ntable = (GameObject)PrefabUtility.InstantiatePrefab(newTable, kitchen.transform);
				ntable.transform.position = table.position;
				ntable.transform.rotation = table.rotation;
				DestroyImmediate(table.gameObject);
			}
		}
	}
}
