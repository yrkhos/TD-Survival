using System.Collections;
using UnityEngine;
using System.Linq;

public class InteractBehaviour : MonoBehaviour
{
    [Header("References")]

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private Equipment equipmentSystem;

    [SerializeField]
    private EquipmentLibrary equipmentLibrary;

    [HideInInspector]
    public bool isBusy = false;

    [Header("Tools Configuration")]
    [SerializeField]
    private GameObject pickaxeVisual;

    [SerializeField]
    private GameObject axeVisual;

    [Header("Other")]

    private Item currentItem;
    private Harvestable currentHarvestable;
    private Tool currentTool;

    private Vector3 spawnItemOffset = new Vector3(0, 0.5f, 0);

    public void DoPickup(Item item)
    {
        if(isBusy)
        {
            return;
        }

        isBusy = true;

        if(inventory.IsFull())
        {
            Debug.Log("Inventory full, can't pick up : " + item.name);
            return;
        }

        currentItem = item;
    }

    public void DoHarvest(Harvestable harvestable)
    {
        if (isBusy)
        {
            return;
        }

        isBusy = true;

        currentTool = harvestable.tool;
        EnableToolGameObjectFromEnum(currentTool);

        currentHarvestable = harvestable;
    }

    // Coroutine appel�e depuis l'animation "Harvesting"
    IEnumerator BreakHarvestable()
    {
        Harvestable currentlyHarvesting = currentHarvestable;

        // Permet de d�sactiver la possibilit� d'int�ragir avec ce Harvestable + d'un fois (passage du layer Harvestable � Default)
        currentlyHarvesting.gameObject.layer = LayerMask.NameToLayer("Default");

        if(currentlyHarvesting.disableKinematicOnHarvest)
        {
            Rigidbody rigidbody = currentlyHarvesting.gameObject.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.AddForce(transform.forward * 800, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(currentlyHarvesting.destroyDelay);

        for (int i = 0; i < currentlyHarvesting.harvestableItems.Length; i++)
        {
            Ressource ressource = currentlyHarvesting.harvestableItems[i];

            if(Random.Range(1, 101) <= ressource.dropChance)
            {
                GameObject instantiatedRessource = Instantiate(ressource.itemData.prefab);
                instantiatedRessource.transform.position = currentlyHarvesting.transform.position + spawnItemOffset;
            }
        }

        Destroy(currentlyHarvesting.gameObject);
    }

    public void AddItemToInventory()
    {
        inventory.AddItem(currentItem.itemData);
        Destroy(currentItem.gameObject);
    }

    public void ReEnablePlayerMovement()
    {
        EnableToolGameObjectFromEnum(currentTool, false);
        isBusy = false;
    }

    private void EnableToolGameObjectFromEnum(Tool toolType, bool enabled = true)
    {
        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == equipmentSystem.equipedWeaponItem).FirstOrDefault();

        if (equipmentLibraryItem != null)
        {
            equipmentLibraryItem.itemPrefab.SetActive(!enabled);
        }

        switch (toolType)
        {
            case Tool.Pickaxe:
                pickaxeVisual.SetActive(enabled);
                break;
            case Tool.Axe:
                axeVisual.SetActive(enabled);
                break;
        }
    }
}
