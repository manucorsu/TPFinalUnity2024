using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastInteraction : MonoBehaviour
{
    public Transform rayOrigin;
    public float rayLength;
    public LayerMask layer;
    public GameObject uiGO;
    public Text hintText;
    public float hintTime;
    private Coroutine showHintDuringTime;

    void Update()
    {
        RaycastHit hit;
        InteractableObject interactable = null;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayLength, layer))
        {
            interactable = hit.collider.GetComponent<InteractableObject>();
            if (interactable)
            {
                if (showHintDuringTime == null || !interactable.Activated)
                {
                    uiGO.SetActive(true);
                    hintText.text = interactable.GetPressEHint();
                }


                if (Input.GetKeyDown(KeyCode.E))
                {
                    string message;
                    if (!interactable.Activated)
                    {
                        interactable.PlayObjectAnimation();
                        message = interactable.GetSuccessfullyOpenedHint();
                    }
                    else
                    {
                        message = interactable.GetAlreadyOpenedHint();
                    }
                    StopAllCoroutines();
                    showHintDuringTime = StartCoroutine(ShowHintDuringTime(message, hintTime));
                }
            }
        }
    }

#if UNITY_EDITOR //solo para la good coding practise
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + rayOrigin.forward * rayLength);
    }
#endif

    IEnumerator ShowHintDuringTime(string hintMessage, float hintTime)
    {
        float time = 0;
        hintText.text = hintMessage;
        uiGO.SetActive(true);
        while (time < hintTime)
        {
            time += Time.deltaTime;
            uiGO.SetActive(true);
            yield return null;
        }
        uiGO.SetActive(false);
    }

}
