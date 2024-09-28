using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPorte : MonoBehaviour
{
    public Animator animator;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Ouvrir", true);
            animator.SetBool("Fermer", false);
        }
    }

    public void OnTriggerExit(Collider other)
    {

        animator.SetBool("Fermer", true);
        animator.SetBool("Ouvrir", false);
    }
}
