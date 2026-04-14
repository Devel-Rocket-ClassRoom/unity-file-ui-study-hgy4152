using UnityEngine;
using UnityEngine.UI;

public class CharacterTableTest1 : MonoBehaviour
{
    public string characterId;

    public Image icon;
    public LocalizationText textName;

    public CharacterTableTest2 characterInfo;

    private void OnEnable()
    {
        OnChangeCharcterId();
    }

    private void OnValidate()
    {
        OnChangeCharcterId();
    }
    public void OnChangeCharcterId()
    {
        CharacterData data = DataTableManager.CharacterTable.Get(characterId);
        if (data != null)
        {
            icon.sprite = data.SpriteIcon;
            textName.Id = data.Name;

            textName.OnChangedId();
        }

    }

    public void OnClick()
    {
        characterInfo.SetCharacterData(characterId);
    }
}
