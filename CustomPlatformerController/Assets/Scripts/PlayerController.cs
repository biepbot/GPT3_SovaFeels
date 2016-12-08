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

        if (velocity.x != 0) HorizontalCollision(ref velocity);
        if (velocity.y != 0) VerticalCollision(ref velocity);

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
        HorizontalCollision(ref velocity);
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
            if (!hit) hit = Physics2D.Raycast(rayCaster.rayCastOrigins.bottomLeft + rayOffset, Vector2.right * -1, interactRange, interactMask);

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
    private void HorizontalCollision(ref Vector3 velocity)
    {
        HandleCollision(ref velocity, false);
    }

    /// <summary>
    /// Checks for colisions to the players top and bottom.
    /// Direction depends on the velocity.
    /// </summary>
    /// <param name="velocity"></param>
    private void VerticalCollision(ref Vector3 velocity)
    {
        HandleCollision(ref velocity, true);
    }

    /// <summary>
    /// Handles the collissions
    /// </summary>
    /// <param name="velocity"></param>
    /// <param name="vertical"></param>
    private void HandleCollision(ref Vector3 velocity, bool vertical)
    {
        float vel = vertical ? velocity.y : velocity.x;
        Vector2 vmovement = (vertical ? Vector2.right : Vector2.up);
        Vector2 hmovement = (vertical ? Vector2.up : Vector2.right);

        float direction = Mathf.Sign(vel);
        float rayLength = Mathf.Abs(vel) + skinWidth;
        for (int i = 0; i < rayCaster.verticleRayCount; i++)
        {
            Vector2 rayOrigin = (direction == -1) ? rayCaster.rayCastOrigins.bottomLeft : vertical ? rayCaster.rayCastOrigins.topLeft : rayCaster.rayCastOrigins.bottomRight;
            rayOrigin += vmovement * ((vertical ? rayCaster.verticleSpacing : rayCaster.horizontalSpacing) * i + (vertical ? velocity.x : 0));
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, hmovement * direction, rayLength, collisionMask);

#if UNITY_EDITOR
            Debug.DrawRay(rayOrigin, hmovement * direction * rayLength * 3, Color.red);
#endif

            if (hit)
            {
                rayLength = hit.distance;
                if (vertical)
                {
                    velocity.y = (hit.distance - skinWidth) * direction;

                    collInfo.below = direction == -1;
                    collInfo.above = direction == 1;
                }
                else
                {
                    velocity.x = (hit.distance - skinWidth) * direction;

                    collInfo.left = direction == -1;
                    collInfo.right = direction == 1;
                }
            }
        }
    }

    public struct CollisionInfo
    {
        public bool above, below, left, right;
        public void Reset()
        {
            above = below = right = left = false;
        }
    }
}
