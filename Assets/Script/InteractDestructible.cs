using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDestructible : MonoBehaviour
{
    public float interactionRange = 5f;
    public LayerMask deconstructibleLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionRange, deconstructibleLayer))
            {
                Deconstructible deconstructible = hit.collider.GetComponent<Deconstructible>();

                if (deconstructible != null)
                {
                    deconstructible.Deconstruct();
                }
            }
        }
    }
}
