using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialoguePanel : HudScreenPanel
{
	[SerializeField] Image icon;
	[SerializeField] TMPro.TextMeshProUGUI characterName;
	[SerializeField] TMPro.TextMeshProUGUI dialogueText;
	[SerializeField] GameObject Options;
	[SerializeField] UIConfigs uiConfigs;

	private string currentText;
	Coroutine setTextCR;
	private bool isTrading;
	public override bool ListenToInput()
	{
		// if (InputManager.Instance.IsCancelButtonPressed || InputManager.Instance.IsMouseLeftButtonDown || InputManager.Instance.IsMouseRightButtonDown)
		if (InputManager.Instance.IsCancelButtonPressed)
		{
			return OnCancelPressed();
		}
		else
		{
			return true;
		}
	}
	private bool OnCancelPressed()
	{
		Debug.Log("Cancel dialogue");
		if (currentText != null)
		{
			if (setTextCR != null)
			{
				StopCoroutine(setTextCR);
			}
			dialogueText.text = currentText;
			setTextCR = null;
			currentText = null;
			Options.SetActive(isTrading);
			return true;
		}
		else
		{
			gameObject.SetActive(false);
			return false;
		}
	}


	public void SetDialogue(Sprite iconSprite, string charName, string text, bool animated = true, bool isTrading = false)
	{
		currentText = text;
		this.isTrading = isTrading;
		if (animated)
		{
			if (setTextCR != null)
			{
				StopCoroutine(setTextCR);
				setTextCR = null;
				SetDialogueQuick(iconSprite, charName, text);
			}
			else
			{
				setTextCR = StartCoroutine(SetDialogueCR(iconSprite, charName, text));
			}
		}
		else
		{
			SetDialogueQuick(iconSprite, charName, text);
		}
	}

	private void SetDialogueQuick(Sprite iconSprite, string charName, string text)
	{
		icon.sprite = iconSprite;
		icon.gameObject.SetActive(iconSprite);

		if (setTextCR != null)
		{
			StopCoroutine(setTextCR);
			setTextCR = null;
		}

		currentText = null;
		SetTMPText(characterName, charName);
		SetTMPText(dialogueText, text);
		Options.SetActive(isTrading);
	}

	private void SetTMPText(TMPro.TextMeshProUGUI tmptext, string text)
	{
		tmptext.gameObject.SetActive(!string.IsNullOrEmpty(text));
		tmptext.text = string.IsNullOrEmpty(text) ? string.Empty : text;
	}

	private IEnumerator SetDialogueCR(Sprite iconSprite, string charName, string text)
	{
		Options.SetActive(false);

		icon.sprite = iconSprite;
		icon.gameObject.SetActive(iconSprite);

		SetTMPText(characterName, charName);

		dialogueText.text = string.Empty;
		foreach (char c in text)
		{
			dialogueText.text += c;
			yield return new WaitForSeconds(1f / uiConfigs.dialogueSpeedCPS);
		}

		Options.SetActive(isTrading);
		setTextCR = null;
		currentText = null;
	}
}
