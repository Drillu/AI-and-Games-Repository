using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
	[SerializeField] AudioClip suspense;
	[SerializeField] AudioClip action;
	// Start is called before the first frame update
	void Start()
	{

	}

	public void StartTesting()
	{
		AudioManager.Instance.PlayMusicOnLayer(AudioManager.MusicLayer.Primary, suspense);
		AudioManager.Instance.PlayMusicOnLayer(AudioManager.MusicLayer.Secondary, action);
		AudioManager.Instance.SetMusicLayerTrackVolumn(AudioManager.MusicLayer.Secondary, 0);
	}

	public void ActionStart()
	{
		AudioManager.Instance.FadeinMusicOnLayer(AudioManager.MusicLayer.Secondary);
	}

	public void StopAction()
	{
		AudioManager.Instance.FadeoutMusicOnLayer(AudioManager.MusicLayer.Secondary);
	}

	public void UpdateOverallVolume(float v)
	{
		AudioManager.Instance.SetAllTracksVolume(v);
	}
	public void UpdateMusicVolume(float v)
	{
		AudioManager.Instance.SetMusicTracksVolume(v);
	}
	public void UpdateSFXVolume(float v)
	{
		AudioManager.Instance.SetSFXVolume(v);
	}
}
