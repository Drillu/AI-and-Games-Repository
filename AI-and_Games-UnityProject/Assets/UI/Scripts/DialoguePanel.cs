using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialoguePanel : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMPro.TextMeshProUGUI characterName;
    [SerializeField] TMPro.TextMeshProUGUI dialogueText;

    Coroutine setTextCR;
    public void Initialize()
    {

    }

    public void SetDialogue(Sprite iconSprite, string charName, string text, bool animated = true)
    {
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
        icon.sprite = iconSprite;
        characterName.text = charName;
        dialogueText.text = text;
    }

    private IEnumerator SetDialogueCR(Sprite iconSprite, string charName, string text)
    {
        icon.sprite = iconSprite;
        characterName.text = charName;
        dialogueText.text = text;
        yield return new WaitForEndOfFrame();
    }
	
}
