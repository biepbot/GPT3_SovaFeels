using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MontyController : BaseController
{

	public GameObject canvas;

	public float range = 1f;
	public string defaultTalk;
	public string ignoreMontyFeedBack;
	public string fightMontyFeedBack;
	public string flightMontyFeedBack;
	public string confrontMontyFeedBack;

	public GameObject NPC;
	private NPCController NPCScript;
	private CanvasScript canvasScript;

	protected override void Awake()
	{
		base.Awake();
		if (NPC != null) NPCScript = NPC.GetComponent<NPCController>();
	}

    public void SetCanvas(CanvasScript canvas)
    {
        canvasScript = canvas;
    }

    protected override void Start()
	{
		base.Start();
		rayCaster.UpdateRayCastOrigins(InnerBounds());
	}

	void LateUpdate()
	{
		CheckForPlayer();
	}

	private void CheckForPlayer()
	{
		for (int i = 0; i < rayCaster.horizontalRayCount; i++)
		{
			Vector2 rayOffset = Vector2.up * (rayCaster.horizontalSpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayCaster.rayCastOrigins.bottomRight + rayOffset, Vector2.right, range, collisionMask);
			if (!hit) hit = Physics2D.Raycast(rayCaster.rayCastOrigins.bottomLeft + rayOffset, Vector2.right * -1, range, collisionMask);

			if (hit) GiveFeedback();
		}
	}

	private void GiveFeedback()
	{
		
		if (NPC != null)
		{
			NPCController.SolutionTypes solution = NPCScript.GetSolution();
			if (solution == NPCController.SolutionTypes.Ignore)
			{
				canvasScript.SetDialogBox(ignoreMontyFeedBack);
				NPC.SetActive(false);
			}

			if (solution == NPCController.SolutionTypes.Flight) canvasScript.SetDialogBox(flightMontyFeedBack);
			if (solution == NPCController.SolutionTypes.Fight) canvasScript.SetDialogBox(fightMontyFeedBack);
			if (solution == NPCController.SolutionTypes.Confrontation) canvasScript.SetDialogBox(confrontMontyFeedBack);
		}
		else
		{
			canvasScript.SetDialogBox(defaultTalk);
		}
		canvasScript.DisableAllButtons();
	}
}
