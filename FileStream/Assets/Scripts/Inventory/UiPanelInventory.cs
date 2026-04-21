using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UiPanelInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiInvenSlotList uiInvenSlotList;

    private void OnEnable()
    {
        OnLoad();

        //SetText();
    }

    public void OnChangeSorting(int index)
    {
        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)index;

    }

    public void OnSave()
    {
        SaveLoadManager.Data.ItemList = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Data.sortingOptions = (UiInvenSlotList.SortingOptions)sorting.value;
        SaveLoadManager.Data.filteringOptions = (UiInvenSlotList.FilteringOptions)filtering.value;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList);


        sorting.value =(int)SaveLoadManager.Data.sortingOptions;
        filtering.value =(int)SaveLoadManager.Data.filteringOptions;

        /*
        sorting.captionText.text = SaveLoadManager.Data.sorting;
        filtering.captionText.text = SaveLoadManager.Data.filtering;

        int index = sorting.options.FindIndex(option => option.text == SaveLoadManager.Data.sorting);
        sorting.value = index != -1 ? index : 0;

        index = filtering.options.FindIndex(option => option.text == SaveLoadManager.Data.filtering);
        filtering.value = index != -1 ? index : 0;
        */
    }

    public void OnCreate()
    {
        uiInvenSlotList.AddRandomItem();
    }

    public void OnDelete()
    {
        uiInvenSlotList.RemoveItem();
    }


    // SortingOptions, FilteringOptions 의 열거형 값을 기반으로한 인덱스로 텍스트 설정
    public void SetText()
    {
        

        sorting.options[0].text =
            string.Format("{0}", DataTableManager.StringTable.Get("CREATIONTIMEASCENDING"));

        sorting.options[1].text =
            string.Format("{0}", DataTableManager.StringTable.Get("CREATIONTIMEDESCENDING"));

        sorting.options[2].text =
            string.Format("{0}", DataTableManager.StringTable.Get("NAMEASCENDING"));

        sorting.options[3].text =
            string.Format("{0}", DataTableManager.StringTable.Get("NAMEDESCENDING"));

        sorting.options[4].text =
            string.Format("{0}", DataTableManager.StringTable.Get("TYPEASCENDING"));

        sorting.options[5].text =
            string.Format("{0}", DataTableManager.StringTable.Get("TYPEDESCENDING"));

        sorting.options[6].text =
            string.Format("{0}", DataTableManager.StringTable.Get("VALUEASCENDING"));

        sorting.options[7].text =
            string.Format("{0}", DataTableManager.StringTable.Get("VALUEDESCENDING"));



        filtering.options[0].text =
            string.Format("{0}", DataTableManager.StringTable.Get("NONE"));

        filtering.options[1].text =
            string.Format("{0}", DataTableManager.StringTable.Get("WEAPON"));

        filtering.options[2].text =
            string.Format("{0}", DataTableManager.StringTable.Get("EQUIP"));

        filtering.options[3].text =
            string.Format("{0}", DataTableManager.StringTable.Get("CONSUMABLE"));

        filtering.options[4].text =
            string.Format("{0}", DataTableManager.StringTable.Get("NONCONSUMABLE"));
    }
}
