using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiInvenSlotList : MonoBehaviour
{
    // 1. 필터링 옵션 저장하기
    // 2. 필터링옵션 : NONCONSUMABLE 추가
    // 3. 솔팅 옵션에 VALUE, TYPE 등도 추가


    public enum SortingOptions
    {
        CreationTimeAscending,
        CreationTimeDescending,
        NameAscending,
        NameDescending,
        TypeAscending,
        TypeDescending,
        ValueAscending,
        ValueDescending,
    }

    public enum FilteringOptions
    {
        None,
        Weapon,
        Equip,
        Consumable,
    }

    // 분류 쓸거니까 컴페어 요소도 넣어두기
    public readonly System.Comparison<SaveItemData>[] comparison =
    {
        // 생성시간 정렬
        (lhs, rhs) => lhs.creationTime.CompareTo(rhs.creationTime),
        (lhs, rhs) => rhs.creationTime.CompareTo(lhs.creationTime),

        // 이름 정렬
        (lhs, rhs) => lhs.ItemData.StringName.CompareTo(rhs.ItemData.StringName),
        (lhs, rhs) => rhs.ItemData.StringName.CompareTo(lhs.ItemData.StringName),

        (lhs, rhs) => lhs.ItemData.Type.CompareTo(rhs.ItemData.Type),
        (lhs, rhs) => rhs.ItemData.Type.CompareTo(lhs.ItemData.Type),

        (lhs, rhs) => lhs.ItemData.Value.CompareTo(rhs.ItemData.Value),
        (lhs, rhs) => rhs.ItemData.Value.CompareTo(lhs.ItemData.Value),

    };

    public readonly System.Func<SaveItemData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.ItemData.Type == ItemTypes.Weapon,
        (x) => x.ItemData.Type == ItemTypes.Equip,
        (x) => x.ItemData.Type == ItemTypes.Consumable,
        (x) => x.ItemData.Type != ItemTypes.Consumable,
    };


    private List<UiInvenSlot> uiSlotList = new List<UiInvenSlot>();
    // 정렬을 위한 리스트
    private List<SaveItemData> saveItemDataList = new List<SaveItemData>(); 
    public UiItemInfo itemInfo;

    public UiInvenSlot prefab;
    public ScrollRect scrollRect;

    private SortingOptions sorting = SortingOptions.CreationTimeAscending;
    private FilteringOptions filter = FilteringOptions.None;

    public SortingOptions Sorting
    {
        get { return sorting; }
        set
        {
            if (sorting != value)
            {
                sorting = value;
                UpdateSlots();
            }


        }
    }
    public FilteringOptions Filtering
    {
        get { return filter; }
        set
        {
            if (filter != value)
            {
                filter = value;
                UpdateSlots();
            }
            
        }
    }

    private int selectedSlotIndex = -1;

    // 데이터 갱신을 위한 이벤트
    public UnityEvent onUpdateSlot;
    public UnityEvent<SaveItemData> onSelectSlot;

    private void OnSelectSlot(SaveItemData saveItemData)
    {
        Debug.Log(saveItemData);
        itemInfo.SetSaveItemData(saveItemData);
    }

    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);

    }


    private void OnDisable()
    {
        saveItemDataList = null;
    }



    public void SetSaveItemDataList(List<SaveItemData> source)
    {
        saveItemDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveItemData> GetSaveItemDataList()
    {
        return saveItemDataList;
    }

    private void UpdateSlots()
    {
        var list = saveItemDataList.Where(filterings[(int)filter]).ToList();
        list.Sort(comparison[(int)sorting]);


        // 아이템 리스트를 받아서 슬롯 리스트 생성
        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);

                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.SaveItemData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotList.Count; i++)
        {
            if(i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetItem(list[i]);
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }

        }

        selectedSlotIndex = -1;
        onUpdateSlot.Invoke();
    }


    public void AddRandomItem()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());
        UpdateSlots();
    }

    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }


        saveItemDataList.Remove(uiSlotList[selectedSlotIndex].SaveItemData);
        UpdateSlots();

    }
}
