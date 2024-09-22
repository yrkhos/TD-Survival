using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [Header("Inventory Panel References")]

    [SerializeField]
    private List<ItemData> content = new List<ItemData>();

    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private Transform inventorySlotsParent;

    [SerializeField]
    private Transform drop;

    const int InventorySize = 20;

    [Header("Action Panel References")]

    [SerializeField]
    private GameObject actionPanel;

    [SerializeField]
    private GameObject useItemButton;

    [SerializeField]
    private GameObject equipItemButton;

    [SerializeField]
    private GameObject dropItemButton;

    [SerializeField]
    private GameObject destroyItemButton;

    private ItemData itemCurrentlySelected;

    [SerializeField]
    private Sprite transparenteTexture;

    [Header("Script equipment Panel References")]

    [SerializeField]
    private EquipmentLibrary equipmentLibrary;

    public static Inventory instance;

    private bool isOpen = false;

    [Header("Armures Panel References")]

    [SerializeField]
    private Image CasqueSlotImage;

    [SerializeField]
    private Image TorseSlotImage;

    [SerializeField]
    private Image GantsSlotImage;

    [SerializeField]
    private Image PantalonSlotImage;

    [SerializeField]
    private Image BottesSlotImage;

    [Header("Amements & Outils Panel References")]

    [SerializeField]
    private Image ArmeSlotImage;

    [SerializeField]
    private Image OutilsSlotImage;

    private void Start()
    {
        RefreshContent();
    }

    private void Awake()
    {
        instance = this;
    }

    public void AddItem(ItemData item)
    {
        content.Add(item);
        RefreshContent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    private void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        isOpen = true;
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        actionPanel.SetActive(false);
        TooltipSystem.instance.Hide();
        isOpen = false;
    }

    private void RefreshContent()
    {
        for (int i = 0; i < inventorySlotsParent.childCount; i++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();

            currentSlot.item = null;
            currentSlot.itemVisual.sprite = transparenteTexture;
        }

        for (int i = 0; i < content.Count; i++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();

            currentSlot.item = content[i];
            currentSlot.itemVisual.sprite = content[i].visual;
        }
    }

    public bool IsFull()
    {
        return InventorySize == content.Count;
    }

    public void OpenActionPanel(ItemData item, Vector3 slotPosition)
    {
        itemCurrentlySelected = item;

        if (item == null)
        {
            actionPanel.SetActive(false);
            return;
        }

        switch (item.itemType)
        {
            case ItemType.Ressource:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(false);
                break;
            case ItemType.Equipement:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(true);
                break;
            case ItemType.Consommable:
                useItemButton.SetActive(true);
                equipItemButton.SetActive(false);
                break;
        }

        actionPanel.transform.position = slotPosition;
        actionPanel.SetActive(true);
    }

    public void CloseActionPanel()
    {
        actionPanel.SetActive(false);
        itemCurrentlySelected = null;
    }

    public void UseActionButton()
    {
        print("Use item :" + itemCurrentlySelected.name);
        CloseActionPanel();
    }

    public void EquipActionButton()
    {

        print("Equip item :" + itemCurrentlySelected.name);

        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.Where(elem => elem.itemData == itemCurrentlySelected).First();

        if(equipmentLibraryItem != null)
        {
            equipmentLibraryItem.itemPregab.SetActive(true);

            switch (itemCurrentlySelected.equipementType)
            {
                case EquipementType.Casque:
                    CasqueSlotImage.sprite = itemCurrentlySelected.visual;
                    break;

                case EquipementType.Torse:
                    TorseSlotImage.sprite = itemCurrentlySelected.visual;
                    break;

                case EquipementType.Gants:
                    GantsSlotImage.sprite = itemCurrentlySelected.visual;
                    break;

                case EquipementType.Jambiere:
                    PantalonSlotImage.sprite = itemCurrentlySelected.visual;
                    break;

                case EquipementType.Bottes:
                    BottesSlotImage.sprite = itemCurrentlySelected.visual;
                    break;
            }
            content.Remove(itemCurrentlySelected);
            RefreshContent();
        }
        else
        {
            Debug.LogError("Equipment : " + itemCurrentlySelected + " non existant dans la scene");
        }

        CloseActionPanel();
    }

    public void DropActionButton()
    {
        GameObject instantiatedItem = Instantiate(itemCurrentlySelected.prefab);
        instantiatedItem.transform.position = drop.position;
        content.Remove(itemCurrentlySelected);
        RefreshContent();
        CloseActionPanel();
    }

    public void DestroyActionButton()
    {
        content.Remove(itemCurrentlySelected);
        RefreshContent();
        CloseActionPanel();
    }
}