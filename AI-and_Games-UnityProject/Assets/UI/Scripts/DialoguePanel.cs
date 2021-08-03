using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class DialoguePanel : HudScreenPanel
{
	[SerializeField] Image icon;
	[SerializeField] TMPro.TextMeshProUGUI characterName;
	[SerializeField] TMPro.TextMeshProUGUI dialogueText;
	[SerializeField] GameObject Options;
	[SerializeField] UIConfigs uiConfigs;

	private string currentText
	{
		get
		{
			if (currentTexts == null || currentTextIndex < 0 || currentTextIndex >= currentTexts.Count)
			{
				return null;
			}
			return currentTexts[currentTextIndex];
		}
	}

	private List<string> currentTexts;
	private int currentTextIndex;
	Coroutine setTextCR;
	private bool isTrading;
	public override bool ListenToInput()
	{
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
		if (currentText != null && currentTextIndex < currentTexts.Count)
		{
			if (setTextCR != null)
			{
				StopCoroutine(setTextCR);
				dialogueText.maxVisibleCharacters = currentText.Length;
				setTextCR = null;
				if (currentTextIndex == currentTexts.Count - 1)
				{
					Options.SetActive(isTrading);
				}
				currentTextIndex++;
			}
			else
			{
				SetDialogueForText(true);
			}
			return true;
		}
		else
		{
			gameObject.SetActive(false);
			return false;
		}
	}


	public void InitializeAndStartDialogue(Sprite iconSprite, string charName, List<string> texts, bool animated = true, bool isTrading = false)
	{
		if (texts == null && texts.Count <= 0)
		{
			Debug.Log("Given dialogue text list is null or empty.");
			return;
		}

		currentTexts = texts;
		currentTextIndex = 0;
		this.isTrading = isTrading;

		icon.sprite = iconSprite;
		icon.gameObject.SetActive(iconSprite);

		SetTMPText(characterName, charName);
		SetDialogueForText(animated);
	}

	private void SetDialogueForText(bool animated)
	{
		if (animated)
		{
			// if (setTextCR != null)
			// {
			// 	StopCoroutine(setTextCR);
			// 	setTextCR = null;
			// 	SetDialogueQuick(currentTexts[currentTextIndex]);
			// }
			// else
			// {
			setTextCR = StartCoroutine(SetDialogueCR(currentText));
			// }
		}
		else
		{
			SetDialogueQuick(currentText);
		}
	}

	private void SetDialogueQuick(string text)
	{
		if (setTextCR != null)
		{
			StopCoroutine(setTextCR);
			setTextCR = null;
		}

		SetTMPText(dialogueText, text);
		currentTextIndex++;
		if (IsShowingLastDialogueLine())
		{
			Options.SetActive(isTrading);
		}
	}

	private IEnumerator SetDialogueCR(string text)
	{
		Options.SetActive(false);

		dialogueText.text = currentText;
		dialogueText.maxVisibleCharacters = 0;

		foreach (char c in text)
		{
			dialogueText.maxVisibleCharacters++;
			yield return new WaitForSeconds(1f / uiConfigs.dialogueSpeedCPS);
		}
		if (IsShowingLastDialogueLine())
		{
			Options.SetActive(isTrading);
		}
		setTextCR = null;
		currentTextIndex++;
	}

	private void SetTMPText(TMPro.TextMeshProUGUI tmptext, string text)
	{
		tmptext.gameObject.SetActive(!string.IsNullOrEmpty(text));
		tmptext.text = string.IsNullOrEmpty(text) ? string.Empty : text;
	}

	private bool IsShowingLastDialogueLine()
	{
		return currentTextIndex == currentTexts.Count - 1;
	}
}
