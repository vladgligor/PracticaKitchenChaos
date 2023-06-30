using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
	public static TutorialUI Instance { get; private set; }
	
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
	


	private void Awake()
	{
		Instance = this;
		

	}

	private void Start()
	{
		GameInput.Instance.OnInteractAction += Instance_OnInteractAction;
		UpdateVisual();
	}

	private void Instance_OnInteractAction(object sender, System.EventArgs e)
	{
		Hide();
		if (KitchenGameManager.Instance.IsTutorial())
		{
			KitchenGameManager.Instance.SetState(KitchenGameManager.State.WaitingToStart);
		}
	}


	public void Show()
	{
		gameObject.SetActive(true);
	}
	public void Hide()
	{
		gameObject.SetActive(false);
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

	

	
}
