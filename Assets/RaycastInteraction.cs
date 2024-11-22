using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastInteraction : MonoBehaviour
{
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject uiGO;
    [SerializeField] private Text hintText;
    [SerializeField] private float hintTime;
    private Coroutine coroutineShowTimedHint;

    private void Update()
    {
        RaycastHit hit;
        InteractableObject interactable = null;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayLength, layer))
        {
            interactable = hit.collider.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Activate();
                    string message;
                    if (interactable.Activated) message = interactable.GetAlreadyOpenedHint();
                    else message = interactable.GetSuccessfullyOpenedHint();

                    StopAllCoroutines();
                    coroutineShowTimedHint = StartCoroutine(ShowTimedHint(hintTime, message));
                }
                else hintText.text = interactable.GetPressEHint();
            }
        }
        if (coroutineShowTimedHint == null) uiGO.SetActive(interactable);
        if (coroutineShowTimedHint == null) Debug.Log(null);
        else Debug.Log("not null");
    }

#if UNITY_EDITOR //solo para la good coding practise
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + rayOrigin.forward * rayLength);
    }
#endif

    private IEnumerator ShowTimedHint(float hintTime, string hint)
    { 
        if (hintTime <= 0)
            throw new System.ArgumentException($"'{nameof(hintTime)}' debe ser mayor a 0");
        if (hint == "")
            Debug.LogWarning($"'{nameof(hint)}' es un string vacío");

        float time = 0;
        while (time < hintTime)
        {
            time += Time.deltaTime;
            uiGO.SetActive(true);
            hintText.text = hint;
            yield return null;
        }

        uiGO.SetActive(false);
    }
}
