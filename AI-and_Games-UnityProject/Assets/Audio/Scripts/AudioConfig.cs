using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Config", menuName = "Configs/AudioConfig")]
public class AudioConfig : ScriptableObject
{
    [Range(0, 1)]
    public float musicTrackVolumn;
    [Range(0, 1)]
    public float primaryLayerMusicTrackVolumn;
    [Range(0, 1)]
    public float secondaryLayerMusicTrackVolumn;
    [Range(0, 1)]
    public float sfxTrackVolumn;
    [Range(0, 3)]
    public float musicFadeTransitionDuration;
}
