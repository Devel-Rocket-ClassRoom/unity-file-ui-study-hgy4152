using UnityEngine;
using UnityEngine.UI;

public class CharacterTableTest2 : MonoBehaviour
{
    public Image icon;
    public LocalizationText textName;
    public LocalizationText textDesc;
    public LocalizationText textAttack;

    public void OnEnable()
    {
        SetEmpty();
    }

    public void SetEmpty()
    {
        icon.sprite = null;
        textName.Id = string.Empty;
        textDesc.Id = string.Empty;
        //textAttack.Id = string.Empty;

        textName.text.text = string.Empty;
        textDesc.text.text = string.Empty;
        //textAttack.text.text = string.Empty;

    }

    public void SetCharacterData(string characterId)
    {
        CharacterData data = DataTableManager.CharacterTable.Get(characterId);
        SetCharacterData(data);
    }
    private void SetCharacterData(CharacterData data)
    {


        icon.sprite = data.SpriteIcon;
        textName.Id = data.Name;
        textDesc.Id = data.Desc;
        //textAttack.Id = $"{data.Attack}";

        textName.OnChangedId();
        textDesc.OnChangedId();
        //textAttack.OnChangedId();

    }
}
