using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{

	private const string PLAYER_PREFS_MUSIC = "music";
	public static MusicManager Instance { get; private set; }
	private float volume = 0.3f;
	private AudioSource audioSource;

	private void Awake()
	{
		Instance = this;
		audioSource = GetComponent<AudioSource>();
		volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC);

	}
	public void ChangeVolume(float changeAmount)
	{
		volume = changeAmount;
		if (changeAmount > 1)
		{
			Debug.LogWarning("ChangeAmount should be between 1 and 0");
		}
		if (volume > 1f)
		{
			volume = 0f;
		}
		audioSource.volume = volume;
		PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC, volume);
		PlayerPrefs.Save();
	}
	public float GetVolume()
	{
		return volume;
	}
}
