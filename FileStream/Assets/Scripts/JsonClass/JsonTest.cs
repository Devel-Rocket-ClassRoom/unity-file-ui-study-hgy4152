using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

[System.Serializable]
public class SomeClass
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;
    public int index;

    // 다른 클래스에서 쓸려고 이렇게 하긴 했는데

    public List<SomeClass> allObjects = new List<SomeClass>();

}
[System.Serializable]
public class ObjectSaveData
{
    public string prefabName;

    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;

}


public class JsonTest : MonoBehaviour
{
   public string filename = "test.json";

    public string FileFullPath => Path.Combine(Application.persistentDataPath, "JsonTest",filename);

    public JsonSerializerSettings jsonSettings;


    // 수업내용
    public string[] prefabNames =
    {
        "Cube",
        "Sphere",
        "Capsule",
        "Cylinder"
    };

    private GameObject target;

    private void Awake()
    {
        target = GetComponent<GameObject>();

        jsonSettings = new JsonSerializerSettings();
        jsonSettings.Formatting = Formatting.Indented;

        // 컨버터별로 나눠서
        jsonSettings.Converters.Add(new Vector3Converter());
        jsonSettings.Converters.Add(new QuaternionConverter());
        jsonSettings.Converters.Add(new ColorConverter());
    }

    public void Save()
    {
        // 직렬화
        SomeClass obj = new SomeClass();

        obj.pos = target.transform.position;
        obj.rot = target.transform.rotation;
        obj.scale = target.transform.localScale;
        obj.color = target.GetComponent<Renderer>().material.color;


        var json = JsonConvert.SerializeObject(obj, jsonSettings);
        File.WriteAllText(FileFullPath, json);
    }

    public void Load()
    {
        var json = File.ReadAllText(FileFullPath);
        var obj = JsonConvert.DeserializeObject<SomeClass>(json);

        target.transform.position = obj.pos;
        target.transform.rotation = obj.rot;
        target.transform.localScale = obj.scale;
        target.GetComponent<Renderer>().material.color = obj.color;
    }


    public void SaveAll(List<GameObject> goList, string name)
    {
        SomeClass root = new SomeClass();
        foreach (var go in goList)
        {
            SomeClass obj = new SomeClass();

            obj.pos = go.transform.position;
            obj.rot = go.transform.rotation;
            obj.scale = go.transform.localScale;
            obj.color = go.GetComponent<Renderer>().material.color;

            switch(go.tag)
            {
                case "Cube":
                    obj.index = 0;
                    break;
                case "Capsule":
                    obj.index = 1;
                    break;
                case "Sphere":
                    obj.index = 2;
                    break;
            }
 

            root.allObjects.Add(obj); // 리스트에 차곡차곡 담기
        }

        string json = JsonConvert.SerializeObject(root, jsonSettings);
        string path = Path.Combine(Application.persistentDataPath, "JsonTest", $"{name}.json");
        File.WriteAllText(path, json);

    }

    public SomeClass LoadAll(string name)
    {
        string path = Path.Combine(Application.persistentDataPath, "JsonTest", $"{name}.json");

        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<SomeClass>(json);
    }



    // 수업내용
    private void CreateRandomObject()
    {
        var prefabName = prefabNames[Random.Range(0,prefabNames.Length)];
        var prefab = Resources.Load<JsonTestObject>(prefabName);
        var obj = Instantiate(prefab);

        // 오브젝트 배치 . . . //
    }

}
