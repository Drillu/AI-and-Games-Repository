using System.Collections.Generic;
using Inventorys;
using UnityEngine;

public static class PrisonerInventorys
{
	public static List<Inventory> GetPrisonerInventory()
	{
		List<Inventory> inventorys = new List<Inventory>();
		inventorys.Add(FirstPrisonerInventory());
		inventorys.Add(SecondInventory());
		inventorys.Add(ThirdInventory());
		inventorys.Add(FourthInventory());
		inventorys.Add(FifthInventory());
		return inventorys;
	}

	static Inventory FirstPrisonerInventory()
	{
		Inventory i = new Inventory();
		InventoryItem item = new InventoryItem();

		item.item = ScriptableObject.CreateInstance<CollectibleItem>();
		item.item.type = InventoryItemType.ScrewDriver;
		item.amount = 2;

		item.Recipe = new List<RecipeItem>();
		item.Recipe.Add(new RecipeItem(InventoryItemType.Spoon, 1));
		i.AddItem(item);
		return i;
	}

	static Inventory SecondInventory()
	{
		Inventory i = new Inventory();
		InventoryItem item = new InventoryItem();

		item.item = ScriptableObject.CreateInstance<CollectibleItem>();
		item.item.type = InventoryItemType.ScrewDriver;
		item.amount = 2;

		item.Recipe = new List<RecipeItem>();
		item.Recipe.Add(new RecipeItem(InventoryItemType.ToiletPaper, 1));
		i.AddItem(item);
		return i;
	}

	static Inventory ThirdInventory()
	{
		Inventory i = new Inventory();
		InventoryItem item = new InventoryItem();

		item.item = ScriptableObject.CreateInstance<CollectibleItem>();
		item.item.type = InventoryItemType.ToiletPaper;
		item.amount = 2;

		item.Recipe = new List<RecipeItem>();
		item.Recipe.Add(new RecipeItem(InventoryItemType.Soap, 1));
		item.Recipe.Add(new RecipeItem(InventoryItemType.Apple, 2));
		i.AddItem(item);
		return i;
	}

	static Inventory FourthInventory()
	{
		Inventory i = new Inventory();
		InventoryItem item = new InventoryItem();

		item.item = ScriptableObject.CreateInstance<CollectibleItem>();
		item.item.type = InventoryItemType.Spoon;
		item.amount = 2;

		item.Recipe = new List<RecipeItem>();
		item.Recipe.Add(new RecipeItem(InventoryItemType.Toothpaste, 1));
		item.Recipe.Add(new RecipeItem(InventoryItemType.Apple, 2));
		i.AddItem(item);
		return i;
	}

	static Inventory FifthInventory()
	{
		Inventory i = new Inventory();
		InventoryItem item = new InventoryItem();

		item.item = ScriptableObject.CreateInstance<CollectibleItem>();
		item.item.type = InventoryItemType.Apple;
		item.amount = 4;

		item.Recipe = new List<RecipeItem>();
		item.Recipe.Add(new RecipeItem(InventoryItemType.Book, 2));
		i.AddItem(item);

		item.item = ScriptableObject.CreateInstance<CollectibleItem>();
		item.item.type = InventoryItemType.Soap;
		item.amount = 1;

		item.Recipe = new List<RecipeItem>();
		item.Recipe.Add(new RecipeItem(InventoryItemType.Book, 4));
		i.AddItem(item);

		item.item = ScriptableObject.CreateInstance<CollectibleItem>();
		item.item.type = InventoryItemType.Toothpaste;
		item.amount = 1;

		item.Recipe = new List<RecipeItem>();
		item.Recipe.Add(new RecipeItem(InventoryItemType.Book, 4));
		i.AddItem(item);
		return i;
	}
}