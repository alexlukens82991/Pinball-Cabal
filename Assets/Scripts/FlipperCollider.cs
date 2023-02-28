using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperCollider : MonoBehaviour
{
    public bool BallIsTrapped;
    public bool IsLeftFlipper;
    public FlipperInput FlipperInput;
    [SerializeField] private HingeJoint m_HingeJoint;
    [SerializeField] private float m_PushVelo;
    private List<GameObject> m_CurrentBalls = new();
    public void TriggerPush()
    {
        if (BallIsTrapped)
        {
            foreach (GameObject ball in m_CurrentBalls)
            {
                ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * m_PushVelo, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ball")
        {
            GameObject ball = collision.collider.gameObject;

            if (!m_CurrentBalls.Contains(ball))
            {
                m_CurrentBalls.Add(ball);
            }

            BallIsTrapped = true;

            print("ANGLE: " + IsLeftFlipper + " | " + m_HingeJoint.angle);

            if (Mathf.Round(m_HingeJoint.angle) == m_HingeJoint.limits.min)
            {
                print("NOPE");
                return;
            }

            if (IsLeftFlipper)
            {
                if (FlipperInput.FlipperLeftState)
                {
                    ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * m_PushVelo, ForceMode.Impulse);
                }
            }
            else
            {
                if (FlipperInput.FlipperRightState)
                {
                    ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * m_PushVelo, ForceMode.Impulse);
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ball")
        {
            GameObject ball = collision.collider.gameObject;

            if (m_CurrentBalls.Contains(ball))
            {
                m_CurrentBalls.Remove(ball);
            }

            BallIsTrapped = false;
        }
    }
}
