using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
	public static OptionsUI Instance { get; private set; }
	[SerializeField] private Scrollbar soundEffectsScrolbar;
	[SerializeField] private Scrollbar musicScrolbar;
	[SerializeField] private Button cancelButton;
	[Header("Buttons")]
	[SerializeField] private Button moveUpButton;
	[SerializeField] private Button moveDownButton;
	[SerializeField] private Button moveLeftButton;
	[SerializeField] private Button moveRightButton;
	[SerializeField] private Button interactButton;
	[SerializeField] private Button interactAlternateButton;
	[SerializeField] private Button pauseButton;
	[Header("Text")]
	[SerializeField] private TextMeshProUGUI moveUpText;
	[SerializeField] private TextMeshProUGUI moveDownText;
	[SerializeField] private TextMeshProUGUI moveLeftText;
	[SerializeField] private TextMeshProUGUI moveRightText;
	[SerializeField] private TextMeshProUGUI interactText;
	[SerializeField] private TextMeshProUGUI interactAlternateText;
	[SerializeField] private TextMeshProUGUI pauseText;
	[SerializeField] private Transform pressToRebindKeyTransform;


	private void Awake()
	{
		Instance = this;
		

	}

	private void Start()
	{
		soundEffectsScrolbar.onValueChanged.AddListener((float varNormalized) =>
		{
			SoundManager.Instance.ChangeVolume(varNormalized);
		});
		musicScrolbar.onValueChanged.AddListener((float varNormalized) =>
		{
			MusicManager.Instance.ChangeVolume(varNormalized);
		});

		cancelButton.onClick.AddListener(() => Hide());

		soundEffectsScrolbar.value = SoundManager.Instance.GetVolume();
		musicScrolbar.value = MusicManager.Instance.GetVolume();


		moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Bindings.Move_up); });
		moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Bindings.Move_down); });
		moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Bindings.Move_left); });
		moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Bindings.Move_right); });
		interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Bindings.Interact); });
		interactAlternateButton.onClick.AddListener(() => { RebindBinding(GameInput.Bindings.InteractAlternate); });
		pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Bindings.Pause); });

		UpdateVisual();

		HidePressToRebindKey();
		Hide();
	}
	
	public void Show()
	{
		gameObject.SetActive(true);
	}
	public void Hide()
	{
		gameObject.SetActive(false);
		Debug.Log("Hidden");
	}

	public void UpdateVisual()
	{
		moveUpText.text = GameInput.Instance.GetBinding(GameInput.Bindings.Move_up);
		moveDownText.text = GameInput.Instance.GetBinding(GameInput.Bindings.Move_down);
		moveLeftText.text = GameInput.Instance.GetBinding(GameInput.Bindings.Move_left);
		moveRightText.text = GameInput.Instance.GetBinding(GameInput.Bindings.Move_right);
		interactText.text = GameInput.Instance.GetBinding(GameInput.Bindings.Interact);
		interactAlternateText.text = GameInput.Instance.GetBinding(GameInput.Bindings.InteractAlternate);
		pauseText.text = GameInput.Instance.GetBinding(GameInput.Bindings.Pause);
	}

	private void ShowPressToRebindKey()
	{
		pressToRebindKeyTransform.gameObject.SetActive(true);
	}

	private void HidePressToRebindKey()
	{
		pressToRebindKeyTransform.gameObject.SetActive(false);
	}

	private void RebindBinding(GameInput.Bindings binding)
	{
		ShowPressToRebindKey();
		GameInput.Instance.RebindBinding(binding, () => {
			HidePressToRebindKey();
			UpdateVisual();
		});
	}
}
