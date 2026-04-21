using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInvenSlot : MonoBehaviour
{
    public int slotIndex = -1;
    public Image imageIcon;
    public TextMeshProUGUI textName;

    public Button button;

    public SaveItemData SaveItemData {  get; private set; }

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        SaveItemData = null;
    }

    public void SetItem(SaveItemData data)
    {
        SaveItemData = data;
        imageIcon.sprite = SaveItemData.ItemData.SpriteIcon;
        textName.text = SaveItemData.ItemData.StringName; 
    }


    
}
