using UnityEngine;
using SaveDataVC = SaveDataV4;
public class SaveLoadTest1 : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data = new SaveDataVC();
            SaveLoadManager.Save();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            if(SaveLoadManager.Load())
            {
                //Debug.Log($"{SaveLoadManager.Data.Name}\n{SaveLoadManager.Data.Gold}\n{SaveLoadManager.Data.ItemId}");

                foreach(var saveItemData in SaveLoadManager.Data.ItemList)
                {
                    Debug.Log(saveItemData.InstanceId);
                }

            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }
        }
    }
}
