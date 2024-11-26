using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryBotRaycastDetection : MonoBehaviour
{
    public Transform rayOrigin;
    public float rayLength;
    public LayerMask layerMask;
    private SentryBotBehavior targeter;

    private void Awake()
    {
        targeter = this.gameObject.GetComponent<SentryBotBehavior>();
        if (rayLength <= 0) rayLength = float.MaxValue;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayLength, layerMask))
        {
            GameObject hitGO = RaycastInteraction.GetGameObjectFromRaycastHit(hit);
            if (hitGO.CompareTag("Player"))
            {
                if(targeter.Patrolling) targeter.StartAggro(hitGO.transform);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + rayOrigin.forward * rayLength);
    }
#endif
}
