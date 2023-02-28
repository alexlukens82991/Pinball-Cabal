using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperInput : MonoBehaviour
{
    [SerializeField] private float m_FlipperActiveVelo;
    [SerializeField] private float m_FlipperActiveForce;
    [SerializeField] private HingeJoint m_LeftJoint;
    [SerializeField] private HingeJoint m_RightJoint;
    [SerializeField] private FlipperCollider m_LeftCollider;
    [SerializeField] private FlipperCollider m_RightCollider;
    public bool FlipperLeftState;
    public bool FlipperRightState;

    private void Update()
    {
        bool leftActive = Input.GetKey(KeyCode.LeftArrow);
        bool rightActive = Input.GetKey(KeyCode.RightArrow);
        if (leftActive != FlipperLeftState)
        {
            SetFlipperState(true, leftActive);
        }

        if (rightActive != FlipperRightState)
        {
            SetFlipperState(false, rightActive);
        }
    }

    private void SetFlipperState(bool leftFlipper, bool state)
    {
        HingeJoint joint = leftFlipper ? m_LeftJoint : m_RightJoint;
        float activeVelo = m_FlipperActiveVelo;

        if (leftFlipper)
            FlipperLeftState = state;
        else
            FlipperRightState = state;

        var motor = joint.motor;
        motor.force = m_FlipperActiveForce;
        motor.targetVelocity = state ? activeVelo : -activeVelo;
        motor.freeSpin = false;
        joint.motor = motor;
        joint.useMotor = true;

        if (state)
        {
            if (leftFlipper)
            {
                m_LeftCollider.TriggerPush();
            }
            else
            {
                m_RightCollider.TriggerPush();
            }
        }
    }
}
