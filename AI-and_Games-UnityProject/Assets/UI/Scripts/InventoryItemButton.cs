using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Image))]
public class InventoryItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] Color NormalColor;
	[SerializeField] Color hoverColor;
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
}
