using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static PathRecorder;

public class RecordPanel : MonoBehaviour
{
    [SerializeField] private PlayButton[] PlayButtons;
    void Start()
    {
        for (int i = 0; i < PlayButtons.Length; i++)
        {

            RecordData r = GetRecordData(i);
            if(r != null)
                PlayButtons[i].SetUp(r);
        }
    }
    RecordData GetRecordData(int index)
    {
        string path = Application.persistentDataPath + "/PathData" + "_" + index + ".json";
        if (File.Exists(path))
        {
            string jsonStr = File.ReadAllText(path);
            RecordData r = JsonUtility.FromJson<RecordData>(jsonStr);
            return r;
        }
        
        return null;
    }
}
