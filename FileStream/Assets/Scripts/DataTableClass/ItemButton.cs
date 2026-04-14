using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public string itemId;

    public Image icon;
    public LocalizationText textName;

    public ItemTableTest2 itemInfo;

    private void OnEnable()
    {
        OnChangeItemId();
    }

    private void OnValidate()
    {
        OnChangeItemId();
    }
    public void OnChangeItemId()
    {
        ItemData data = DataTableManager.ItemTable.Get(itemId);
        if(data != null )
        {
            icon.sprite = data.SpriteIcon;
            textName.Id = data.Name;

            textName.OnChangedId();
        }

    }

    public void OnClick()
    {
        itemInfo.SetItemData(itemId);
    }
}
 