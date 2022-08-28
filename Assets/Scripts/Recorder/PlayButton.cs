using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PathRecorder;

public class PlayButton : MonoBehaviour
{
    private List <Vector3> pathList = new List<Vector3>();
    private List <Vector3> pathList_rotation = new List<Vector3>();
    private List <float> pathList_zoom = new List<float>();
    public bool CanPlayRecord{get; private set;}
    [SerializeField] private GameObject RecordPanel;
    [SerializeField] private Player player;
    private int curPosIndex, curPosIndex_rotation;
    private Text ButtonText
    {
        get
        {
            return GetComponentInChildren<Text>();
        }
    }
    public Button MyButton
    {
        get
        {
            return GetComponent<Button>();
        }
    }
    private void Awake() {
        MyButton.onClick.AddListener(() => OnEmptyClick());
    }
    public void SetUp(RecordData r)
    {
        ButtonText.text = "Saved File " + (transform.GetSiblingIndex() + 1) + "\n" + r.date;

        curPosIndex = 0;
        curPosIndex_rotation = 0;
        MyButton.onClick.RemoveAllListeners();
        MyButton.onClick.AddListener(() => OnButtonClick());
        pathList = r.positions;
        pathList_rotation = r.rotations;
        pathList_zoom = r.zooms;
    }

    void OnEmptyClick()
    {
        SceneManager.LoadSceneAsync(SceneNames.MainScene);
    }
    private void OnButtonClick()
    {
        RecordPanel.SetActive(false);
        CanPlayRecord = true;
        player.Play(pathList, pathList_rotation, pathList_zoom);
    }
}
