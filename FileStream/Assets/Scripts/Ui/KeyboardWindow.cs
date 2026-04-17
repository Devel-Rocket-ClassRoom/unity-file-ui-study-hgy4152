using System;
using System.Collections;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardWindow : GenericWindow
{
    public TextMeshProUGUI header;

    public Button cancel;
    public Button delete;
    public Button accept;

    private Coroutine cursor;





    public void Awake()
    {
        cancel.onClick.AddListener(OnCancel);
        delete.onClick.AddListener(OnDelete);
        accept.onClick.AddListener(OnAccept);

        // 수업
        var keys = rootKeyboard.GetComponentsInChildren<Button>();
        foreach (var key in keys)
        {
            var text = key.GetComponentInChildren<TextMeshProUGUI>();
            key.onClick.AddListener(() => Onkey(text.text));
        }
       
    }


    private void OnCancel()
    {
        CoroutineOn();
    }
    private void OnDelete()
    {
        header.text = header.text.Remove(header.text.Length - 1);

        if(header.text == string.Empty)
        {
            CoroutineOn();
        }
    }
    private void OnAccept()
    {
        Debug.Log(header.text);
    }

    public void OnButtonClicked(GameObject sender)
    {
        CoroutineOff();

        header.text += sender.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    


    public override void Open()
    {

        // sb.Clear();
        // timer = 0f;
        // blink = false;
        // UpdateInputField();

        CoroutineOn();
        base.Open();

        

    }

    public override void Close()
    {
        base.Close();
        CoroutineOff();

    }


    private void CoroutineOn()
    {
        if (cursor != null)
        {
            StopCoroutine(cursor);
        }

        cursor = StartCoroutine(CursorBlink());

    }
    private void CoroutineOff()
    {
        if (cursor != null)
        {
            StopCoroutine(cursor);
            cursor = null; 
            header.text = string.Empty;
        }
    }

    IEnumerator CursorBlink()
    {
        yield return null;

        while (true)
        {
            Debug.Log("blink");
            header.text = "_";
            yield return new WaitForSeconds(0.3f);
            header.text = string.Empty;
            yield return new WaitForSeconds(0.3f);

        }

    }


    #region 수업 내용
    private readonly StringBuilder sb = new StringBuilder();
    public TextMeshProUGUI inputField;
    public int maxCharacters = 7;
    public GameObject rootKeyboard;
    private float timer = 0f;

    private float cursorDelay = 0.5f;
    private bool blink;
    private void Onkey(string key)
    {
        if (sb.Length < maxCharacters)
        {
            sb.Append(key);
            UpdateInputField();
        }
    }

    public void OnCancelC()
    {
        sb.Clear();
        UpdateInputField();

    }

    public void OnDeleteC()
    {
        if(sb.Length > 0)
        {
            sb.Length -= 1;
            UpdateInputField();

        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > cursorDelay)
        {
            timer = 0f;
            blink = !blink;
            UpdateInputField();

        }
    }

    private void UpdateInputField()
    {
        bool showCursor = sb.Length < maxCharacters && !blink;

        if (showCursor)
        {
            sb.Append('_');
        }
        // 스트링 빌더 값 바로 사용
        inputField.SetText(sb);

        if (showCursor)
        {
            sb.Length -= 1;
        }
    }
    #endregion
}
