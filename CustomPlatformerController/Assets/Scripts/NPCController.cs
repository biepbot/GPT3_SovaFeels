using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NPCController : MonoBehaviour
{
	public GameObject canvas;
	private CanvasScript canvasScript;

	public string badEndResponse;
	public string goodEndResponse;

	public List<Dialog> dialogList;
	private int currentDialog = 0;


	public void Awake()
	{
		canvasScript = canvas.GetComponent<CanvasScript>();
	}

	public void Talk()
	{
		string[] dialog = new string[5];
		dialog[0] = dialogList[currentDialog].initialDialog;
		dialogList[currentDialog].answers.CopyTo(dialog, 1);
		canvasScript.SetDialog(dialog);
	}

	public void RespondGood()
	{
		string[] dialog = new string[1];
		dialog[0] = goodEndResponse;
		canvasScript.SetDialog(dialog);
	}

	[System.Serializable]
	public class Dialog
	{
		public string initialDialog;
		public string[] answers; // !!! Must be 4 big !!!
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
