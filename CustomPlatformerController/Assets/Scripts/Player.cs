using UnityEngine;
using Assets.Scripts.Base;
using UnityStandardAssets.CrossPlatformInput;

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
        if (MobileHelper.OnTouchDevice)
        {
            Instantiate(Resources.Load("MobileSingleStickControl"));
        }
	}

	void Update()
	{
        CheckYBump();

        CheckXMovement();

        CheckJump();

        //Enable gravity to the player, if the player is not standing on a platform
		if (!playerController.collInfo.below) velocity.y -= gravity * Time.deltaTime;

        //Move the player with the adjusted speeds
        playerController.Move(velocity);

		if (Input.GetAxis("Interact") == 1) playerController.Interact();

	}

    /// <summary>
    /// Checks if the player is holding any buttons to jump
    /// </summary>
    private void CheckJump()
    {
        //If the player is pressing the positive vertical axis input, or jump input, the character needs to jump
        bool doJump = Input.GetAxis("Vertical") == 1 || Input.GetAxis("Jump") == 1 || CrossPlatformInputManager.GetButtonDown("Jump");

        //If the player needs to jump, and is able to, jump.
        if (doJump && playerController.collInfo.below) velocity.y = jumpingHeight;
    }

    /// <summary>
    /// Checks if the player is holding any buttons to move left / right
    /// </summary>
    private void CheckXMovement()
    {
        int inputX;
        if (MobileHelper.OnTouchDevice)
        {
            inputX = (int)CrossPlatformInputManager.GetAxis("Horizontal");
        }
        else
        {
            inputX = (int)Input.GetAxis("Horizontal");
        }

        //The movement over the X axis is defined by the input of the Horizontal axis
        playerInfo.direction = inputX;
        velocityTarget = inputX * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, velocityTarget, ref smoothVelocity, smoothTime);
    }

    /// <summary>
    /// Checks if the player bumps into anything on the y-axis, and adjusts their speed if this is the case
    /// </summary>
    private void CheckYBump()
    {
        //If the player is colliding above or below, remove his speed
        if (playerController.collInfo.below || playerController.collInfo.above) velocity.y = 0;
    }

	public void PlatformMove(Vector3 velocity)
	{
		playerController.PlatformMove(velocity);
	}

	public struct PlayerInfo
	{
		public int direction; // Left: -1; Right: 1;
	}
}
