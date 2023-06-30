using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{

	private const string PLAYER_PREFS_SOUND_EFFECTS = "sound effects";
	public static SoundManager Instance { get; private set; }
	[SerializeField] private AudioClipRefsSO audioClipRefSO;

	private float volume = 1f;

	private void Awake()
	{
		Instance = this;
		volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS);
	}

	private void Start()
	{
		DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
		DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
		CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
		Player.Instance.OnPickedSomething += Player_OnPickedSomething;
		BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
		TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
	}

	private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
	{
		TrashCounter trashCounter = sender as TrashCounter;
		PlaySound(audioClipRefSO.objectDrop, trashCounter.transform.position);
	}

	private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
	{
		BaseCounter baseCounter = sender as BaseCounter;
		PlaySound(audioClipRefSO.objectDrop, baseCounter.transform.position);
	}

	private void Player_OnPickedSomething(object sender, System.EventArgs e)
	{
		PlaySound(audioClipRefSO.objectPickup, Player.Instance.transform.position);
	}

	private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
	{
		CuttingCounter cuttingCounter = sender as CuttingCounter;
		PlaySound(audioClipRefSO.chop, cuttingCounter.transform.position);
	}

	private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
	{
		DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
		PlaySound(audioClipRefSO.deliveryFail, deliveryCounter.transform.position);
	}

	private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
	{
		DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
		PlaySound(audioClipRefSO.deliverySuccess, deliveryCounter.transform.position);
	}

	private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
	{
		PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
	}

	private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
	{
		AudioSource.PlayClipAtPoint(audioClip, position, volume);
	}

	public void PlayFootstepsSound(Vector3 position, float volume)
	{
		PlaySound(audioClipRefSO.footstep, position, volume);
	}

	public void ChangeVolume(float changeAmount)
	{
		volume = changeAmount;
		if (changeAmount > 1)
		{
			Debug.LogWarning("ChangeAmount should be between 0 and 1");
		}
		if (volume > 1)
		{
				volume = 0f;
		}
		PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS, volume);
		PlayerPrefs.Save();
	}



	public float GetVolume()
	{
		return volume;
	}
}
