using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiHeroList : MonoBehaviour
{
    public enum SortingOptions
    {
        CreationTimeAscending,
        CreationTimeDescending,
        NameAscending,
        NameDescending,
        AttackPowerAscending, 
        AttackPowerDescending,
    }

    public enum FilteringOptions
    {
        None,
        Common,
        Rare,
        Epic,
    }

    // 분류 쓸거니까 컴페어 요소도 넣어두기
    public readonly System.Comparison<SaveCharacter>[] comparison =
    {
        // 생성시간 정렬
        (lhs, rhs) => lhs.creationTime.CompareTo(rhs.creationTime),
        (lhs, rhs) => rhs.creationTime.CompareTo(lhs.creationTime),

        // 이름 정렬
        (lhs, rhs) => lhs.CharacterData.StringName.CompareTo(rhs.CharacterData.StringName),
        (lhs, rhs) => rhs.CharacterData.StringName.CompareTo(lhs.CharacterData.StringName),

        // 공격력 정렬
        (lhs, rhs) => lhs.CharacterData.Attack.CompareTo(rhs.CharacterData.Attack),
        (lhs, rhs) => rhs.CharacterData.Attack.CompareTo(lhs.CharacterData.Attack),



    };

    public readonly System.Func<SaveCharacter, bool>[] filterings =
    {
        (x) => true,
        (x) => x.CharacterData.Grade == Grade.Common,
        (x) => x.CharacterData.Grade == Grade.Rare,
        (x) => x.CharacterData.Grade == Grade.Epic,
    };


    private List<UiHeroSlot> uiSlotList = new List<UiHeroSlot>();
    // 정렬을 위한 리스트
    private List<SaveCharacter> saveCharacterDataList = new List<SaveCharacter>();

    public UiHeroInfo HeroInfo;

    public UiHeroSlot prefab;
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
    public UnityEvent<SaveCharacter> onSelectSlot;

    private void OnSelectSlot(SaveCharacter saveCharacter)
    {
        Debug.Log(saveCharacter);
        HeroInfo.SetCharacterInfo(saveCharacter);
    }

    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);

    }


    private void OnDisable()
    {
        saveCharacterDataList = null;
    }



    public void SetSaveCharacterDataList(List<SaveCharacter> source)
    {
        saveCharacterDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveCharacter> GetSaveCharacterDataList()
    {
        return saveCharacterDataList;
    }

    private void UpdateSlots()
    {
        var list = saveCharacterDataList.Where(filterings[(int)filter]).ToList();
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
                    onSelectSlot.Invoke(newSlot.SaveCharacterData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotList.Count; i++)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetCharacter(list[i]);
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
        saveCharacterDataList.Add(SaveCharacter.GetRandomItem());
        UpdateSlots();
    }

    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }


        saveCharacterDataList.Remove(uiSlotList[selectedSlotIndex].SaveCharacterData);
        UpdateSlots();

    }
}
