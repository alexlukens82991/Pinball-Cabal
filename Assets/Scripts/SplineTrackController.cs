using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class SplineTrackController : MonoBehaviour
{
    [SerializeField] private SplineContainer m_Spline;
    [SerializeField] private GameObject m_Ball;

    [Range(0f,1f)]
    [SerializeField] private float m_Position;
    [SerializeField] private float m_Speed;

    public bool BallCaptured;
    public bool EditorMode;

    private void UpdateBallPosition(GameObject ball)
    {
        if (m_Position > 1f)
        {
            m_Position = 1f;
        }
        else if (m_Position < 0)
        {
            m_Position = 0;
        }

        float3 position = m_Spline.EvaluatePosition(m_Position);
        Vector3 positionVector = new(position.x, position.y, position.z);

        ball.transform.position = positionVector;
        print(positionVector);
    }

    private IEnumerator AnimateBall(GameObject ball)
    {
        print("CAPTURED BALL. STARTING ANIMATION");
        m_Position = 0;

        do
        {
            float3 position = m_Spline.EvaluatePosition(m_Position);
            Vector3 positionVector = new(position.x, position.y, position.z);

            ball.transform.position = positionVector;

            m_Position += Time.deltaTime * m_Speed;
            yield return null;
        } while (m_Position < 1);

        Rigidbody rb = ball.GetComponent<Rigidbody>();

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.forward * 2, ForceMode.Impulse);

        BallCaptured = false;
    }

    public void TriggerHit(Collider other)
    {
        print("HIT");
        if (!BallCaptured)
        {
            BallCaptured = true;

            StartCoroutine(AnimateBall(other.gameObject));
        }
    }
    private void OnDrawGizmos()
    {
        if (!EditorMode)
            return;

        UpdateBallPosition(m_Ball);
    }
}
