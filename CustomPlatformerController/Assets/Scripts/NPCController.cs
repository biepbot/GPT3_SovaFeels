﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NPCController : MonoBehaviour
{
	public GameObject canvas;
	private CanvasScript canvasScript;

	public Dialog[] dialogList;
	public int currentDialogNumber = 0;
	private Option lastOption;
	public string MontyFeedBack;

	public void Awake()
	{
		canvasScript = canvas.GetComponent<CanvasScript>();
	}

	public void Talk()
	{
		if (currentDialogNumber == -1)
		{
			canvasScript.DisableAllButtons();
			canvasScript.SetDialogBox(lastOption.charaterResponse);
		}
		else
		{
			canvasScript.EnableAllButtons();
			Dialog currentDialog = dialogList[currentDialogNumber];
			canvasScript.SetDialogBox(currentDialog.initialDialog);

			for (int i = 0; i < 4; i++)
			{
				canvasScript.answerButtons[i].onClick.RemoveAllListeners();
				LoadDialog(i, currentDialog);

				//if (currentDialog.answers[i].answerType != SolutionTypes.Confrontation)
				//{
				//	canvasScript.answerButtons[i].transform.GetChild(0).GetComponent<Text>().text = currentDialog.answers[i].buttonText;
					
				//	canvasScript.answerButtons[i].onClick.AddListener(delegate { RespondBad(currentDialog.answers[i]); });
				//}
				//else
				//{
				//	canvasScript.answerButtons[i].transform.GetChild(0).GetComponent<Text>().text = currentDialog.answers[i].buttonText;
				//	canvasScript.answerButtons[i].onClick.AddListener(delegate { RespondGood(currentDialog.answers[i]); });
				//}
			}
			
		}

	}

	private void LoadDialog(int i, Dialog currentDialog)
	{
		if (currentDialog.answers[i].answerType != SolutionTypes.Confrontation)
		{
			canvasScript.answerButtons[i].transform.GetChild(0).GetComponent<Text>().text = currentDialog.answers[i].buttonText;

			canvasScript.answerButtons[i].onClick.AddListener(delegate { RespondBad(currentDialog.answers[i]); });
		}
		else
		{
			canvasScript.answerButtons[i].transform.GetChild(0).GetComponent<Text>().text = currentDialog.answers[i].buttonText;
			canvasScript.answerButtons[i].onClick.AddListener(delegate { RespondGood(currentDialog.answers[i]); });
		}
	}

	public void RespondGood(Option selectedResponse)
	{
		currentDialogNumber++;

		if (currentDialogNumber >= dialogList.Length)
		{
			lastOption = selectedResponse;
			currentDialogNumber = -1;
		}

		Talk();
	}

	public void RespondBad(Option selectedResponse)
	{
		lastOption = selectedResponse;
		currentDialogNumber = -1;
		Talk();

	}

	[System.Serializable]
	public class Dialog
	{
		public string initialDialog;
		public Option[] answers;
	}

	[System.Serializable]
	public class Option
	{
		public string buttonText;
		public SolutionTypes answerType;
		public string charaterResponse;
	}

	public enum SolutionTypes
	{
		Fight,
		Flight,
		Confrontation
	}
}
