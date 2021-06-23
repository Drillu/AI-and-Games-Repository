using UnityEngine;
using Inventorys;

[CreateAssetMenu]
public class CollectibleItem : ScriptableObject
{
	public InventoryItemType type;
	public string ItemName
	{
		get
		{
			string result = this.type.ToString();
			int len = result.Length;
			for (int i = 0; i < len; i++)
			{
				char c = result[i];
				if (char.IsUpper(c))
				{
					result = result.Insert(i, " ");
					i++;
				}
			}
			return result;
		}
	}
	public string description;
    public Sprite itemIcon;

}
