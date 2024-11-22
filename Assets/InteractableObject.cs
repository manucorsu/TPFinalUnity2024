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
    private string art;
    private string este;
    private string abierto;
    [SerializeField] private string nombre;

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
            art = "la";
            este = "Esta";
            abierto = "abierta";
        }
        else
        {
            art = "el";
            este = "Este";
            abierto = "abierto";
        }
    }

    public void Activate()
    {
        animator.SetTrigger("Open");
        Activated = true;
    }

    public string GetPressEHint() => $"Presiona [E] para abrir {art} {nombre}.";
    public string GetSuccessfullyOpenedHint() => $"Abriste {art} {nombre}.";
    public string GetAlreadyOpenedHint() => $"{este} {nombre} ya está {abierto}.";
}
