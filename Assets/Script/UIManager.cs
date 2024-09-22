using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] UIPanels;

    [HideInInspector]
    public bool atLeastOnePanelOpened;

    void Update()
    {
        atLeastOnePanelOpened = UIPanels.Any((panel) => panel == panel.activeSelf);

    }
}
