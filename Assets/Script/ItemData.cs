using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/New item")]
public class ItemData : ScriptableObject
{
    public string nom;
    public string description;
    public Sprite visual;
    public GameObject prefab;

    public ItemType itemType;
    public EquipementType equipementType;
}

public enum ItemType
{
    Ressource,
    Consommable,
    Equipement
}

public enum EquipementType
{
    Tete,
    Torse,
    Gants,
    Jambiere,
    Chaussure,
    Arme,
    Outil
}
