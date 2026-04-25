using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpInventory : MonoBehaviour
{
    public GameObject Inventory;

    private UiInvenSlotList inven;
    private UiInvenSlotList.FilteringOptions inventoryFilter;

    // 인벤토리 호출 및 이미지 설정을 위한 멤버
    public Button button;
    public Image icon;
    private void Start()
    {
        inven = Inventory.GetComponent<UiPanelInventory>().uiInvenSlotList;
        
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        // 명시 안하면 본인 image가 0순위라 지꺼 가져옴
        icon = transform.GetChild(0).GetComponent<Image>();
    }


    private void OnClick()
    {
        inventoryFilter = (UiInvenSlotList.FilteringOptions)Enum.Parse(typeof(UiInvenSlotList.FilteringOptions), gameObject.name);
        OpenInventory();
    }

    public void OpenInventory()
    {
        Inventory.SetActive(true);
        inven.Filtering = inventoryFilter;
        inven.equipSlot = this;
    }

    public void CloseInventory()
    {
        Inventory.SetActive(false);
    }
}
