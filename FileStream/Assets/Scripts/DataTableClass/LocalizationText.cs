using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Icons;

[ExecuteInEditMode]
public class LocalizationText : MonoBehaviour
{
#if UNITY_EDITOR
    public Languages editorLang;
#endif
    public string Id;
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged += OnChangeLanguage;

            OnChangedId();
        }
#if UNITY_EDITOR
        else
        {
            OnChangeLanguage(editorLang);
            //OnChangedId();
        }
#endif
    }

    private void OnDisable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged -= OnChangeLanguage;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Variables.Languages = Languages.Korean;
            OnChangedId();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Variables.Languages = Languages.English;
            OnChangedId();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Variables.Languages = Languages.Japanese;
            OnChangedId();
        }
    }


    private void OnValidate()
    {
#if UNITY_EDITOR
        OnChangeLanguage(editorLang);
        //OnChangedId();
#else
        OnChangeLanguage();
        //OnChangedId();
#endif
    }

    public void OnChangedId()
    {
        text.text = DataTableManager.StringTable.Get(Id);
    }

    private void OnChangeLanguage()
    {
        text.text = DataTableManager.StringTable.Get(Id);
    }

#if UNITY_EDITOR
    private void OnChangeLanguage(Languages lang)
    {
        var stringTable = DataTableManager.GetStringTable(lang);
        text.text = stringTable.Get(Id);
    }
#endif

    private static void RefreshAllInEditor(Languages lang)
    {
        var all = Object.FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);

        foreach (var loc in all)
        {
            loc.editorLang = lang;
            loc.OnChangeLanguage(lang);
        }
    }

    [ContextMenu("ChangeLanguageKr")]
    private void ChangeLanguageKr()
    {
        Variables.Languages = Languages.Korean;
        RefreshAllInEditor(Languages.Korean);
    }
    [ContextMenu("ChangeLanguageEn")]
    private void ChangeLanguageEn()
    {
        Variables.Languages = Languages.English;
        RefreshAllInEditor(Languages.English);
    }
    [ContextMenu("ChangeLanguageJp")]
    private void ChangeLanguageJp()
    {
        Variables.Languages = Languages.Japanese;
        RefreshAllInEditor(Languages.Japanese);
    }
}
