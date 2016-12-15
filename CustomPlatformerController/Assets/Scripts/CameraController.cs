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

    private bool isStarted = false;

    public void AddPlayer(GameObject player)
    {
        this.player = player;

        thisTransfrom = this.transform;

        currentPos = thisTransfrom.position;
        playerTransfrom = player.transform;
        playerScript = player.GetComponent<Player>();

        isStarted = true;
    }
	
	// Update is called once per frame
	void LateUpdate ()
	{
        if (!isStarted) return;

		tragetPos = new Vector3(playerTransfrom.position.x + cameraOffset * playerScript.playerInfo.direction, playerTransfrom.position.y, thisTransfrom.position.z);
		currentPos.x = Mathf.SmoothDamp(currentPos.x, tragetPos.x, ref smoothPos.x, horizontalMoveSpeed);
		currentPos.y = Mathf.SmoothDamp(currentPos.y, tragetPos.y, ref smoothPos.y, verticleMoveSpeed);
		thisTransfrom.position = currentPos;
	}
}
