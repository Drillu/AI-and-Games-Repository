using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Config", menuName = "Configs/AudioConfig")]
public class AudioConfig : ScriptableObject
{
	[Range(0, 1)]
	public float masterVolume;

	[Range(0, 1)]
	public float musicTracskVolume;

	[Range(0, 1)]
	public float sfxTrackVolume;

	[Range(0, 3)]
	public float musicFadeTransitionDuration;
	[Header("Internal config")]
	[Tooltip("These values are reference volume for each music layer. \n1. They CAN NOT be set by player.\n2. They DO NOT reflect the run time volume, they are the configuration of the maximum volume of each layer")]
	[Range(0, 1)]
	public float primaryLayerMusicTrackVolumn;
	[Tooltip("These values are reference volume for each music layer. \n1. They CAN NOT be set by player.\n2. They DO NOT reflect the run time volume, they are the configuration of the maximum volume of each layer")]
	[Range(0, 1)]
	public float secondaryLayerMusicTrackVolumn;
}
