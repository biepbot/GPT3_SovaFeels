using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StraightMovementController : PlatformController
{
	public float moveSpeed;
	public float moveDistance;
	public MovableDirection platformDirection;

	private float currentlyMovedDistance;
	
	protected override void Awake()
	{
		base.Awake();
	}

	
	protected override void Update()
	{
		base.Update();
	}

	// Updates raycaster origins.
	// Checks type of movement: horizontal or verticle.
	// Change directions when set distance moved.
	protected override void Move()
	{
		rayCaster.UpdateRayCastOrigins(InnerBounds());

		Vector3 velocity = ConvertDirectionToVector3(platformDirection);
		velocity *= moveSpeed * Time.deltaTime;

		if (platformDirection == MovableDirection.Up || platformDirection == MovableDirection.Down)
		{
			VerticleMovement(velocity);
			currentlyMovedDistance += Mathf.Abs(velocity.y);
		}

		if (platformDirection == MovableDirection.Right || platformDirection == MovableDirection.Left)
		{
			HorizontalMovement(velocity);
			currentlyMovedDistance += Mathf.Abs(velocity.x);
		}

		if (currentlyMovedDistance >= moveDistance)
		{
			SwapMoveDirections();
			currentlyMovedDistance = 0;
			Wait();
		}

		thisTransform.Translate(velocity);
	}

	// Changest given movableDirection into an vector3.
	private Vector3 ConvertDirectionToVector3(MovableDirection Dir)
	{
		Vector3 returnVelocity = Vector3.zero;
		if (Dir == MovableDirection.Up) returnVelocity = Vector3.up;
		if (Dir == MovableDirection.Down) returnVelocity = Vector3.up * -1;
		if (Dir == MovableDirection.Right) returnVelocity = Vector3.right;
		if (Dir == MovableDirection.Left) returnVelocity = Vector3.right * -1;
		return returnVelocity;
	}

	// Inverts Direction
	private void SwapMoveDirections()
	{
		if (platformDirection == MovableDirection.Up) platformDirection = MovableDirection.Down;
		else if (platformDirection == MovableDirection.Down) platformDirection = MovableDirection.Up;
		else if (platformDirection == MovableDirection.Right) platformDirection = MovableDirection.Left;
		else if (platformDirection == MovableDirection.Left) platformDirection = MovableDirection.Right;
	}

	public enum MovableDirection
	{
		Up,
		Down,
		Left,
		Right
	}
}
