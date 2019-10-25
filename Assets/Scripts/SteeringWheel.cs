using Leap.Unity.Interaction;
using UnityEngine;


public class SteeringWheel : MonoBehaviour
{
    // Public references
    public InteractionBehaviour interactionBehaviour;
    public Transform wheelTransform;
    public WheelCollider[] wheelColliders;
    public float steeringRatio = 12;

    // Private state
    private Vector3 wheelNormal;
    private float currentAngle = 0;
    private Vector3? leftHandLastPosition;
    private Vector3? rightHandLastPosition;


    public void Start()
    {
        this.interactionBehaviour.OnPerControllerContactBegin += BeginTouchWheel;
        this.interactionBehaviour.OnPerControllerContactEnd += EndTouchWheel;
        this.interactionBehaviour.OnContactStay += StayTouchWheel;

        this.wheelNormal = this.wheelTransform.TransformVector(Vector3.up).normalized;
    }

    public void BeginTouchWheel(InteractionController controller)
    {
        if (controller.isLeft)
        {
            this.leftHandLastPosition = controller.hoverPoint;
        }
        else if (controller.isRight)
        {
            this.rightHandLastPosition = controller.hoverPoint;
        }
    }

    public void EndTouchWheel(InteractionController controller)
    {
        if (controller.isLeft)
        {
            this.leftHandLastPosition = null;
        }
        else if (controller.isRight)
        {
            this.rightHandLastPosition = null;
        }
    }

    public void StayTouchWheel()
    {
        Vector3 wheelCenter = this.wheelTransform.position;


        float angleSum = 0;
        int angleCount = 0;
        Debug.DrawLine(wheelCenter, wheelCenter + this.wheelNormal, Color.magenta);

        foreach (InteractionController controller in this.interactionBehaviour.hoveringControllers)
        {
            Vector3 currentHandPosition = controller.hoverPoint;
            Vector3? handLastPosition = null;
            if (controller.isLeft)
            {
                if (this.leftHandLastPosition.HasValue)
                {
                    handLastPosition = this.leftHandLastPosition.Value;
                }

                this.leftHandLastPosition = currentHandPosition;
            }
            else if (controller.isRight)
            {
                if (this.rightHandLastPosition.HasValue)
                {
                    handLastPosition = this.rightHandLastPosition.Value;
                }
                this.rightHandLastPosition = currentHandPosition;
            }

            if (handLastPosition.HasValue)
            {
                Vector3 lastDelta = handLastPosition.Value - wheelCenter;
                Vector3 currentDelta = currentHandPosition - wheelCenter;
                Vector3 initialDeltaProjection = Vector3.ProjectOnPlane(lastDelta, this.wheelNormal);
                Vector3 currentDeltaProjection = Vector3.ProjectOnPlane(currentDelta, this.wheelNormal);

                float angleDelta = Vector3.SignedAngle(lastDelta, currentDelta, this.wheelNormal);

                if (angleDelta > 50)
                {
                    lastDelta = handLastPosition.Value - wheelCenter;
                    currentDelta = currentHandPosition - wheelCenter;
                    initialDeltaProjection = Vector3.ProjectOnPlane(lastDelta, this.wheelNormal);
                    currentDeltaProjection = Vector3.ProjectOnPlane(currentDelta, this.wheelNormal);

                    angleDelta = Vector3.SignedAngle(lastDelta, currentDelta, this.wheelNormal);


                }
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
