using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(PullMeasurer))]
public class Notch : XRSocketInteractor
{
    // Settings
    // Controls how far the player has to pull back for the arrow to be released
    [Range(0, 1)] public float releaseThreshold = 0.25f;

    public PullMeasurer PullMeasurer { get; private set; } = null;
    // Set when the bow is picked up or dropped
    public bool IsReady { get; private set; } = false;

    // Need to cast to custom for Force Deselect
    // Get a reference to the interactionManager so we can use the forceDeselect function
    private CustomInteractionManager CustomManager => interactionManager as CustomInteractionManager;


    protected override void Awake()
    {
        base.Awake();
        PullMeasurer = GetComponent<PullMeasurer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // Arrow is released once the puller is released
        PullMeasurer.selectExited.AddListener(ReleaseArrow);

        // Whenever the PullMeasurer is pulled,
        // Move the point where the arrow is attached (must follow the middle transform of the string)
        PullMeasurer.Pulled.AddListener(MoveAttach);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        // Also unsubscribe from events
        PullMeasurer.selectExited.RemoveListener(ReleaseArrow);
        PullMeasurer.Pulled.RemoveListener(MoveAttach);
    }

    public void ReleaseArrow(SelectExitEventArgs args)
    {
        // Only release if the target is an arrow using custom deselect
        if (selectTarget is Arrow && PullMeasurer.PullAmount > releaseThreshold)
            CustomManager.ForceDeselect(this);
    }

    public void MoveAttach(Vector3 pullPosition, float pullAmount)
    {
        // Move attach when bow is pulled, this updates the renderer as well
        attachTransform.position = pullPosition;
    }

    public void SetReady(BaseInteractionEventArgs args)
    {
        // Set the notch ready if bow is selected
        IsReady = args.interactable.isSelected;
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        // We check for the hover here too, since it factors in the recycle time of the socket
        // We also check that notch is ready, which is set once the bow is picked up
        return base.CanSelect(interactable) && CanHover(interactable) && IsArrow(interactable);
    }

    private bool IsArrow(XRBaseInteractable interactable)
    {
        // Simple arrow check, can be tag or interaction layer as well
        return interactable is Arrow;
    }

    public override XRBaseInteractable.MovementType? selectedInteractableMovementTypeOverride
    {
        // Use instantaneous so it follows smoothly
        get { return XRBaseInteractable.MovementType.Instantaneous; }
    }

    // This enables the socket to grab the arrow immediately
    public override bool requireSelectExclusive => false;
}
