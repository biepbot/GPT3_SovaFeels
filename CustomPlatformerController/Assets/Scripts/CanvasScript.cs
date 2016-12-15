using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
	public Camera playerCamera;
	public GameObject interactableIcon;
	public Text saidField;
	public Button[] answerButtons = new Button[4];

	public GameObject dialogPannel;

	private bool isActive = false;
	private Transform interactableTransform;

	void Start()
	{
		interactableTransform = interactableIcon.transform;
        playerCamera = Camera.main;
    }

	void LateUpdate()
	{
		interactableIcon.SetActive(isActive);
		isActive = false;
	}

	public void SetInteractable(Vector3 worldPos)
	{
		isActive = true;
		Vector3 NewPos = playerCamera.WorldToScreenPoint(worldPos);
		interactableTransform.position = NewPos;
	}

	public void SetDialogBox(string text)
	{
		saidField.text = text;
		dialogPannel.SetActive(true);

	}

	public void DisableAllButtons()
	{
		foreach (Button b in answerButtons)
		{
			b.gameObject.SetActive(false);
		}
	}

	public void EnableAllButtons()
	{
		foreach (Button b in answerButtons)
		{
			b.gameObject.SetActive(true);
		}
	}

	public void RemoveDialog()
	{
		if (dialogPannel.activeSelf)
		{
			dialogPannel.SetActive(false);
			DisableAllButtons();
		}
	}
}
