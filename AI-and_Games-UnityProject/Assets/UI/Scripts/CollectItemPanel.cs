using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CollectItemPanel : HudScreenPanel
{
	[SerializeField] TMPro.TextMeshProUGUI itemTextTmp;
	[SerializeField] PlayableDirector timeline;
	private bool isTimelineCancelled;
	public override bool ListenToInput()
	{
		if (InputManager.Instance.IsCancelButtonPressed ||
			InputManager.Instance.IsMouseLeftButtonDown ||
			InputManager.Instance.IsMouseLeftButtonDown)
		{
			if (!isTimelineCancelled)
			{
				isTimelineCancelled = true;
				timeline.Stop();
				PlayTimeline(false);
			}
			return false;
		}
		else
		{
			return true;
		}
	}

	public void InitializeAndShow(string text)
	{
		itemTextTmp.text = text;
		isTimelineCancelled = false;
		PlayTimeline(true);
	}

	public void OnShowItemTrackFinished()
	{
		StartCoroutine(WaitAndClosePanel());
	}

	IEnumerator WaitAndClosePanel()
	{
		yield return new WaitForSeconds(2f);
		if (!isTimelineCancelled)
		{
			isTimelineCancelled = true;
			PlayTimeline(false);
			hudScreen.CurrentPanelCancelled(this);
		}
	}

	private void PlayTimeline(bool show)
	{
		foreach (TrackAsset track in ((TimelineAsset)timeline.playableAsset).GetRootTracks())
		{
			track.muted = show ? !track.name.Equals("Show") : !track.name.Equals("Hide");
		}
		timeline.RebuildGraph();
		timeline.playableGraph.GetRootPlayable(0).SetTime(0);
		timeline.playableGraph.GetRootPlayable(0).SetSpeed(1);
		timeline.Play();
	}
}
