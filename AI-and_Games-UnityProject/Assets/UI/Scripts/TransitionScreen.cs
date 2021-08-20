using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TransitionScreen : ScreenBase
{
	[SerializeField] Image transitionImage;
	[SerializeField] TextMeshProUGUI transitionText;
	bool isTransitioning;
	public override void Initialize()
	{
	}

	public override bool ListenToInput()
	{
		return isTransitioning;
	}
	public void SetTransitionText(string text)
	{
		transitionText.text = text;
	}
	public void FadeIn(float duration)
	{
		transitionImage.color = Color.black;
		StartCoroutine(FadeCR(duration, -1));
	}
	public void FadeOut(float duration)
	{
		Color initialColor = Color.black;
		initialColor.a = 0;
		transitionImage.color = initialColor;
		StartCoroutine(FadeCR(duration, 1));
	}

	IEnumerator FadeCR(float duration, int sign)
	{
		isTransitioning = true;
		float delta = Time.deltaTime / duration;
		delta *= sign;
		Color c = transitionImage.color;
		do
		{
			c.a = Mathf.Clamp(c.a + delta, 0, 1);
			transitionImage.color = c;
			yield return new WaitForEndOfFrame();
		} while(c.a > 0 && c.a < 1);
		isTransitioning = false;
	}
}
