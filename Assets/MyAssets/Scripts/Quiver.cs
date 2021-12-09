using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Quiver : XRBaseInteractable
{
    public GameObject arrowPrefab = null;

    protected override void OnEnable()
    {
        base.OnEnable();
        // Once the quiver has been selected, CreateAndSelectArrow
        selectEntered.AddListener(CreateAndSelectArrow);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(CreateAndSelectArrow);
    }


    // Instead of having the interactor as parameter, we can have
    // args so we can use them to give aditional information (if needed)
    private void CreateAndSelectArrow(SelectEnterEventArgs args)
    {
        /* Once the Quiver is selected, we create an arrow
         * and we use the interaction manager to force the selection
         * on the interactor (interacting hand) */
        Arrow arrow = CreateArrow(args.interactor.transform);
        interactionManager.ForceSelect(args.interactor, arrow);
    }

    private Arrow CreateArrow(Transform orientation)
    {
        // Create arrow, and get arrow component
        GameObject arrowObject = Instantiate(arrowPrefab, orientation.position, orientation.rotation);
        return arrowObject.GetComponent<Arrow>();
    }
}
