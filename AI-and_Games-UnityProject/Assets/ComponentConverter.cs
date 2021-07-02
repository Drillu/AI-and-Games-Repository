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
		foreach (GameObject table in kitchen.transform)
		{
			if (table.name.StartsWith("Table"))
			{
				GameObject ntable = Instantiate(newTable, kitchen.transform);
				ntable.transform.position = table.transform.position;
				ntable.transform.rotation = table.transform.rotation;
				DestroyImmediate(table);
			}
		}
	}
}
