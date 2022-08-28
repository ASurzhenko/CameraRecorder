using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathRecorder : MonoBehaviour
{
    [SerializeField] Camera Cam;
    public bool CanStartRecord{get; private set;}
    private List<Vector3> pathList = new List<Vector3>();
    private List<Vector3> pathList_rotation = new List<Vector3>();
    private List<float> pathList_zoom = new List<float>();
    private int curPosIndex;
    private string path;
    private const int MAXFILEINDEX = 9;
    private int lastFileIndex;
    public int LastFileIndex
    {
        get
        {
            lastFileIndex = PlayerPrefs.GetInt("LastFileIndex", 0);
            return lastFileIndex;
        }
        set
        {
            PlayerPrefs.SetInt("LastFileIndex", value);
        }
    }
    private void Start() {
        path = Application.persistentDataPath + "/PathData";
        print("Save path: " + path);
    }
    public void StartRecord()
    {
        pathList.Clear();
        pathList_rotation.Clear();
        pathList_zoom.Clear();
        curPosIndex = 0;
        CanStartRecord = true;
    }
    public void StopRecord()
    {
        Save();
        CanStartRecord = false;
    }
    void Update()
    {
        if(CanStartRecord)
        {
            Record();
        }
    }
    void Record()
    {
        pathList.Add(Cam.transform.position);
        pathList_rotation.Add(Cam.transform.eulerAngles);
        pathList_zoom.Add(Cam.fieldOfView);
        curPosIndex++;   
    }
    void Save()
    {
        RecordData r = new RecordData(pathList, pathList_rotation, pathList_zoom, DateTime.Now.ToString());
        string jsonStr = JsonUtility.ToJson(r);
        string savePath = path + "_" + LastFileIndex + ".json";
        if(LastFileIndex < 9)
            LastFileIndex++;
        else    
            LastFileIndex = 0;
        File.WriteAllText(savePath, jsonStr);
    }
    void OnDrawGizmos() {
        if(Application.isPlaying)
        {
            if(pathList.Count > 1)
            {
                for (int i = 1; i < pathList.Count; i++)
                {
                    if(pathList[i - 1] != pathList[i])
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(pathList[i - 1], pathList[i]);
                    }
                }
            }
        }
    }

    [Serializable]
    public class RecordData 
    {
        public List<Vector3> positions;
        public List<Vector3> rotations;
        public List<float> zooms;
        public string date;
        public RecordData(List<Vector3> positions, List<Vector3> rotations, List<float> zooms, string date)
        {
            this.positions = positions;
            this.rotations = rotations;
            this.zooms = zooms;
            this.date = date;
        }
    }
}
