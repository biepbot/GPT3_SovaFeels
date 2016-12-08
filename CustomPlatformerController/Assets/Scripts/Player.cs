using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float jumpingHeight = 15f;
	public float gravity = 30f;
	public float smoothTime = 0.15f;

	public PlayerInfo playerInfo;

	private Vector3 velocity;
	private float velocityTarget;
	private float smoothVelocity;

	private PlayerController playerController;

	void Awake()
	{
		playerController = GetComponent<PlayerController>();
	}

	void Update()
	{
		if (playerController.collInfo.below || playerController.collInfo.above) velocity.y = 0;
		int inputX = 0;

        inputX = (int)Input.GetAxis("Horizontal");
        playerInfo.direction = (int)Input.GetAxis("Horizontal");

        bool doJump = Input.GetAxis("Vertical") == 1 || Input.GetAxis("Jump") == 1;

		if (doJump && playerController.collInfo.below) velocity.y = jumpingHeight;

		velocityTarget = inputX * moveSpeed; // Time.deltaTime in PlayerController
		velocity.x = Mathf.SmoothDamp(velocity.x, velocityTarget, ref smoothVelocity, smoothTime);

		if (!playerController.collInfo.below) velocity.y -= gravity * Time.deltaTime;

		playerController.Move(velocity);

		if (Input.GetAxis("Interact") == 1) playerController.Interact();

	}

	public void PlatformMove(Vector3 velocity)
	{
		playerController.PlatformMove(velocity);
	}

	public struct PlayerInfo
	{

		public int health;
		public int damage;
		public int direction; // Left: -1; Right: 1;
	}
}
