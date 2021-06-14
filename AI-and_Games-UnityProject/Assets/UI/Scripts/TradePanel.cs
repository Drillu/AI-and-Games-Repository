using Inventorys;
using UnityEngine;

public class TradePanel : HudScreenPanel
{
	[SerializeField] InventoryPanel firstInventoryPanel;
	[SerializeField] InventoryPanel secondInventoryPanel;

	public override bool ListenToInput()
	{
		throw new System.NotImplementedException();
	}

	public void OnCancelPressed()
	{

	}

	public void SetupTradePanel(Inventory fisrt, Inventory second)
	{
		
	}
}