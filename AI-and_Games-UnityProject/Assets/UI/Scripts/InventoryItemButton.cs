using Inventorys;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

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
	Action OnItemHovered;
	Action OnItemClicked;
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Mouse Entered Inventory Item button");
		backgroundImg.color = hoverColor;
		OnItemHovered?.Invoke();
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

	public void Initalize(InventoryItem item, Action onItemHovered = null, Action onItemClicked = null)
	{
		OnItemHovered = onItemHovered;
		OnItemClicked = onItemClicked;

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

	public void OnItemButtonClicked()
	{
		OnItemClicked?.Invoke();
	}
}
