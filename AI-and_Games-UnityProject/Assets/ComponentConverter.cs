using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

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

	[MenuItem("Converter/Convert Cafeteria Table")]
	public static void ConvertCafeteriaTable()
	{
		GameObject Cafeteria = GameObject.Find("Cafeteriaa");
		GameObject newTable = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GamePlay/SceneObjects/CafeteriaTable.prefab");
		foreach (Transform table in Cafeteria.transform)
		{
			if (table.name.Trim().ToLower().StartsWith("cafeteria_table") && !table.GetComponent<CollectibleSpawner>())
			{
				GameObject ntable = (GameObject)PrefabUtility.InstantiatePrefab(newTable, Cafeteria.transform);
				ntable.transform.position = table.position;
				ntable.transform.rotation = table.rotation;
				DestroyImmediate(table.gameObject);
			}
		}
	}


	[MenuItem("Converter/Convert Library")]
	public static void ConvertLibrary()
	{
		List<GameObject> libraries = GameObject.FindObjectsOfType<GameObject>().ToList().FindAll(go => go.name.Trim().ToLower().StartsWith("library"));
		GameObject newlib = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GamePlay/Collectibles/Library.prefab");
		foreach (GameObject lib in libraries)
		{
			GameObject nlib = (GameObject)PrefabUtility.InstantiatePrefab(newlib, lib.transform.parent);
			nlib.transform.position = lib.transform.position;
			nlib.transform.rotation = lib.transform.rotation;
			DestroyImmediate(lib);
		}
	}
}
