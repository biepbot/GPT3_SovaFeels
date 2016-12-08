using UnityEngine;
using System.Collections;


public class PlayerController : BaseController
{
	public float interactRange = 1f;

	public CollisionInfo collInfo;

	public LayerMask interactMask;

	private GameObject interactable;

	public GameObject canvas;

	private CanvasScript canvasScript;

	protected override void Awake()
	{
		base.Awake();
		canvasScript = canvas.GetComponent<CanvasScript>();
	}
    
    /// <summary>
    /// This is the standard move method.
    /// Takes the velocity at which the player should be moved.
    /// </summary>
    /// <param name="velocity"></param>
    public void Move(Vector3 velocity)
	{
		velocity *= Time.deltaTime;
		rayCaster.UpdateRayCastOrigins(InnerBounds());
		collInfo.Reset();

		if (velocity.x != 0) HorizontalColision(ref velocity);
		if (velocity.y != 0) VerticalColision(ref velocity);

		thisTransform.Translate(velocity);

		CheckInteractable();
	}
    
    /// <summary>
    /// Moves the player when on a platform.
    /// Takes the platform velocity to move the player at equal speed.
    /// </summary>
    /// <param name="velocity"></param>
    public void PlatformMove(Vector3 velocity)
	{
		rayCaster.UpdateRayCastOrigins(InnerBounds());
		HorizontalColision(ref velocity);
		thisTransform.Translate(velocity);
	}
    
    /// <summary>
    /// If there is an item to interact with, interact with it.
    /// </summary>
    public void Interact()
	{
		if (interactable != null)
		{
			interactable.GetComponent<NPCController>().Talk();
		}
	}
    
    /// <summary>
    /// Looks at the players right and left for the closest interactable object.
    /// This object will be saved in interactable.
    /// </summary>
    private void CheckInteractable()
	{
		interactable = null;
		for (int i = 0; i < rayCaster.horizontalRayCount; i++)
		{
			Vector2 rayOffset = Vector2.up * (rayCaster.horizontalSpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayCaster.rayCastOrigins.bottomRight + rayOffset, Vector2.right, interactRange, interactMask);
			if(!hit) hit = Physics2D.Raycast(rayCaster.rayCastOrigins.bottomLeft + rayOffset, Vector2.right * -1, interactRange, interactMask);

			if (hit)
			{
				interactable = hit.transform.gameObject;
				Vector3 interactablePos = interactable.transform.position;
				canvasScript.SetInteractable(new Vector3(interactablePos.x, interactablePos.y + 2, interactablePos.z));
				return;
			}
		}
		canvasScript.RemoveDialog();
	}
    
    /// <summary>
    /// Checks for colisions to the players left and right.
    /// Direction depends on the velocity.
    /// </summary>
    /// <param name="velocity"></param>
    private void HorizontalColision(ref Vector3 velocity)
	{
		float directionX = Mathf.Sign(velocity.x);
		float rayLength = Mathf.Abs(velocity.x) + skinWidth;
		for (int i = 0; i < rayCaster.horizontalRayCount; i++)
		{
			Vector2 rayOrigin = (directionX == -1) ? rayCaster.rayCastOrigins.bottomLeft : rayCaster.rayCastOrigins.bottomRight;
			rayOrigin += Vector2.up * (rayCaster.horizontalSpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength * 3, Color.red);

			if (hit)
			{
				velocity.x = (hit.distance - skinWidth) * directionX;
				rayLength = hit.distance;

				collInfo.left = directionX == -1;
				collInfo.right = directionX == 1;
			}
		}
	}
    
    /// <summary>
    /// Checks for colisions to the players top and bottom.
    /// Direction depends on the velocity.
    /// </summary>
    /// <param name="velocity"></param>
    private void VerticalColision(ref Vector3 velocity)
	{
		float directionY = Mathf.Sign(velocity.y);
		float rayLength = Mathf.Abs(velocity.y) + skinWidth;
		for (int i = 0; i < rayCaster.verticleRayCount; i++)
		{
			Vector2 rayOrigin = (directionY == -1) ? rayCaster.rayCastOrigins.bottomLeft : rayCaster.rayCastOrigins.topLeft;
			rayOrigin += Vector2.right * (rayCaster.verticleSpacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength * 3, Color.red);

			if (hit)
			{
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;

				collInfo.below = directionY == -1;
				collInfo.above = directionY == 1;
			}
		}
	}

	public struct CollisionInfo
	{
		public bool above, below;
		public bool left, right;

		public void Reset()
		{
			above = false;
			below = false;
			right = false;
			left = false;
		}
	}
}
