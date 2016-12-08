using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
	public Camera playerCamera;
	public GameObject interactableIcon;
	public Text saidField;
	public Button[] awnserButtons = new Button[4];

	public GameObject dialogPannel;

	private bool isActive = false;
	private Transform interactableTransform;

	private void Awake()
	{
		interactableTransform = interactableIcon.transform;
	}

	private void LateUpdate()
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

	public void SetDialog(string[] textInfo)
	{
		saidField.text = textInfo[0];
		if (textInfo.Length > 1)
		{
			awnserButtons[0].transform.GetChild(0).GetComponent<Text>().text = textInfo[1];
			awnserButtons[1].transform.GetChild(0).GetComponent<Text>().text = textInfo[2];
			awnserButtons[2].transform.GetChild(0).GetComponent<Text>().text = textInfo[3];
			awnserButtons[3].transform.GetChild(0).GetComponent<Text>().text = textInfo[4];
		}
		else
		{
		}

		dialogPannel.SetActive(true);

	}

	public void RemoveDialog()
	{
		dialogPannel.SetActive(false);
	}
}
