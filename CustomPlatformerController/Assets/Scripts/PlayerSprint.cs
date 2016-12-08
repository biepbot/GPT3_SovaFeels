using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class PlayerSprint : MonoBehaviour
{
	public float moveSpeed = 2f;
	public float jumpingHeight = 0.2f;
	public float gravity = -0.5f;
	public float smoothTime = 0.1f;
	public float dashSpeed = 10f;
	public float sprintTime = 0f;
	public float maxSprintTime = 2f;
	public float staminaRegenSpeed = 0.25f;

	private Vector3 velocity;
	private float velocityTarget;
	private float smoothVelocity;
	private bool sprinting = false;

	private PlayerController playerController;

	void Awake()
	{
		playerController = GetComponent<PlayerController>();
	}

	void Update()
	{
		if (playerController.collInfo.below || playerController.collInfo.above) velocity.y = 0;
		int inputX = 0;

		if (Input.GetKey(KeyCode.LeftArrow)) inputX -= 1;
		if (Input.GetKey(KeyCode.RightArrow)) inputX += 1;
		if (Input.GetKey(KeyCode.UpArrow) && playerController.collInfo.below) velocity.y = jumpingHeight;



		if (Input.GetKey(KeyCode.DownArrow) && sprintTime > 0 && (sprintTime > maxSprintTime / 3 || sprinting) && inputX != 0)
		{
			sprinting = true;
			sprintTime -= Time.deltaTime;
			velocityTarget = inputX * dashSpeed * Time.deltaTime;
		}
		else
		{
			sprinting = false;
			if (sprintTime < maxSprintTime) sprintTime += staminaRegenSpeed * Time.deltaTime;
			velocityTarget = inputX * moveSpeed * Time.deltaTime;
		}

		velocity.x = Mathf.SmoothDamp(velocity.x, velocityTarget, ref smoothVelocity, smoothTime);

		if (!playerController.collInfo.below) velocity.y += gravity * Time.deltaTime;

		playerController.Move(velocity);
	}
}
