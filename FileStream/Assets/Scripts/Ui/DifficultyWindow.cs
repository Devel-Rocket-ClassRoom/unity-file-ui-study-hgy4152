using Newtonsoft.Json;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DifficultyData
{
    public int selected;
    public string selectDiff;
}


public class DifficultyWindow : GenericWindow
{
    public Toggle[] toggles;
    public Button applyButton;
    public Button cancelButton;

    private DifficultyData Data { get; set; } = new DifficultyData();
    private static JsonSerializerSettings settings = new JsonSerializerSettings();
    private string SaveDirectory;
    private string SavePath;
    private string fileName = "difficulty.json";

    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);

        applyButton.onClick.AddListener(OnApply);
        cancelButton.onClick.AddListener(OnCancel);

        SaveDirectory = Path.Combine(Application.persistentDataPath, "/Difficulty");
        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }
    }

    public override void Open()
    {
        base.Open();

        
       

        if(File.Exists(SavePath))
        {
            var json = File.ReadAllText(SavePath);
            var saveData = JsonConvert.DeserializeObject<DifficultyData>(json, settings);

            toggles[saveData.selected].isOn = true;
            Debug.Log("로드");

        }
        else
        {
            toggles[0].isOn = true;

        }

    }

    public override void Close()
    {
        base.Close();
    }

    public void OnEasy(bool active)
    {
        if(active)
        {
            Debug.Log("OnEasy");
            Data.selectDiff = "Easy";
            Data.selected = 0;
        }
    }

    public void OnNormal(bool active)
    {

        if (active)
        {
            Debug.Log("OnNormal");
            Data.selectDiff = "Normal";
            Data.selected = 1;

        }
    }

    public void OnHard(bool active)
    {

        if (active)
        {
            Debug.Log("OnHard");
            Data.selectDiff = "Hard";
            Data.selected = 2;

        }
    }

    public void OnApply()
    {


        var json = JsonConvert.SerializeObject(Data, settings);
        SavePath = Path.Combine(SaveDirectory, fileName);

        File.WriteAllText(SavePath, json);
        Debug.Log("저장");

        windowManager.Open(0);

    }
    public void OnCancel()
    {
        windowManager.Open(0);
    }

}
