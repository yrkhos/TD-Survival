using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deconstructible : MonoBehaviour
{
   /*public int woodAmount = 10;
    public int stoneAmount = 5;*/

    public void Deconstruct()
    {
        // Ajouter les ressources � l'inventaire du joueur
        /*PlayerInventory.instance.AddWood(woodAmount);
        PlayerInventory.instance.AddStone(stoneAmount);*/

        // D�truire l'objet
        Destroy(gameObject);
    }
}
