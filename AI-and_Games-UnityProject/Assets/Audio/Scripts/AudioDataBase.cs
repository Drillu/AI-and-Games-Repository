using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioDatabase")]
public class AudioDataBase : ScriptableObject
{
	[Header("Music Clips")]
	public AudioClip intro;
	public AudioClip suspension;
	public AudioClip action;
	[Header("Ingame Clips")]
	public List<AudioClip> chatClips;
	public AudioClip collectObjectClip;
	public AudioClip alarmClip;
	[Header("UI Clips")]
	public AudioClip uiHover;
	public AudioClip uiConfirm;

	public AudioClip GetIntroSceneAudioClip()
	{
		return intro;
	}
	public AudioClip GetSuspencionLayerAudioClip()
	{
		return suspension;
	}

	public AudioClip GetActionLayerAudioClip()
	{
		return action;
	}
	public AudioClip GetRandomChatSFXClip()
	{
		return chatClips[Random.Range(0, chatClips.Count)];
	}
}
