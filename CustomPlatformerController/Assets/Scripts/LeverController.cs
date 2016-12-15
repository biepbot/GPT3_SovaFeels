using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour {

	public int allowedHits = 1;
	public List<ChangingObject> objectList;

	private int currentHitCount = 0;

	public void Hit()
	{
		if(currentHitCount < allowedHits)
		{
			currentHitCount++;

			foreach (ChangingObject cObject in objectList)
			{
				if (cObject.amount != 0)
				{
					cObject.objectToChange.GetComponent<StraightMovementController>().moveSpeed += cObject.amount;
				}

				cObject.objectToChange.SetActive(cObject.enabled);
			}
		} 
	}

	[System.Serializable]
	public class ChangingObject
	{
		public GameObject objectToChange;
		public float amount = 0;
		public bool enabled = true;
	}
}
