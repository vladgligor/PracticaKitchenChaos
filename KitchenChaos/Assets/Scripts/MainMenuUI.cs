using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
	[SerializeField] private Button quitButton;

	private void Awake()
	{
		playButton.onClick.AddListener(() =>
		{
			Loader.Load(Loader.Scene.Gameplay);
		});
		
		quitButton.onClick.AddListener(() =>
		{
			Application.Quit();
		});
		Time.timeScale = 1.0f;
	}

	private void PlayClick()
	{

	}
}
