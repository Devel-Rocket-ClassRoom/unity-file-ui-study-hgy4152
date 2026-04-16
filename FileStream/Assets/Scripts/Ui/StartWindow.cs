using UnityEngine;
using UnityEngine.UI;

public class StartWindow : GenericWindow
{
    public Button continueButton;
    public Button startButton;
    public Button optionButton;
    public bool canContinue;

    
    private void Awake()
    {
        // 코드로 OnClick에 연결
        continueButton.onClick.AddListener(OnContinue);
        startButton.onClick.AddListener(OnNewGame);
        optionButton.onClick.AddListener(OnOption);
    }


    public override void Open()
    {
        base.Open();
        continueButton.gameObject.SetActive(canContinue);

        if (!canContinue)
        {
            firstSelected = startButton.gameObject;
        }    
    }

    public override void Close()
    {
        base.Close();

    }

    public void OnContinue()
    {
        Debug.Log("OnContinue");
    }

    public void OnNewGame()
    {
        Debug.Log("OnNewGame");

    }

    public void OnOption()
    {
        Debug.Log("OnOption");

    }
}
