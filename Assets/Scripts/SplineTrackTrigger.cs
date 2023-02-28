using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineTrackTrigger : MonoBehaviour
{
    public SplineTrackController splineTrackController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
            splineTrackController.TriggerHit(other); 
    }
}
