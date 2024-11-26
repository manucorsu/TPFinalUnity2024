using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Genero //género de la palabra del objeto (la/esta puerta, el/este locker, la/esta ventana)
{
    Femenino,
    Masculino
}

public class InteractableObject : MonoBehaviour
{
    [field: SerializeField] public bool Activated { get; private set; } = false;
    //el field:SerializeField es solo para poder verlo del inspector, no se debe tocar desde ahí

    [SerializeField] private Animator animator;
    [SerializeField] private Genero genero;
    private string the;
    private string @this;
    private string open;
    [SerializeField] private string nombre;

    private string pressEHint;
    private string succesfullyOpenedHint;
    private string alreadyOpenedHint;
    private float ht;

    private void Awake()
    {
        Activated = false;
        if (!animator)
        {
            animator = GetComponentInParent<Animator>();
        }
        if (nombre == "") throw new System.Exception("No se asignó el nombre del objeto!");

        if (genero == Genero.Femenino)
        {
            the = "la";
            @this = "Esta";
            open = "abierta";
        }
        else
        {
            the = "el";
            @this = "Este";
            open = "abierto";
        }

        pressEHint = $"Presioná [E] para abrir {the} {nombre}.";
        succesfullyOpenedHint = $"Abriste {the} {nombre}.";
        alreadyOpenedHint = $"{@this} {nombre} ya está {open}.";
        Activated = false;
    }

    private void Start()
    {
        ht = RaycastInteraction.HintTime;
    }

    public void Activate()
    {
        if (!Activated)
        {
            animator.SetTrigger("Open");
            Activated = true;
            UIManager.Instance.StartShowingTimedHint(succesfullyOpenedHint, Color.white, ht);
        }
        else
        {
            UIManager.Instance.StartShowingTimedHint(alreadyOpenedHint, Color.white, ht);
        }
    }

    public void ShowEHint()
    {
        UIManager.Instance.SetHintText(pressEHint, Color.white);
    }
}
