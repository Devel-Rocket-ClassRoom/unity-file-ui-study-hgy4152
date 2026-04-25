using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiHeroInfo : MonoBehaviour
{
    public Image characterImage;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI characterDescription;

    public TextMeshProUGUI characterAtk;
    public TextMeshProUGUI characterHealth;
    public TextMeshProUGUI characterDef;


    public void SetCharacterInfo(SaveCharacter saveCharacter)
    {
        if (saveCharacter == null)
        {
            // 캐릭터 정보가 없는 경우 처리
            Debug.Log("캐릭터 정보가 없습니다.");
            return;
        }
        // 캐릭터 정보를 UI에 표시하는 로직을 여기에 작성
        // 예: 이미지, 이름, 레벨 등
        Debug.Log($"캐릭터 이름: {saveCharacter.CharacterData.StringName}");
    }
}
