using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Vector3> pathList = new List<Vector3>();
    public List<Vector3> pathList_rotation = new List<Vector3>();
    public List<float> pathList_zoom = new List<float>();
    public bool CanPlayRecord { get; private set; }
    [SerializeField] Camera Cam;
    [SerializeField] private int FramesToUpdate;
    [SerializeField] GameObject RecordPanel;
    public int curPosIndex;
    bool CanStopPlaying => curPosIndex >= pathList.Count;
    public void Play(List<Vector3> pathList, List<Vector3> pathList_rotation, List<float> pathList_zoom)
    {
        curPosIndex = 0;
        this.pathList = pathList;
        this.pathList_rotation = pathList_rotation;
        this.pathList_zoom = pathList_zoom;
        CanPlayRecord = true;
    }
    
    private void Update()
    {
        if (CanPlayRecord)
        {
            Play();
        }
    }
    void Play()
    {
        if(CanStopPlaying)
        {
            CanPlayRecord = false;
            StartCoroutine(StopPlaying());
            return;
        }

        if (curPosIndex < pathList.Count)
        {
            Cam.transform.position = pathList[curPosIndex];
            Cam.transform.localRotation = Quaternion.Euler(pathList_rotation[curPosIndex]);
            Cam.fieldOfView = pathList_zoom[curPosIndex];
            curPosIndex++;
        }
    }
    IEnumerator StopPlaying()
    {
        yield return new WaitForSeconds(1);
        RecordPanel.SetActive(true);
    }
}
