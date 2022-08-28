using System.Collections;
using System.Collections.Generic;
//using Recorder;
using UnityEngine;
using UnityEngine.SceneManagement;

enum RecordState
{
    NotRecord,
    Record
}
public class UIManager : MonoBehaviour
{
    [SerializeField] private PathRecorder pathRecorder;
    [SerializeField] private GameObject startRecordbtn, stopRecordBtn;
    private RecordState recordState;
    
#region Path Recording
    public void OnClickStartPathRecord()
    {
        recordState = RecordState.Record;
        startRecordbtn.SetActive(false);
        stopRecordBtn.SetActive(true);

        pathRecorder.StartRecord();
    }
    public void OnClickStopPathRecord()
    {
        recordState = RecordState.NotRecord;
        startRecordbtn.SetActive(true);
        stopRecordBtn.SetActive(false);

        pathRecorder.StopRecord();
    }
    
#endregion

    public void GoToPlayScene()
    {
        if(recordState == RecordState.Record)
            OnClickStopPathRecord();

        SceneManager.LoadSceneAsync(SceneNames.PlayScene);
    }
}
