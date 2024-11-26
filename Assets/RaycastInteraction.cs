using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastInteraction : MonoBehaviour
{
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float hintTimeBacking;
    public static float HintTime { get; private set; }

    private void Awake()
    {
        HintTime = hintTimeBacking;
    }
    private void Update()
    {
        RaycastHit hit;
        InteractableObject interactable = null;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayLength, layer))
        {
            interactable = GetGameObjectFromRaycastHit(hit, false).GetComponent<InteractableObject>();
            if (interactable != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Activate();
                }
                else if (!interactable.Activated)
                {
                    interactable.ShowEHint();
                }
            }
        }
        if ((!UIManager.Instance.TimedHintCrRunning && !interactable) || (interactable && interactable.Activated)))
        {
            UIManager.Instance.SetUIGOActive(false);
        }
    }

#if UNITY_EDITOR //solo para la good coding practise
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + rayOrigin.forward * rayLength);
    }
#endif



    public static GameObject GetGameObjectFromRaycastHit(RaycastHit hit, bool getRootGO = true)
    {
        GameObject go = hit.collider.gameObject;
        if (getRootGO)
        {
            return go.transform.root.gameObject;
        }
        else return go;
    }
}