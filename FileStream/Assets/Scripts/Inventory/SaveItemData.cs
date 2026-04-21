using UnityEngine;
using System;
using System.Data;
using Newtonsoft.Json;

[Serializable]
public class SaveItemData
{
    public Guid InstanceId { get; set; }

    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData ItemData { get; set; }


    public DateTime creationTime{ get; set; }

    public SaveItemData()
    {
        // json에서 알아서 직렬화, 역직렬화 처리해줌
        InstanceId = Guid.NewGuid();
        creationTime = DateTime.Now;
    }

    public static SaveItemData GetRandomItem()
    {
        SaveItemData newItem = new SaveItemData();
        newItem.ItemData = DataTableManager.ItemTable.GetRandom();
        return newItem;
    }

    public override string ToString()
    {
        return $"{InstanceId}\n{creationTime}\n{ItemData.Id}";
    }
}
