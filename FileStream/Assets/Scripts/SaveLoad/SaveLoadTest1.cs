using UnityEngine;
using SaveDataVC = SaveDataV3;
public class SaveLoadTest1 : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data = new SaveDataVC();
            SaveLoadManager.Data.Name = "test3";
            SaveLoadManager.Data.Gold = 4000;
            //itemid는 DataManager.Get(itemId)으로 받아서 넣어도 됌
            SaveLoadManager.Save();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            if(SaveLoadManager.Load())
            {
                Debug.Log($"{SaveLoadManager.Data.Name}\n{SaveLoadManager.Data.Gold}\n{SaveLoadManager.Data.ItemId}");

            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }
        }
    }
}
