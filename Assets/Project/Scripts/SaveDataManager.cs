using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    public SaveData TemporarySaveData;
    private string DataPath = "/Project/savedata.json";

    public void Save()
    {
        File.WriteAllText(Application.dataPath + DataPath, JsonUtility.ToJson(TemporarySaveData), System.Text.Encoding.UTF8);
    }

    public void Load()
    {
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + DataPath), TemporarySaveData);
    }
}
