using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private Text ButtonText;
    [SerializeField] private Controller[] touchControllers;
    private Controller mouseController;
    private bool isMouseController;
    private void Awake() {
        mouseController = GetComponent<CameraMouseController>();
        mouseController.enabled = false;
        if(!IsMobile())
            OnSwitchController();
    }
    public void OnSwitchController()
    {
        isMouseController = !isMouseController;
        mouseController.enabled = isMouseController;
        Array.ForEach(touchControllers, (x) => x.enabled = !isMouseController);
        ButtonText.text = isMouseController ? "Controller\nTouch" : "Controller\nMouse";  
        mouseController.rotate = !isMouseController;
    }
    private bool IsMobile() => Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
}
