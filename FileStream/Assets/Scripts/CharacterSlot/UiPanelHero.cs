using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UiPanelHero : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiHeroList uiHeroList;

    private void OnEnable()
    {
        OnLoad();

        //SetText();
    }

    public void OnChangeSorting(int index)
    {
        uiHeroList.Sorting = (UiHeroList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiHeroList.Filtering = (UiHeroList.FilteringOptions)index;

    }

    public void OnSave()
    {
        SaveLoadManager.Data.CharacterList = uiHeroList.GetSaveCharacterDataList();
        SaveLoadManager.Data.characterSortingOptions = (UiHeroList.SortingOptions)sorting.value;
        SaveLoadManager.Data.characterFilteringOptions = (UiHeroList.FilteringOptions)filtering.value;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiHeroList.SetSaveCharacterDataList(SaveLoadManager.Data.CharacterList);


        sorting.value = (int)SaveLoadManager.Data.characterSortingOptions;
        filtering.value = (int)SaveLoadManager.Data.characterFilteringOptions;
    }

    public void OnCreate()
    {
        uiHeroList.AddRandomItem();
    }

    public void OnDelete()
    {
        uiHeroList.RemoveItem();
    }


    


}
