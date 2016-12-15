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
    Vector2 levelCameraDimension;

    public void AddPlayer(GameObject player)
    {
        Vector2 topRightCorner = new Vector2(1, 1);
        levelCameraDimension = Camera.main.ViewportToWorldPoint(topRightCorner);

        this.player = player;

        thisTransfrom = this.transform;

        currentPos = thisTransfrom.position;
        playerTransfrom = player.transform;
        playerScript = player.GetComponent<Player>();

        isStarted = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isStarted) return;

        tragetPos = new Vector3(playerTransfrom.position.x + cameraOffset * playerScript.playerInfo.direction, playerTransfrom.position.y, thisTransfrom.position.z);
        currentPos.x = Mathf.SmoothDamp(currentPos.x, tragetPos.x, ref smoothPos.x, horizontalMoveSpeed);
        currentPos.y = Mathf.SmoothDamp(currentPos.y, tragetPos.y, ref smoothPos.y, verticleMoveSpeed);

        //Limit max and min X
        if (currentPos.x + levelCameraDimension.x + 0.5f > LevelController.maxX)
        {
            currentPos.x = LevelController.maxX - levelCameraDimension.x - 0.5f;
        }
        else if (currentPos.x - levelCameraDimension.x - 0.5f < LevelController.minX)
        {
            currentPos.x = LevelController.minX + levelCameraDimension.x + 0.5f;
        }
        thisTransfrom.position = currentPos;
    }
}
