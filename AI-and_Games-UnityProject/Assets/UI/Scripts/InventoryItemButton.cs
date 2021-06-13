using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Image))]
public class InventoryItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Backgorund color")]
	[SerializeField] Color NormalColor;
	[SerializeField] Color hoverColor;
	[Header("Text color")]
	[SerializeField] Color NormalItemTextColor;
	[SerializeField] Color HoldForPlayerItemTextColor;
	[Header("Game Objects")]
	[SerializeField] TMPro.TextMeshProUGUI itemNameText;
	[SerializeField] TMPro.TextMeshProUGUI itemQuantityText;
	Image backgroundImg;

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Mouse Entered Inventory Item button");
		backgroundImg.color = hoverColor;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("Mouse Exited Inventory Item button");
		backgroundImg.color = NormalColor;
	}

	private void Awake()
	{
		backgroundImg = GetComponent<Image>();
	}

	public void Initalize(Inventorys.InventoryItem item)
	{
		if (item.isHoldForPlayer)
		{
			itemNameText.color = HoldForPlayerItemTextColor;
			itemQuantityText.color = HoldForPlayerItemTextColor;
		}
		else
		{
			itemNameText.color = NormalItemTextColor;
			itemQuantityText.color = NormalItemTextColor;
		}

		itemNameText.text = item.name;
		itemQuantityText.text = item.amount.ToString();
	}
}
