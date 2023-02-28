using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject m_Object;
    [SerializeField] private float m_SpawnInterval;
    [SerializeField] private Vector3 m_ForceDirection;
    [SerializeField] private float m_TargetForce;
    [SerializeField] private Transform m_Target;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        do
        {
            GameObject newObj = Instantiate(m_Object);
            newObj.transform.position = transform.position;

            Vector3 forceDir = Vector3.zero;

            if (m_Target != null)
            {
                forceDir = (m_Target.position - transform.position).normalized;
            }
            else
            {
                forceDir = m_ForceDirection;
            }


            Rigidbody rb = newObj.GetComponent<Rigidbody>();
            rb.AddForce(forceDir * m_TargetForce, ForceMode.Impulse);

            yield return new WaitForSeconds(m_SpawnInterval);
        } while (true);
    }
}
