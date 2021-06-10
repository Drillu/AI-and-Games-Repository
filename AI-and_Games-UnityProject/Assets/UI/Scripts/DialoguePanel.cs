using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialoguePanel : HudScreenPanel
{
	[SerializeField] Image icon;
	[SerializeField] TMPro.TextMeshProUGUI characterName;
	[SerializeField] TMPro.TextMeshProUGUI dialogueText;
	[SerializeField] UIConfigs uiConfigs;

	private string currentText;
	Coroutine setTextCR;

	public void OnCancelPressed()
	{
		if (currentText != null)
		{
			if (setTextCR != null)
			{
				StopCoroutine(setTextCR);
			}
			dialogueText.text = currentText;
			currentText = null;
		}
		else
		{

		}
	}
	public void SetDialogue(Sprite iconSprite, string charName, string text, bool animated = true)
	{
		currentText = text;
		if (animated)
		{
			setTextCR = StartCoroutine(SetDialogueCR(iconSprite, charName, text));
		}
		else
		{
			SetDialogueQuick(iconSprite, charName, text);
		}
	}

	private void SetDialogueQuick(Sprite iconSprite, string charName, string text)
	{
		if (iconSprite)
		{
			icon.sprite = iconSprite;
		}
		SetTMPText(characterName, charName);
		SetTMPText(dialogueText, text);
		currentText = null;
	}

	private void SetTMPText(TMPro.TextMeshProUGUI tmptext, string text)
	{
		tmptext.text = string.IsNullOrEmpty(text) ? string.Empty : text;
	}

	private IEnumerator SetDialogueCR(Sprite iconSprite, string charName, string text)
	{
		icon.sprite = iconSprite;
		characterName.text = charName;
		dialogueText.text = string.Empty;
		foreach (char c in text)
		{
			dialogueText.text += c;
			yield return new WaitForSeconds(1f / uiConfigs.dialogueSpeedCPS);
		}
		setTextCR = null;
		currentText = null;
	}


}
