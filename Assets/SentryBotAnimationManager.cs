using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SentryBotAnimationManager : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;

    void Awake()
    {
        if (!agent) agent = GetComponentInParent<NavMeshAgent>();
        if (!anim) anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void OpenDome(bool value)
    {
        anim.SetBool("DomeIsOpen", value);
    }

    public void CameraScan(bool value)
    {
        anim.SetBool("CameraIsSwiping", value);
    }
}
