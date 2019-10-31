using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;

public class RadioKnobController : MonoBehaviour
{
    // Public references
    [NotNull]
    public InteractionBehaviour interactionBehaviour;
    [NotNull]
    public AudioSource audioSource;

    // Private state
    private float currentVolume = 1F;
    private bool isRadioOn = false;


    public void Start()
    {
        this.interactionBehaviour.OnPerControllerContactBegin += BeginTouchButton;
        this.interactionBehaviour.OnPerControllerContactEnd += EndTouchButton;
        this.interactionBehaviour.OnGraspedMovement += TurningButton;

        // Ensure radio is off
        TurnOffRadio();
    }

    public void BeginTouchButton(InteractionController controller)
    {
        if (this.isRadioOn)
        {
            TurnOffRadio();
        }
        else
        {
            TurnOnRadio();
        }
    }

    public void EndTouchButton(InteractionController controller)
    {
    }

    public void TurningButton(Vector3 oldPosition, Quaternion oldRotation, Vector3 newPosition, Quaternion newRotation, List<InteractionController> graspingControllers)
    {

    }

    public void TurnOnRadio()
    {
        this.isRadioOn = true;
        this.audioSource.volume = Mathf.Max(this.currentVolume, 0.1F);
    }

    public void TurnOffRadio()
    {
        this.isRadioOn = false;
        this.audioSource.volume = 0;
    }
}