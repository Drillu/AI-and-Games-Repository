using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public enum MusicLayer
	{
		Primary,
		Secondary
	}

	[SerializeField] List<AudioSource> primaryTracks;
	[SerializeField] List<AudioSource> secondaryTracks;
	[SerializeField] List<AudioSource> sfxTracks;
	[SerializeField] AudioConfig config;
	float FinalMusicTrackVolume { get { return config.masterVolume * config.musicTracskVolume; } }
	float FinalSFXTrackVolume { get { return config.masterVolume * config.sfxTrackVolume; } }
	Dictionary<MusicLayer, List<AudioSource>> layerTracks;
	int primaryActiveTrackIndex = 0;
	int secondaryActiveTrackIndex = 0;
	int activeSfxTrackIndex = 0;
	public static AudioManager Instance
	{
		get; set;
	}
	private void Awake()
	{
		if (Instance && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(this);

			layerTracks = new Dictionary<MusicLayer, List<AudioSource>>();
			layerTracks.Add(MusicLayer.Primary, primaryTracks);
			layerTracks.Add(MusicLayer.Secondary, secondaryTracks);
			SetAllTracksVolumeBasedOnConfig();
		}
	}
	#region  play audio functions
	public void PlayMusicOnLayer(MusicLayer layer, AudioClip clip, bool loop = true, bool fadeTransition = false)
	{
		if (fadeTransition)
		{
			PlayMusicOnLayerWithFadeTransition(layer, clip, loop);
		}
		else
		{
			PlayMusicOnLayerQuick(layer, clip, loop);
		}
	}

	void PlayMusicOnLayerQuick(MusicLayer layer, AudioClip clip, bool loop)
	{
		switch (layer)
		{
			case MusicLayer.Primary:
				primaryTracks[primaryActiveTrackIndex].clip = clip;
				primaryTracks[primaryActiveTrackIndex].loop = loop;
				primaryTracks[primaryActiveTrackIndex].Play();
				break;
			case MusicLayer.Secondary:
				secondaryTracks[secondaryActiveTrackIndex].clip = clip;
				secondaryTracks[secondaryActiveTrackIndex].loop = loop;
				secondaryTracks[secondaryActiveTrackIndex].Play();
				break;
			default:
				break;
		}
	}

	void PlayMusicOnLayerWithFadeTransition(MusicLayer layer, AudioClip clip, bool loop)
	{
		switch (layer)
		{
			case MusicLayer.Primary:

				if (!primaryTracks[primaryActiveTrackIndex].isPlaying)
				{
					primaryTracks[primaryActiveTrackIndex].clip = clip;
					primaryTracks[primaryActiveTrackIndex].loop = loop;
					primaryTracks[primaryActiveTrackIndex].Play();
				}
				else
				{
					StartCoroutine(FadeoutMusicCR(layer, primaryTracks[primaryActiveTrackIndex]));
					primaryActiveTrackIndex = primaryActiveTrackIndex + 1 >= primaryTracks.Count ? 0 : primaryActiveTrackIndex + 1;
					AudioSource source = primaryTracks[primaryActiveTrackIndex];
					source.clip = clip;
					source.loop = loop;
					StartCoroutine(FadeinMusicCR(layer, source));
				}
				break;
			case MusicLayer.Secondary:
				if (!secondaryTracks[secondaryActiveTrackIndex].isPlaying)
				{
					secondaryTracks[secondaryActiveTrackIndex].clip = clip;
					secondaryTracks[secondaryActiveTrackIndex].loop = loop;
					secondaryTracks[secondaryActiveTrackIndex].Play();
				}
				else
				{
					StartCoroutine(FadeoutMusicCR(layer, secondaryTracks[secondaryActiveTrackIndex]));
					secondaryActiveTrackIndex = secondaryActiveTrackIndex + 1 >= secondaryTracks.Count ? 0 : secondaryActiveTrackIndex + 1;
					AudioSource source = secondaryTracks[secondaryActiveTrackIndex];
					source.clip = clip;
					source.loop = loop;
					StartCoroutine(FadeinMusicCR(layer, source));
				}
				break;
			default:
				break;
		}
	}

	public void FadeinMusicOnLayer(MusicLayer layer)
	{
		AudioSource asource;
		if (layer == MusicLayer.Primary)
		{
			asource = primaryTracks[primaryActiveTrackIndex];
		}
		else
		{
			asource = secondaryTracks[secondaryActiveTrackIndex];
		}
		StartCoroutine(FadeinMusicCR(layer, asource));
	}

	public void FadeoutMusicOnLayer(MusicLayer layer)
	{
		AudioSource asource;
		if (layer == MusicLayer.Primary)
		{
			asource = primaryTracks[primaryActiveTrackIndex];
		}
		else
		{
			asource = secondaryTracks[secondaryActiveTrackIndex];
		}
		StartCoroutine(FadeoutMusicCR(layer, asource, false));
	}

	public void PlaySFX(AudioClip clip)
	{
		AudioSource currentSfxSource = sfxTracks[activeSfxTrackIndex];
		if (currentSfxSource.isPlaying)
		{
			activeSfxTrackIndex = activeSfxTrackIndex + 1 >= sfxTracks.Count ? 0 : activeSfxTrackIndex + 1;
			currentSfxSource = sfxTracks[activeSfxTrackIndex];
		}
		currentSfxSource.clip = clip;
		currentSfxSource.Play();
	}

	public void PlaySFXAtPosition(AudioClip clip, Vector3 position)
	{
		AudioSource.PlayClipAtPoint(clip, position, config.masterVolume * config.sfxTrackVolume);
	}

	IEnumerator FadeinMusicCR(MusicLayer layer, AudioSource audioSource)
	{
		float finalVolumn = GetLayerVolumn(layer);

		if (config.musicFadeTransitionDuration > 0)
		{
			audioSource.volume = 0;
			float delta = finalVolumn * Time.deltaTime / config.musicFadeTransitionDuration;
			while (audioSource.volume < finalVolumn)
			{
				audioSource.volume += delta;
				yield return new WaitForEndOfFrame();
			}
		}
		else
		{
			Debug.LogError("Music Fade Duration is 0.");
			audioSource.volume = finalVolumn;
		}
	}
	IEnumerator FadeoutMusicCR(MusicLayer layer, AudioSource audioSource, bool stopAfterFadeout = true)
	{
		float finalVolumn = GetLayerVolumn(layer);

		if (config.musicFadeTransitionDuration > 0)
		{
			audioSource.volume = finalVolumn;
			float delta = finalVolumn * Time.deltaTime / config.musicFadeTransitionDuration;
			while (audioSource.volume > 0)
			{
				audioSource.volume -= delta;
				yield return new WaitForEndOfFrame();
			}
		}
		else
		{
			Debug.LogError("Music Fade Duration is 0.");
			audioSource.volume = 0;
		}
		if (stopAfterFadeout)
		{
			audioSource.Stop();
		}
	}
	#endregion

	#region Update Audio Volume functions
	private void SetAllTracksVolumeBasedOnConfig()
	{
		foreach (AudioSource audioSource in primaryTracks)
		{
			audioSource.volume = FinalMusicTrackVolume;
		}
		foreach (AudioSource audioSource in secondaryTracks)
		{
			audioSource.volume = FinalMusicTrackVolume;
		}
		foreach (AudioSource audioSource in sfxTracks)
		{
			audioSource.volume = FinalSFXTrackVolume;
		}
	}

	public void SetSFXVolume(float volume, bool updateConfigAsset = true)
	{
		volume = Mathf.Clamp(volume, 0f, 1f);
		if (updateConfigAsset)
		{
			config.sfxTrackVolume = volume;
		}
		foreach (AudioSource track in sfxTracks)
		{
			track.volume = config.masterVolume * volume;
		}
	}

	public void SetMusicTracksVolume(float volume, bool updateConfigAsset = true)
	{
		volume = Mathf.Clamp(volume, 0f, 1f);
		if (updateConfigAsset)
		{
			config.musicTracskVolume = volume;
		}

		foreach (AudioSource audioSource in primaryTracks)
		{
			audioSource.volume = config.masterVolume * volume * config.primaryLayerMusicTrackVolumn;
		}
		foreach (AudioSource audioSource in secondaryTracks)
		{
			audioSource.volume = config.masterVolume * volume * config.secondaryLayerMusicTrackVolumn;
		}
	}

	public void SetAllTracksVolume(float volume, bool updateConfigAsset = true)
	{
		volume = Mathf.Clamp(volume, 0f, 1f);
		if (updateConfigAsset)
		{
			config.masterVolume = volume;
		}

		SetAllTracksVolumeBasedOnConfig();
	}


	// 
	public void SetMusicLayerTrackVolumn(MusicLayer layer, float volume, bool UpdateConfigAsset = true)
	{
		volume = Mathf.Clamp(volume, 0f, 1f);
		if (UpdateConfigAsset)
		{
			UpdateLayerVolumnConfig(layer, volume);
		}
		List<AudioSource> tracks = layerTracks[layer];
		foreach (AudioSource track in tracks)
		{
			track.volume = volume;
		}
	}
	void UpdateLayerVolumnConfig(MusicLayer layer, float volumn)
	{
		if (layer == MusicLayer.Primary)
		{
			config.primaryLayerMusicTrackVolumn = volumn;
		}
		else
		{
			config.secondaryLayerMusicTrackVolumn = volumn;
		}
	}

	float GetLayerVolumn(MusicLayer layer)
	{
		if (layer == MusicLayer.Primary)
		{
			return config.primaryLayerMusicTrackVolumn;
		}
		else
		{
			return config.secondaryLayerMusicTrackVolumn;
		}
	}
	#endregion
}

