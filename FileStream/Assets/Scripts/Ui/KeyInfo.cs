using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyInfo : MonoBehaviour
{
    // 호출될 함수가 있는 Key 스크립트 (인스펙터에서 드래그하거나 코드로 찾기)
    public KeyboardWindow keyboard;

    void Start()
    {

        Button[] allButtons = GetComponentsInChildren<Button>();

        foreach (Button btn in allButtons)
        {

            GameObject buttonObj = btn.gameObject;

            // 각자 본인 넣기 위해 람다식으로 넣음
            btn.onClick.AddListener(() => { keyboard.OnButtonClicked(buttonObj); });
        }
    }
}
