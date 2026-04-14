using TMPro;
using UnityEngine;

public class StringTableText : MonoBehaviour
{
    public string id;
    public TextMeshProUGUI text;
    void Start()
    {
        OnChangedId();
    }

    // Update is called once per frame
    void OnChangedId()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }
}
