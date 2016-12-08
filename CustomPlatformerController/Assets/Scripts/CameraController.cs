using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject player;
	private Transform playerTransfrom;
	private Player playerScript;

	public float cameraOffset;

	public float horizontalMoveSpeed;
	public float verticleMoveSpeed;

	private Transform thisTransfrom;
	private Vector3 tragetPos;
	private Vector3 currentPos;
	private Vector3 smoothPos;


	void Awake ()
	{
		playerTransfrom = player.transform;
		playerScript = player.GetComponent<Player>();

		thisTransfrom = this.transform;
	}

	void Start()
	{
		currentPos = thisTransfrom.position;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		tragetPos = new Vector3(playerTransfrom.position.x + cameraOffset * playerScript.playerInfo.direction, playerTransfrom.position.y, thisTransfrom.position.z);
		currentPos.x = Mathf.SmoothDamp(currentPos.x, tragetPos.x, ref smoothPos.x, horizontalMoveSpeed);
		currentPos.y = Mathf.SmoothDamp(currentPos.y, tragetPos.y, ref smoothPos.y, verticleMoveSpeed);
		thisTransfrom.position = currentPos;
	}
}
