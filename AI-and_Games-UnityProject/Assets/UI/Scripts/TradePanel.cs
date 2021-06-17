using Inventorys;
using UnityEngine;

public class TradePanel : HudScreenPanel
{
	[SerializeField] InventoryPanel firstInventoryPanel;
	[SerializeField] InventoryPanel secondInventoryPanel;

	public override bool ListenToInput()
	{
		return true;
	}

	public void OnCancelPressed()
	{

	}

	public void SetupTradePanel(Inventory first, Inventory second)
	{
		firstInventoryPanel.SetupInventoryPanel(first);
		firstInventoryPanel.gameObject.SetActive(true);
		secondInventoryPanel.SetupInventoryPanel(second);
		secondInventoryPanel.gameObject.SetActive(true);
	}
}