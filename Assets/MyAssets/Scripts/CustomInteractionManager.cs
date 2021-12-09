using UnityEngine.XR.Interaction.Toolkit;

public class CustomInteractionManager : XRInteractionManager
{
    // Used by the notch when the arrow is pulled too far at the PullMeasurer
    public void ForceDeselect(XRBaseInteractor interactor)
    {
        if (interactor.selectTarget)
            SelectExit(interactor, interactor.selectTarget);
    }
}
