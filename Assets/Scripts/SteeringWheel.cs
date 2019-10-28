using Leap.Unity.Interaction;
using UnityEngine;


public class SteeringWheel : MonoBehaviour
{
    // Public references
    [NotNull]
    public InteractionBehaviour interactionBehaviour;
    [NotNull]
    public Transform wheelTransform;
    [NotNull]
    public WheelCollider[] wheelColliders;
    public float steeringRatio = 12;

    // Private state
    private float currentAngle = 0;
    private Vector3? leftHandLastDelta;
    private Vector3? rightHandLastDelta;


    public void Start()
    {
        this.interactionBehaviour.OnPerControllerContactBegin += BeginTouchWheel;
        this.interactionBehaviour.OnPerControllerContactEnd += EndTouchWheel;
        this.interactionBehaviour.OnContactStay += StayTouchWheel;
    }

    public void BeginTouchWheel(InteractionController controller)
    {
        if (controller.isLeft)
        {
            this.leftHandLastDelta = controller.hoverPoint - this.wheelTransform.position;
        }
        else if (controller.isRight)
        {
            this.rightHandLastDelta = controller.hoverPoint - this.wheelTransform.position;
        }
    }

    public void EndTouchWheel(InteractionController controller)
    {
        if (controller.isLeft)
        {
            this.leftHandLastDelta = null;
        }
        else if (controller.isRight)
        {
            this.rightHandLastDelta = null;
        }
    }

    public void StayTouchWheel()
    {
        Vector3 wheelCenter = this.wheelTransform.position;
        Vector3 wheelNormal = this.wheelTransform.TransformVector(Vector3.up).normalized;



        float angleSum = 0;
        int angleCount = 0;

        foreach (InteractionController controller in this.interactionBehaviour.hoveringControllers)
        {
            Vector3 handCurrentDelta = controller.hoverPoint - wheelCenter;
            Vector3? handLastDelta = null;
            if (controller.isLeft)
            {
                // If there is a previous value for this hand
                if (this.leftHandLastDelta.HasValue)
                {
                    // Grab it
                    handLastDelta = this.leftHandLastDelta.Value;
                }

                // Store current value as previous value (no matter what)
                this.leftHandLastDelta = handCurrentDelta;
            }
            else if (controller.isRight)
            {
                if (this.rightHandLastDelta.HasValue)
                {
                    handLastDelta = this.rightHandLastDelta.Value;
                }
                this.rightHandLastDelta = handCurrentDelta;
            }

            // Ensure we have both a current and previous value
            if (handLastDelta.HasValue)
            {
                Vector3 lastDeltaProjection = Vector3.ProjectOnPlane(handLastDelta.Value, wheelNormal);
                Vector3 currentDeltaProjection = Vector3.ProjectOnPlane(handCurrentDelta, wheelNormal);

                float angleDelta = Vector3.SignedAngle(handLastDelta.Value, handCurrentDelta, wheelNormal);

                angleSum += angleDelta;
                angleCount++;
            }
        }

        if (angleCount > 0)
        {
            float angle = angleSum / angleCount;
            this.wheelTransform.Rotate(Vector3.up, angle, Space.Self);
            UpdateCurrentAngle(this.currentAngle + angle);
        }
    }

    private void UpdateCurrentAngle(float newAngle)
    {
        this.currentAngle = newAngle;
        foreach (WheelCollider wheel in this.wheelColliders)
        {
            wheel.steerAngle = newAngle / this.steeringRatio;
        }
    }
}
