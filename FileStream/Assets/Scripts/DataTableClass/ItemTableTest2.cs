using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemTableTest2 : MonoBehaviour
{

    public Image icon;
    public LocalizationText textName;
    public LocalizationText textDesc;

    public void OnEnable()
    {
        SetEmpty();
    }

    public void SetEmpty()
    {
        icon.sprite = null;
        textName.Id = string.Empty;
        textDesc.Id = string.Empty;

        textName.text.text = string.Empty;
        textDesc.text.text = string.Empty;

    }

    public void SetItemData(string itemId)
    {
        ItemData data = DataTableManager.ItemTable.Get(itemId);
        SetItemData(data);
    }
    private void SetItemData(ItemData data)
    {
        

        icon.sprite = data.SpriteIcon;
        textName.Id = data.Name;
        textDesc.Id = data.Desc;


        textName.OnChangedId();
        textDesc.OnChangedId();

    }
}
